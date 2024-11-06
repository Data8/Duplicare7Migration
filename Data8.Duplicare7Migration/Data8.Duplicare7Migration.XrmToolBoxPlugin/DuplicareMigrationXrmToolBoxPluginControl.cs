using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Crm.Sdk.Messages;
using System.IO;
using System.Threading;
using XrmToolBox.Extensibility.Interfaces;
using System.Net;

namespace Data8.Duplicare7Migration.XrmToolBoxPlugin
{
    public partial class DuplicareMigrationXrmToolBoxPluginControl : PluginControlBase, IPrivatePlugin
    {
        private Settings mySettings;
        private bool canUpgrade;
        private bool hasUpgrade;
        
        public DuplicareMigrationXrmToolBoxPluginControl()
        {
            InitializeComponent();
        }

        private void Data8DuplicareXrmToolBoxPluginControl_Load(object sender, EventArgs e)
        {
            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();

                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }
        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data8DuplicareXrmToolBoxPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), mySettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);
            
            LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);

            IsReadyForUpgrade();
        }

        private void IsReadyForUpgrade()
        {
            hasUpgrade = false;
            canUpgrade = false;

            var solutionQry = new QueryExpression("solution");
            solutionQry.ColumnSet = new ColumnSet("solutionid", "version", "uniquename");
            var solutionNameFilter = solutionQry.Criteria.AddFilter(LogicalOperator.Or);
            solutionNameFilter.AddCondition("uniquename", ConditionOperator.Equal, "Data8DuplicateDetectionPlus");
            solutionNameFilter.AddCondition("uniquename", ConditionOperator.Equal, "Data8DuplicateDetectionPlus_Upgrade");

            var solutions = Service.RetrieveMultiple(solutionQry).Entities;
            if (!solutions.Any())
            {
                MessageBox.Show("Cannot find duplicare");
            } 
            else
            {
                var mainSolution = solutions.FirstOrDefault(s => s.GetAttributeValue<string>("uniquename") == "Data8DuplicateDetectionPlus");
                var versionNumber = new Version(mainSolution.GetAttributeValue<string>("version"));
                if (versionNumber > new Version(7, 0))
                    MessageBox.Show("A later version of duplicare is already installed");

                hasUpgrade = solutions.Any(s => s.GetAttributeValue<string>("uniquename") == "Data8DuplicateDetectionPlus_Upgrade");
                canUpgrade = true;
            }

            startMigrationButton.Enabled = canUpgrade;
        }

        private void migrateButton_Click(object sender, EventArgs e)
        {
            logMessageListView.Items.Clear();
            startMigrationButton.Enabled = false;

            if (!canUpgrade)
                return;

            WorkAsync(new WorkAsyncInfo
            {
                Work = (worker, args) =>
                {
                    var results = new List<ListViewItem>();
                    try
                    {
                        if (hasUpgrade)
                        {
                            AddLogMessage("Already had stage and upgrade solution imported");
                        }
                        else
                        {
                            worker.ReportProgress(-1, "Installing new solution for Stage & Upgrade...");
                            AddLogMessage("Starting installation");

                            var jobId = StartSolutionImport();
                            var count = 0;

                            worker.ReportProgress(-1, "Importing changes...");

                            AddLogMessage("Solution Importing...");
                            while (true)
                            {
                                Thread.Sleep(10000);
                                count += 10000;

                                var job = Service.Retrieve("asyncoperation", jobId, new ColumnSet("statuscode"));
                                var status = job.GetAttributeValue<OptionSetValue>("statuscode").Value;

                                if (status == 30)
                                    break;

                                if (status == 31)
                                    throw new ApplicationException("Import failed");

                                if (status == 32)
                                    throw new ApplicationException("Import cancelled");

                                AddLogMessage($"Still importing - {count / 1000} seconds...", true);
                            }

                            AddLogMessage("Solution Imported");
                        }

                        var steps = GetSteps();
                        AddLogMessage($"{steps.Count} steps to move");

                        foreach (var step in steps)
                        {
                            var entity = new Entity("sdkmessageprocessingstep", step.Id);
                            var newId = (Guid)step.GetAttributeValue<AliasedValue>("newplugin.plugintypeid").Value;
                            entity["plugintypeid"] = new EntityReference("plugintype", newId);
                            Service.Update(entity);

                            AddLogMessage($"Moved step {step.Id} to new package");
                        }

                        var upgradeJobId = StartSolutionUpgrade();
                        var upgradeCount = 0;
                        worker.ReportProgress(-1, "Promoting Upgrade solution...");
                        AddLogMessage($"Promoting upgrade solution...");
                        AddLogMessage($"Starting...");
                        while (true)
                        {
                            Thread.Sleep(10000);
                            upgradeCount += 10000;

                            var job = Service.Retrieve("asyncoperation", upgradeJobId, new ColumnSet("statuscode"));
                            var status = job.GetAttributeValue<OptionSetValue>("statuscode").Value;

                            if (status == 30)
                                break;

                            if (status == 31)
                                throw new ApplicationException("Import failed");

                            if (status == 32)
                                throw new ApplicationException("Import cancelled");

                            AddLogMessage($"Still upgrading - {upgradeCount / 1000} seconds...", true);
                        }

                        if (apiKeyTextBox.Text.Length > 0)
                        {
                            AddLogMessage("Setting API Key...");
                            var apiKey = apiKeyTextBox.Text;
                            var existingConfig = Service.RetrieveMultiple(new QueryExpression("data8_globaldedupeconfiguration") { ColumnSet = new ColumnSet("data8_apikey") }).Entities;
                            if (existingConfig.Any())
                            {
                                var settings = existingConfig.First();
                                settings["data8_apikey"] = apiKey;
                                Service.Update(settings);
                            }
                        }

                        AddLogMessage($"Completed");
                    }
                    catch (Exception ex)
                    {
                        AddLogMessage("Process Failed");
                        AddLogMessage(ex.Message);
                    }

                    args.Result = results;
                },
                ProgressChanged = progress =>
                {
                    SetWorkingMessage(progress.UserState.ToString());
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (args.Result is List<ListViewItem> list)
                        logMessageListView.Items.AddRange(list.ToArray());
                }
            });
        }

        private List<Entity> GetSteps()
        {
            var fetchXml = @"
                <fetch xmlns:generator='MarkMpn.SQL4CDS'>
                  <entity name='sdkmessageprocessingstep'>
                    <attribute name='sdkmessageprocessingstepid' />
                    <link-entity name='plugintype' to='plugintypeid' from='plugintypeid' alias='plugintype' link-type='inner'>
                      <attribute name='plugintypeid' />
                      <link-entity name='pluginassembly' to='pluginassemblyid' from='pluginassemblyid' alias='pluginassembly' link-type='inner'>
                        <attribute name='pluginassemblyid' />
                        <link-entity name='pluginassembly' to='name' from='name' alias='newassembly' link-type='inner'>
                          <attribute name='pluginassemblyid' />
                          <filter>
                            <condition attribute='packageid' operator='not-null' />
                          </filter>
                          <order attribute='pluginassemblyid' />
                        </link-entity>
                        <filter>
                          <condition attribute='name' operator='eq' value='Data8.DuplicateDetectionPlus' />
                          <condition attribute='packageid' operator='null' />
                        </filter>
                        <order attribute='pluginassemblyid' />
                      </link-entity>
                      <link-entity name='plugintype' to='name' from='name' alias='newplugin' link-type='inner'>
                        <attribute name='plugintypeid' />
                        <filter>
                          <condition attribute='pluginassemblyid' operator='eq' valueof='newassembly.pluginassemblyid' />
                        </filter>
                        <order attribute='plugintypeid' />
                      </link-entity>
                      <order attribute='plugintypeid' />
                    </link-entity>
                    <order attribute='sdkmessageprocessingstepid' />
                  </entity>
                </fetch>
            ";

            return Service.RetrieveMultiple(new FetchExpression(fetchXml)).Entities.ToList();
        }

        private Guid StartSolutionImport()
        {
            var solutionUrl = solutionLocationTextBox.Text;
            var solutionFileContents = new byte[0];
            using (var webclient = new WebClient())
            {
                solutionFileContents = webclient.DownloadData(solutionUrl);
            }

            var request = new ExecuteAsyncRequest
            {
                Request = new ImportSolutionRequest
                {
                    CustomizationFile = solutionFileContents,
                    OverwriteUnmanagedCustomizations = true,
                    PublishWorkflows = true,
                    HoldingSolution = true
                }
            };

            var response = (ExecuteAsyncResponse)Service.Execute(request);
            return response.AsyncJobId;
        }

        private Guid StartSolutionUpgrade()
        {
            var request = new ExecuteAsyncRequest
            {
                Request = new DeleteAndPromoteRequest()
                {
                    UniqueName = "Data8DuplicateDetectionPlus"
                }
            };

            var response = (ExecuteAsyncResponse)Service.Execute(request);
            return response.AsyncJobId;
        }

        delegate void AddLogMessageCallback(string message, bool overwrite);

        private void AddLogMessage(string message, bool overwrite = false)
        {
            if (logMessageListView.InvokeRequired)
            {
                AddLogMessageCallback i = new AddLogMessageCallback(AddLogMessage);
                this.Invoke(i, new object[] { message, overwrite });
            }
            else
            {
                var listItem = new ListViewItem(DateTime.Now.ToString("G"));
                listItem.SubItems.Add(message);

                if (overwrite)
                    logMessageListView.Items[logMessageListView.Items.Count - 1] = listItem;
                else
                    logMessageListView.Items.Add(listItem);
            }
        }
    }
}
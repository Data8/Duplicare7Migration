namespace Data8.Duplicare7Migration.XrmToolBoxPlugin
{
    partial class DuplicareMigrationXrmToolBoxPluginControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DuplicareMigrationXrmToolBoxPluginControl));
            this.startMigrationButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.migrationTab = new System.Windows.Forms.TabPage();
            this.apiKeyGroup = new System.Windows.Forms.GroupBox();
            this.apiKeyTextBox = new System.Windows.Forms.TextBox();
            this.logMessageListView = new System.Windows.Forms.ListView();
            this.timestampColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.logColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.settingsTab = new System.Windows.Forms.TabPage();
            this.solutionGroup = new System.Windows.Forms.GroupBox();
            this.solutionLocationTextBox = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabControl1.SuspendLayout();
            this.migrationTab.SuspendLayout();
            this.apiKeyGroup.SuspendLayout();
            this.settingsTab.SuspendLayout();
            this.solutionGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // startMigrationButton
            // 
            this.startMigrationButton.Location = new System.Drawing.Point(20, 213);
            this.startMigrationButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.startMigrationButton.Name = "startMigrationButton";
            this.startMigrationButton.Size = new System.Drawing.Size(312, 44);
            this.startMigrationButton.TabIndex = 5;
            this.startMigrationButton.Text = "Start Migration";
            this.startMigrationButton.UseVisualStyleBackColor = true;
            this.startMigrationButton.Click += new System.EventHandler(this.migrateButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.migrationTab);
            this.tabControl1.Controls.Add(this.settingsTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1746, 960);
            this.tabControl1.TabIndex = 6;
            // 
            // migrationTab
            // 
            this.migrationTab.Controls.Add(this.apiKeyGroup);
            this.migrationTab.Controls.Add(this.logMessageListView);
            this.migrationTab.Controls.Add(this.startMigrationButton);
            this.migrationTab.Controls.Add(this.label1);
            this.migrationTab.Location = new System.Drawing.Point(8, 39);
            this.migrationTab.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.migrationTab.Name = "migrationTab";
            this.migrationTab.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.migrationTab.Size = new System.Drawing.Size(1730, 913);
            this.migrationTab.TabIndex = 0;
            this.migrationTab.Text = "Migrate";
            this.migrationTab.UseVisualStyleBackColor = true;
            // 
            // apiKeyGroup
            // 
            this.apiKeyGroup.Controls.Add(this.apiKeyTextBox);
            this.apiKeyGroup.Location = new System.Drawing.Point(728, 75);
            this.apiKeyGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.apiKeyGroup.Name = "apiKeyGroup";
            this.apiKeyGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.apiKeyGroup.Size = new System.Drawing.Size(740, 119);
            this.apiKeyGroup.TabIndex = 9;
            this.apiKeyGroup.TabStop = false;
            this.apiKeyGroup.Text = "API Key";
            // 
            // apiKeyTextBox
            // 
            this.apiKeyTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.apiKeyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.apiKeyTextBox.Location = new System.Drawing.Point(4, 28);
            this.apiKeyTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.apiKeyTextBox.Name = "apiKeyTextBox";
            this.apiKeyTextBox.Size = new System.Drawing.Size(732, 68);
            this.apiKeyTextBox.TabIndex = 8;
            // 
            // logMessageListView
            // 
            this.logMessageListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logMessageListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.timestampColumn,
            this.logColumn});
            this.logMessageListView.FullRowSelect = true;
            this.logMessageListView.HideSelection = false;
            this.logMessageListView.Location = new System.Drawing.Point(20, 269);
            this.logMessageListView.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.logMessageListView.Name = "logMessageListView";
            this.logMessageListView.Size = new System.Drawing.Size(1694, 625);
            this.logMessageListView.SmallImageList = this.imageList1;
            this.logMessageListView.TabIndex = 7;
            this.logMessageListView.UseCompatibleStateImageBehavior = false;
            this.logMessageListView.View = System.Windows.Forms.View.Details;
            // 
            // timestampColumn
            // 
            this.timestampColumn.Text = "Timestamp";
            this.timestampColumn.Width = 258;
            // 
            // logColumn
            // 
            this.logColumn.Text = "Log Entry";
            this.logColumn.Width = 460;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1547, 200);
            this.label1.TabIndex = 6;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // settingsTab
            // 
            this.settingsTab.Controls.Add(this.solutionGroup);
            this.settingsTab.Location = new System.Drawing.Point(8, 39);
            this.settingsTab.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.Size = new System.Drawing.Size(1730, 913);
            this.settingsTab.TabIndex = 1;
            this.settingsTab.Text = "Settings";
            this.settingsTab.UseVisualStyleBackColor = true;
            // 
            // solutionGroup
            // 
            this.solutionGroup.Controls.Add(this.solutionLocationTextBox);
            this.solutionGroup.Location = new System.Drawing.Point(4, 4);
            this.solutionGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.solutionGroup.Name = "solutionGroup";
            this.solutionGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.solutionGroup.Size = new System.Drawing.Size(1516, 119);
            this.solutionGroup.TabIndex = 10;
            this.solutionGroup.TabStop = false;
            this.solutionGroup.Text = "Solution Location";
            // 
            // solutionLocationTextBox
            // 
            this.solutionLocationTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.solutionLocationTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.solutionLocationTextBox.Location = new System.Drawing.Point(4, 28);
            this.solutionLocationTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.solutionLocationTextBox.Name = "solutionLocationTextBox";
            this.solutionLocationTextBox.Size = new System.Drawing.Size(1508, 37);
            this.solutionLocationTextBox.TabIndex = 8;
            this.solutionLocationTextBox.Text = resources.GetString("solutionLocationTextBox.Text");
            // 
            // DuplicareMigrationXrmToolBoxPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "DuplicareMigrationXrmToolBoxPluginControl";
            this.Size = new System.Drawing.Size(1746, 960);
            this.Load += new System.EventHandler(this.Data8DuplicareXrmToolBoxPluginControl_Load);
            this.tabControl1.ResumeLayout(false);
            this.migrationTab.ResumeLayout(false);
            this.migrationTab.PerformLayout();
            this.apiKeyGroup.ResumeLayout(false);
            this.apiKeyGroup.PerformLayout();
            this.settingsTab.ResumeLayout(false);
            this.solutionGroup.ResumeLayout(false);
            this.solutionGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button startMigrationButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage migrationTab;
        private System.Windows.Forms.ListView logMessageListView;
        private System.Windows.Forms.ColumnHeader logColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader timestampColumn;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox apiKeyGroup;
        private System.Windows.Forms.TextBox apiKeyTextBox;
        private System.Windows.Forms.TabPage settingsTab;
        private System.Windows.Forms.GroupBox solutionGroup;
        private System.Windows.Forms.TextBox solutionLocationTextBox;
    }
}

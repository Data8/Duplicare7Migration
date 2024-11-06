using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Data8.Duplicare7Migration.XrmToolBoxPlugin.Migration

{
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "Data8 duplicare Package Migration"),
        ExportMetadata("Description", "This tool will step by step the process of updating Duplicare to use packages."),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAACxMAAAsTAQCanBgAAAYcaVRYdFhNTDpjb20uYWRvYmUueG1wAAAAAAA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/Pg0KPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS42LWMxNDIgNzkuMTYwOTI0LCAyMDE3LzA3LzEzLTAxOjA2OjM5ICAgICAgICAiPg0KICA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPg0KICAgIDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0RXZ0PSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VFdmVudCMiIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1sbnM6cGhvdG9zaG9wPSJodHRwOi8vbnMuYWRvYmUuY29tL3Bob3Rvc2hvcC8xLjAvIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE4IChXaW5kb3dzKSIgeG1wOkNyZWF0ZURhdGU9IjIwMTgtMDktMTFUMTM6MDA6MTErMDE6MDAiIHhtcDpNZXRhZGF0YURhdGU9IjIwMTgtMDktMTFUMTM6MDA6MTErMDE6MDAiIHhtcDpNb2RpZnlEYXRlPSIyMDE4LTA5LTExVDEzOjAwOjExKzAxOjAwIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOmVlNDUxNGE1LTUzNjItNzQ0ZS04YzVhLTg0NDczZGZlNmZiYSIgeG1wTU06RG9jdW1lbnRJRD0iYWRvYmU6ZG9jaWQ6cGhvdG9zaG9wOjFhNmNlYTBmLTRhMTQtMjQ0NC05YzA1LTMzZDc3MTI3MTk5NiIgeG1wTU06T3JpZ2luYWxEb2N1bWVudElEPSJ4bXAuZGlkOjhhYTMwNjA3LTc4NWQtZmE0Mi1iZThkLWE4NThmM2YxZGUwZSIgZGM6Zm9ybWF0PSJpbWFnZS9wbmciIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiPg0KICAgICAgPHhtcE1NOkhpc3Rvcnk+DQogICAgICAgIDxyZGY6U2VxPg0KICAgICAgICAgIDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJjcmVhdGVkIiBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOjhhYTMwNjA3LTc4NWQtZmE0Mi1iZThkLWE4NThmM2YxZGUwZSIgc3RFdnQ6d2hlbj0iMjAxOC0wOS0xMVQxMzowMDoxMSswMTowMCIgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWRvYmUgUGhvdG9zaG9wIENDIDIwMTggKFdpbmRvd3MpIiAvPg0KICAgICAgICAgIDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJzYXZlZCIgc3RFdnQ6aW5zdGFuY2VJRD0ieG1wLmlpZDplZTQ1MTRhNS01MzYyLTc0NGUtOGM1YS04NDQ3M2RmZTZmYmEiIHN0RXZ0OndoZW49IjIwMTgtMDktMTFUMTM6MDA6MTErMDE6MDAiIHN0RXZ0OnNvZnR3YXJlQWdlbnQ9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE4IChXaW5kb3dzKSIgc3RFdnQ6Y2hhbmdlZD0iLyIgLz4NCiAgICAgICAgPC9yZGY6U2VxPg0KICAgICAgPC94bXBNTTpIaXN0b3J5Pg0KICAgIDwvcmRmOkRlc2NyaXB0aW9uPg0KICA8L3JkZjpSREY+DQo8L3g6eG1wbWV0YT4NCjw/eHBhY2tldCBlbmQ9InIiPz6SQKyvAAADvElEQVRIS7WUX2gcRRzHd/Z2d9a9u9xt7pJLLvenTUKT3ENLaay1YPVBxRbbIgUpKFoQCw2lpfgg6oMpgSh9qEUUqSD6UGgtCsUKLVVsqWlThIq214D2KqGx9E8ud7ndu9vd2b0d57pjntybK5IPw+53vr9ZvszMzgCMMdceCw/u3//7tiCKPelsR1SlLhMSwAQh6+jk+5tX9TzdF3omFX4xlzp97EuMXVpuCWMGpDp19vtz3x7/+cx31OK4SDQIQGDs4OHnd+yklj/sJTry3lunvjpKBABcJpvYum3Dxo25W4W7ExPHj01d6072ecP8EOjbHzUWT6XiY3u3JZMxVQ15Zv9AryTyv05deOHlVzzHD56+/dG1ykguOzKShlD0nGrVVBRJCUKjVvWcFjAD8NBgLJ3punRpJp+f9azp6Znr12dXr+7PpiOe0wLWJjtm5cohA/N1A5kGSqW7iHnzzzu9yU6A3VBQ7Vi3t7k5/jA3GdcLp22tQAQ1lsAcTDwupzbRrg/MJQLKwFZRHWpKjPlimS9VHvoc7BmVU095ugXsTSYrAHvWg4BMhBtX3c7muvMwAhPrSc0b0oJ2r4qGMW/MniFPogOhPmXFZl5i7zCBPQMbWSc/PXho/2t3FuP8Y92AF8VbUu31/cbhjzmE6CB/2AE/fP35jyc+c/VS3bCvLHBSvsN4+0Nn6rL50SfWiW/oIH8YAa7rTp89KQQESRR/++lUtVg13pnE5UWval+46IkWMAK00vzCvTnTMkmzatrc3T9ogexettNdKXNug/Z9YATUtBKyDEGUiqUFs16t1+g/SgCbhsBLOcy5tO8DI8BGCLuuEo7qVd2oaQ3HoYXmHwqx63D4/wWIEgR8gEyC6HK5zNu25xMwdjhAPmccBUZAhxqHsmLWdKI1XQN1y/MJOAHJ6QM848JnBISjsezwGiIEQcActjW9JsqkWfEIWJcRwqmHk2gF+yT/dePq5O4tT4yu6R9cYQFprPAcx4MtT8ofDM6Ec7t4GKXjfAiMj49T6YPanYwExaiMIITJ4Q0FeVW+bOeR+uzatdmuXjrIH8YEPQaGcxKERES7M8OJIBG4uFiqNwWTtgKWaDjIdP79L9u6JB8xwHUQWgpoj0cOMOzlDGg4trW8M2igZQmwG9x8sUKaYdiqzK+MCKSFpLa+ZR+0G1d/OfLugZv534ne8cbY9jf3VSoar+ud5y+qu16Vk4yjwAgwFh+c+2LCMpuXHSGTGx3Swuj2HNEA2XxQie3bAxTFq/4HHPcPbGjFx2RxJhQAAAAASUVORK5CYII="),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", "iVBORw0KGgoAAAANSUhEUgAAAFAAAABQCAIAAAABc2X6AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAACxMAAAsTAQCanBgAAAYcaVRYdFhNTDpjb20uYWRvYmUueG1wAAAAAAA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/Pg0KPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS42LWMxNDIgNzkuMTYwOTI0LCAyMDE3LzA3LzEzLTAxOjA2OjM5ICAgICAgICAiPg0KICA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPg0KICAgIDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0RXZ0PSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VFdmVudCMiIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1sbnM6cGhvdG9zaG9wPSJodHRwOi8vbnMuYWRvYmUuY29tL3Bob3Rvc2hvcC8xLjAvIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE4IChXaW5kb3dzKSIgeG1wOkNyZWF0ZURhdGU9IjIwMTgtMDktMTFUMTM6MDA6MTErMDE6MDAiIHhtcDpNZXRhZGF0YURhdGU9IjIwMTgtMDktMTFUMTM6MDA6MTErMDE6MDAiIHhtcDpNb2RpZnlEYXRlPSIyMDE4LTA5LTExVDEzOjAwOjExKzAxOjAwIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOmVlNDUxNGE1LTUzNjItNzQ0ZS04YzVhLTg0NDczZGZlNmZiYSIgeG1wTU06RG9jdW1lbnRJRD0iYWRvYmU6ZG9jaWQ6cGhvdG9zaG9wOjFhNmNlYTBmLTRhMTQtMjQ0NC05YzA1LTMzZDc3MTI3MTk5NiIgeG1wTU06T3JpZ2luYWxEb2N1bWVudElEPSJ4bXAuZGlkOjhhYTMwNjA3LTc4NWQtZmE0Mi1iZThkLWE4NThmM2YxZGUwZSIgZGM6Zm9ybWF0PSJpbWFnZS9wbmciIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiPg0KICAgICAgPHhtcE1NOkhpc3Rvcnk+DQogICAgICAgIDxyZGY6U2VxPg0KICAgICAgICAgIDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJjcmVhdGVkIiBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOjhhYTMwNjA3LTc4NWQtZmE0Mi1iZThkLWE4NThmM2YxZGUwZSIgc3RFdnQ6d2hlbj0iMjAxOC0wOS0xMVQxMzowMDoxMSswMTowMCIgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWRvYmUgUGhvdG9zaG9wIENDIDIwMTggKFdpbmRvd3MpIiAvPg0KICAgICAgICAgIDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJzYXZlZCIgc3RFdnQ6aW5zdGFuY2VJRD0ieG1wLmlpZDplZTQ1MTRhNS01MzYyLTc0NGUtOGM1YS04NDQ3M2RmZTZmYmEiIHN0RXZ0OndoZW49IjIwMTgtMDktMTFUMTM6MDA6MTErMDE6MDAiIHN0RXZ0OnNvZnR3YXJlQWdlbnQ9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE4IChXaW5kb3dzKSIgc3RFdnQ6Y2hhbmdlZD0iLyIgLz4NCiAgICAgICAgPC9yZGY6U2VxPg0KICAgICAgPC94bXBNTTpIaXN0b3J5Pg0KICAgIDwvcmRmOkRlc2NyaXB0aW9uPg0KICA8L3JkZjpSREY+DQo8L3g6eG1wbWV0YT4NCjw/eHBhY2tldCBlbmQ9InIiPz6SQKyvAAAKwUlEQVR4XuWaeXQV1R3H587MW+blvewheZCExWyYIEhTAlhBodBqpedAVVQaoVCoB05BEYpCWepCrVZ6auEAohWBpljAlsVKlb1lM0BBQCAsAglZyPb2bbb+3pv7QrYXAr2Xdzh8/uH3uzNh3nd+v3vv7947SFVV5l7iTguGxwX8fo/LqTMYBMHE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"),
        ExportMetadata("BackgroundColor", "White"),
        ExportMetadata("PrimaryFontColor", "Black"),
        ExportMetadata("SecondaryFontColor", "Gray")]
    public class DuplicareMigrationXrmToolBoxPlugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new DuplicareMigrationXrmToolBoxPluginControl();
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        public DuplicareMigrationXrmToolBoxPlugin()
        {
            // If you have external assemblies that you need to load, uncomment the following to 
            // hook into the event that will fire when an Assembly fails to resolve
            // AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolveEventHandler);
        }

        /// <summary>
        /// Event fired by CLR when an assembly reference fails to load
        /// Assumes that related assemblies will be loaded from a subfolder named the same as the Plugin
        /// For example, a folder named Sample.XrmToolBox.MyPlugin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Assembly AssemblyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Assembly loadAssembly = null;
            Assembly currAssembly = Assembly.GetExecutingAssembly();

            // base name of the assembly that failed to resolve
            var argName = args.Name.Substring(0, args.Name.IndexOf(","));

            // check to see if the failing assembly is one that we reference.
            List<AssemblyName> refAssemblies = currAssembly.GetReferencedAssemblies().ToList();
            var refAssembly = refAssemblies.Where(a => a.Name == argName).FirstOrDefault();

            // if the current unresolved assembly is referenced by our plugin, attempt to load
            if (refAssembly != null)
            {
                // load from the path to this plugin assembly, not host executable
                string dir = Path.GetDirectoryName(currAssembly.Location).ToLower();
                string folder = Path.GetFileNameWithoutExtension(currAssembly.Location);
                dir = Path.Combine(dir, folder);

                var assmbPath = Path.Combine(dir, $"{argName}.dll");

                if (File.Exists(assmbPath))
                {
                    loadAssembly = Assembly.LoadFrom(assmbPath);
                }
                else
                {
                    throw new FileNotFoundException($"Unable to locate dependency: {assmbPath}");
                }
            }

            return loadAssembly;
        }
    }
}
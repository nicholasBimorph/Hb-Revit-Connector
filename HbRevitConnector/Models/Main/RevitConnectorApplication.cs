using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;

namespace HbRevitConnector.Models.Main
{
    public class RevitConnectorApplication : IExternalApplication
    {
        /// <summary>Implement this method to execute some tasks when Autodesk Revit starts.</summary>
        /// <param name="application">A handle to the application being started.</param>
        /// <returns>Indicates if the external application completes its work successfully.</returns>
        public Result OnStartup(UIControlledApplication application)
        {
            string tabName = "HB Workshops";

            application.CreateRibbonTab(tabName);

            RibbonPanel ribbonPanelProductivity = application.CreateRibbonPanel(tabName, "API Workshop");

            this.AddButton(ribbonPanelProductivity, new RevitConnectorButton());

            return Result.Succeeded;
        }

        private void AddButton(RibbonPanel ribbonPanel, RevitConnectorButton buttonData)
        {
            PushButtonData pushButtonData = new PushButtonData(buttonData.InternalButtonName,
                buttonData.VisibleButtonName, Assembly.GetExecutingAssembly().Location, buttonData.GetType().FullName);

            PushButton pushButton = (PushButton)ribbonPanel.AddItem(pushButtonData);
            //pushButton.ToolTip = buttonData.ToolTip;

        }

        /// <summary>Implement this method to execute some tasks when Autodesk Revit shuts down.</summary>
        /// <param name="application">A handle to the application being shut down.</param>
        /// <returns>Indicates if the external application completes its work successfully.</returns>
        public Result OnShutdown(UIControlledApplication application)
        {

            return Result.Succeeded;
        }
    }
}

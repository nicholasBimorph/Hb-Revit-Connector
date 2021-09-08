using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace HbRevitConnector.Models.Main
{
    public class RevitConnectorButton : IExternalCommand
    {

        public string InternalButtonName { get; }

        public string VisibleButtonName { get; }

        public RevitConnectorButton()
        {
            InternalButtonName = "Revit connector";

            VisibleButtonName = "Revit connector";
        }

        /// <summary>Overload this method to implement and external command within Revit.</summary>
        /// <returns> The result indicates if the execution fails, succeeds, or was canceled by user. If it does not
        /// succeed, Revit will undo any changes made by the external command. </returns>
        /// <param name="commandData"> An ExternalCommandData object which contains reference to Application and View
        /// needed by external command.</param>
        /// <param name="message"> Error message can be returned by external command. This will be displayed only if the command status
        /// was "Failed".  There is a limit of 1023 characters for this message; strings longer than this will be truncated.</param>
        /// <param name="elements"> Element set indicating problem elements to display in the failure dialog.  This will be used
        /// only if the command status was "Failed".</param>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var document = commandData.Application.ActiveUIDocument.Document;

            var revitConnectorMain = new RevitConnectorMain(document);

            revitConnectorMain.StartUp();

           return revitConnectorMain.Run();
        }
    }
}

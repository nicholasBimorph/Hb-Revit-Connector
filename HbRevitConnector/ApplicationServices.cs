using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using HB.RestAPI.Core.Services;
using HbRevitConnector.Settings;

namespace HbRevitConnector
{
    internal class ApplicationServices
    {
        internal Document Document { get; }

        internal HBApiClient WebClientService { get; }

        internal IList<string> ParameterNames { get; }

        public ApplicationServices(Document document, HBApiClient webClientService)
        {
            this.Document = document;

            this.WebClientService = webClientService;

            this.ParameterNames = new List<string>
            {
                AreaParameterNames.LevelParameterName,
                AreaParameterNames.PlotParameterName,
                AreaParameterNames.BlockParameterName,
                AreaParameterNames.SpaceTypeParameterName,
                AreaParameterNames.UnitTypeParameterName,
                AreaParameterNames.TenureParameterName,
                AreaParameterNames.AccessibilityTypeParameterName,
                AreaParameterNames.AreaParameterName,
                AreaParameterNames.NumberParameterName,
                AreaParameterNames.AreaTypeParameterName
            };


        }

    }
}

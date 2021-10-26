using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using HB.RestAPI.Core.Models;
using HB.RestAPI.Core.Models.Factories;
using HB.RestAPI.Core.Models.Types;
using HbConnector.Core.Interfaces;
using HbRevitConnector.Extensions;
using HbRevitConnector.Settings;

namespace HbRevitConnector.Models.Harvesters
{
    internal class AreaHarvester : IDataHarvester
    {
        private readonly Document _document;
        private readonly DataNodeFactory _dataNodeFactory;
        private readonly ApplicationServices _applicationServices;
        private const double mmFactor = 304;


        internal AreaHarvester(DataNodeFactory dataNodeFactory, ApplicationServices applicationServices)
        {
             _document = applicationServices.Document;

             _dataNodeFactory = dataNodeFactory;

             _applicationServices = applicationServices;
        }

        public IList<DataNode> Harvest()
        {
            var areas = new FilteredElementCollector(_document)
                        .OfCategory(BuiltInCategory.OST_Areas).WhereElementIsNotElementType()
                        .Cast<Area>().ToList();

            var dataNodes = new List<DataNode>(areas.Count);

            foreach (var area in areas)
            {
                var properties = new List<Property>();

                foreach (string parameterName in _applicationServices.ParameterNames)
                {
                    if (parameterName == AreaParameterNames.AreaParameterName)
                    {
                        var parameter = area.LookupParameter(parameterName);

                        if(parameter==null) continue;

                        string parameterValue = area.LookupParameter(parameterName).AsValueString();

                        var property = new Property(parameterName, parameterValue);

                        properties.Add(property);
                    }

                    else
                    {
                        var parameter = area.LookupParameter(parameterName);

                        if (parameter == null) continue;

                        string parameterValue = area.LookupParameter(parameterName).GetParameterValueAsString();

                        var property = new Property(parameterName, parameterValue);

                        properties.Add(property);
                    }
                }

                var hbArea = new HbArea(0, properties);

               var dataNode =  _dataNodeFactory.Create(hbArea);

                dataNodes.Add(dataNode);
            }

            return dataNodes;
        }
    }
}

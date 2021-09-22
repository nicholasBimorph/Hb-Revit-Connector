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


namespace HbRevitConnector.Models.Harvesters
{
    internal class AreaHarvester : IDataHarvester
    {
        private readonly Document _document;
        private readonly DataNodeFactory _dataNodeFactory;

        internal AreaHarvester(Document document, DataNodeFactory dataNodeFactory)
        {
             _document = document;

             _dataNodeFactory = dataNodeFactory;
        }

        private const double mmFactor = 304;

        public IList<DataNode> Harvest()
        {
            var areas = new FilteredElementCollector(_document)
                        .OfCategory(BuiltInCategory.OST_Areas).WhereElementIsNotElementType()
                        .Cast<Area>().ToList();

            var dataNodes = new List<DataNode>(areas.Count);

            foreach (var area in areas)
            {
                var paramName = new Property("Area Name", area.Name);

                var paramPerimeter = new Property("Perimeter ", area.Perimeter * mmFactor);

                var properties = new Property[] { paramName, paramPerimeter };

                var hbArea = new HbArea(area.Area * mmFactor, properties);

               var dataNode =  _dataNodeFactory.Create(hbArea);

                dataNodes.Add(dataNode);
            }

            return dataNodes;
        }
    }
}

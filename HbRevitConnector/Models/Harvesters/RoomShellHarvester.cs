using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using HB.RestAPI.Core.Interfaces;
using HB.RestAPI.Core.Models;
using HbConnector.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using HB.RestAPI.Core.Models.Factories;

namespace HbRevitConnector.Models.Harvesters
{
    internal class RoomShellHarvester : IDataHarvester
    {
       
        private readonly IGeometryExtractor<Solid, Room> _roomsolidExtractor;
        private readonly Document _document;
        private readonly IGeometryConverter<Solid> _meshConverter;
        private readonly DataNodeFactory _dataNodeFactory;


        internal RoomShellHarvester(
            DataNodeFactory dataNodeFactory, 
            IGeometryExtractor<Solid,Room> roomsolidExtractor,
            IGeometryConverter<Solid> meshConverter,
            Document document)
        {
           

            _document = document;

            _roomsolidExtractor = roomsolidExtractor;

            _meshConverter = meshConverter;

            _dataNodeFactory = dataNodeFactory;

        }


        public IList<DataNode> Harvest()
        {
            var rooms = new FilteredElementCollector(_document)
            .OfCategory(BuiltInCategory.OST_Rooms).WhereElementIsNotElementType()
            .Cast<Room>().ToList();

           var solids = _roomsolidExtractor.Extract(rooms);

           var dataNodes = new List<DataNode>(solids.Count);

           foreach (var solid in solids)
           {
              var hbMesh =  _meshConverter.ToHbType(solid);

             var dataNode =  _dataNodeFactory.Create(hbMesh);


             dataNodes.Add(dataNode);
           }

           return dataNodes;
        }
    }
}

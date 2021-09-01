using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using HB.RestAPI.Core.Interfaces;
using HB.RestAPI.Core.Models;
using HbConnector.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HbRevitConnector.Models.Harvesters
{
    internal class RoomShellHarvester : IDataHarvester
    {
        private readonly ISerializer _serializer;
        private readonly IGeometryExtractor<Solid, Room> _roomShellExtractor;
        private readonly Document _document;

        internal RoomShellHarvester(
            ISerializer serializer, 
            IGeometryExtractor<Solid,Room> roomShellExtractor, 
            Document document)
        {
            _serializer = serializer;

            _document = document;

            _roomShellExtractor = roomShellExtractor;
        }


        public IList<DataNode> Harvest()
        {
            var rooms = new FilteredElementCollector(_document)
            .OfCategory(BuiltInCategory.OST_Rooms).WhereElementIsNotElementType()
            .Cast<Room>().ToList();

           var solids = _roomShellExtractor.Extract(rooms);

            throw new NotImplementedException();
        }
    }
}

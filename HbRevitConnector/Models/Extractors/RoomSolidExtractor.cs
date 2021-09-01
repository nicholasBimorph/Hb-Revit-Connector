using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using HbConnector.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HbRevitConnector.Models.Extractors
{
    internal class RoomSolidExtractor : IGeometryExtractor<Solid, Room>
    {
        public IList<Solid> Extract(IList<Room> hostAppType)
        {
            throw new NotImplementedException();
        }
    }
}

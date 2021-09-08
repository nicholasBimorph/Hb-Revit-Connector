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
            var solids = new List<Solid>();

            foreach (var room in hostAppType)
            {
                var shell = room.ClosedShell;

                foreach (var geoObject in shell)
                {
                    var solid = geoObject as Solid;

                    if (solid == null ||
                        solid.Faces.Size == 0 ||
                        solid.Edges.Size == 0
                        || solid.Volume <= 0) continue;

                    solids.Add(solid);
                }

            }

            return solids;
        }

    }
}

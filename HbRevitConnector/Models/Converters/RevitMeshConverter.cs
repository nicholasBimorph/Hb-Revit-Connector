using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using HB.RestAPI.Core.Interfaces;
using HB.RestAPI.Core.Types;
using HbConnector.Core.Interfaces;
namespace HbRevitConnector.Models.Converters
{
    internal class RevitMeshConverter : IGeometryConverter<Solid>
    {
        public IHbObject ToHbType(Solid geometry)
        {
           
            var controls = new SolidOrShellTessellationControls()
            {
                Accuracy = 0.03,
                LevelOfDetail = 0.1,
                MinAngleInTriangle = 3 * Math.PI / 180.0,
                MinExternalAngleBetweenTriangles = 0.2 * Math.PI
            };


            var triangulatedShell = SolidUtils.TessellateSolidOrShell(geometry, controls);


            var component = triangulatedShell.GetShellComponent(0);

            int totalVertices = component.VertexCount;

            int totalFaces = component.TriangleCount;

            var faces = new int[totalFaces][];

            var vertices = new double[totalVertices][];


            for (int i = 0; i < totalFaces; i++)
            {
                var face = component.GetTriangle(i);

                var faceIndices = new[] { face.VertexIndex0, face.VertexIndex1, face.VertexIndex2 };

                faces[i] = faceIndices;

            }

            for (int i = 0; i < totalVertices; i++)
            {
                var vertex = component.GetVertex(i);

                vertices[i] = new[] { vertex.X, vertex.Y, vertex.Z };

            }

            return new HbMesh(vertices, faces);
        }
    }
}

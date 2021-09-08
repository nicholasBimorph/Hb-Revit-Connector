using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using HB.RestAPI.Core.Interfaces;
using HB.RestAPI.Core.Models.Factories;
using HB.RestAPI.Core.Services;
using HbConnector.Core.Interfaces;
using HbRevitConnector.Models.Converters;
using HbRevitConnector.Models.Extractors;
using HbRevitConnector.Models.Harvesters;

namespace HbRevitConnector.Models.Main
{
    internal class RevitConnectorMain : IApplicationEntryPoint<Result,Document>
    {
        private IGeometryExtractor<Solid, Room> _roomSolidExtractor;
        private ISerializer _serializer;
        private  IDataHarvester _roomShellHarvester;
        private DataNodeFactory _dataNodeFactory;
        private IGeometryConverter<Solid> _meshConverter;

        public Document CADAppDocument { get; }

        internal RevitConnectorMain(Document document)
        {
            this.CADAppDocument = document;
        }

        public Result Run()
        {
            try
            {
                _roomShellHarvester.Harvest();
            }
            catch (Exception e)
            {
               
                return Result.Failed;
            }

            return Result.Succeeded;
        }

        public void StartUp()
        {
           
            _roomSolidExtractor = new RoomSolidExtractor();

            _serializer = new JsonSerializer();

            _dataNodeFactory = new DataNodeFactory(_serializer);

            _meshConverter = new RevitMeshConverter();

            _roomShellHarvester = new RoomShellHarvester(
                _dataNodeFactory, 
                _roomSolidExtractor, 
                _meshConverter,
                this.CADAppDocument);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using HB.RestAPI.Core.Interfaces;
using HB.RestAPI.Core.Models;
using HB.RestAPI.Core.Models.Factories;
using HB.RestAPI.Core.Services;
using HbConnector.Core.Interfaces;
using HbConnector.Core.Settings;
using HbRevitConnector.Models.Converters;
using HbRevitConnector.Models.Extractors;
using HbRevitConnector.Models.Harvesters;
using HB.RestAPI.Core.Settings;

namespace HbRevitConnector.Models.Main
{
    internal class RevitConnectorMain : IApplicationEntryPoint<Result,Document>
    {
        private IGeometryExtractor<Solid, Room> _roomSolidExtractor;
        private ISerializer _serializer;
        private  IDataHarvester _roomShellHarvester;
        private DataNodeFactory _dataNodeFactory;
        private IGeometryConverter<Solid> _meshConverter;
        private  HBApiClient _hbApiClient;

        public Document CADAppDocument { get; }

        internal RevitConnectorMain(Document document)
        {
            this.CADAppDocument = document;
        }

        public Result Run()
        {
            try
            {
               var dataNodes=  _roomShellHarvester.Harvest();

                var applicationDataContainer = new ApplicationDataContainer(dataNodes, TemporaryProjectStream.ProjectStream);

               _hbApiClient.RequestFinished += _hbApiClient_RequestFinished;

                _hbApiClient.AsyncPostRequest(HbApiEndPoints.AsyncPostEndPoint, applicationDataContainer);
            }
            catch (Exception e)
            {
               
                return Result.Failed;
            }

            return Result.Succeeded;
        }

        private void _hbApiClient_RequestFinished(object sender, string e)
        {
            _hbApiClient.RequestFinished -= _hbApiClient_RequestFinished;

            string response = e;
        }

        public void StartUp()
        {
            _roomSolidExtractor = new RoomSolidExtractor();

            _serializer = new JsonSerializer();

            _hbApiClient = new HBApiClient(_serializer);

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

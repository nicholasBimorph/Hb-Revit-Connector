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
using HbRevitConnector.Models.Views;
using HbRevitConnector.ViewModel;

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

                var viewModel = new RevitConnectorViewModel(_roomShellHarvester, _hbApiClient);

                var window = new RevitConnectorWindow(viewModel);

                window.Show();
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

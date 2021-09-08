﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HB.RestAPI.Core.Models;
using HB.RestAPI.Core.Services;
using HB.RestAPI.Core.Settings;
using HbConnector.Core.Interfaces;
using HbConnector.Core.Settings;

namespace HbRevitConnector.ViewModel.Commands
{
    internal class UploadDataCommand : ICommand
    {
        private readonly IDataHarvester _roomShellHarvester;

        private readonly HBApiClient _hbApiClient;

        private const string AsyncPostEndpoint = HbApiEndPoints.AsyncPostEndPoint;

        internal UploadDataCommand(IDataHarvester roomShellHarvester) => _roomShellHarvester = roomShellHarvester;
       

        /// <summary>Defines the method that determines whether the command can execute in its current state.</summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        /// <returns>
        /// <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>Defines the method to be called when the command is invoked.</summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        public void Execute(object parameter)
        {
           var dataNodes =  _roomShellHarvester.Harvest();

           var applicationDataContainer = new ApplicationDataContainer(dataNodes, TemporaryProjectStream.ProjectStream);

            _hbApiClient.RequestFinished += _hbApiClient_RequestFinished;

           _hbApiClient.AsyncPostRequest(AsyncPostEndpoint, applicationDataContainer);

        }

        private void _hbApiClient_RequestFinished(object sender, string e)
        {
            
        }

        /// <summary>Occurs when changes occur that affect whether or not the command should execute.</summary>
        public event EventHandler CanExecuteChanged;
    }
}
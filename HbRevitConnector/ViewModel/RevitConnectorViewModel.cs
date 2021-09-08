using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HbConnector.Core.Interfaces;

namespace HbRevitConnector.ViewModel
{
    internal class RevitConnectorViewModel
    {
        public ICommand RoomShellHarvesterCommand { get; }
        internal RevitConnectorViewModel(IDataHarvester roomShellHarvester)
        {

        }
    }
}

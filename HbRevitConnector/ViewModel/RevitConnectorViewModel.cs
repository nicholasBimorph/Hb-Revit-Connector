using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Autodesk.Revit.DB;
using HB.RestAPI.Core.Services;
using HbConnector.Core.Interfaces;
using HbRevitConnector.Annotations;
using HbRevitConnector.Models;
using HbRevitConnector.ViewModel.Commands;
using Visibility = System.Windows.Visibility;

namespace HbRevitConnector.ViewModel
{
    public class RevitConnectorViewModel: INotifyPropertyChanged
    {
        private Visibility _uploadButtonProgressBarVis = Visibility.Hidden ;
        public ICommand UploadDataCommand { get; }

        public Visibility UploadButtonProgressBarVis
        {
            get => _uploadButtonProgressBarVis;

            set
            {
                _uploadButtonProgressBarVis = value;

                this.OnPropertyChanged(nameof(this.UploadButtonProgressBarVis));
            }
        }

        internal RevitConnectorViewModel(DataHarvesterEngine dataHarvesterEngine, HBApiClient hbApiClient)
        {
            this.UploadDataCommand = new UploadDataCommand(dataHarvesterEngine, hbApiClient);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HB.RestAPI.Core.Models;
using HbConnector.Core.Interfaces;

namespace HbRevitConnector.Models
{
    internal class DataHarvesterEngine
    {
        private readonly IList<IDataHarvester> _dataHarvesters;

        internal DataHarvesterEngine(IList<IDataHarvester> dataHarvesters) => _dataHarvesters = dataHarvesters;
    

        internal IList<DataNode> Run()
        {
            var dataNodes = new List<DataNode>();

            foreach (var dataHarvester in _dataHarvesters)
                dataNodes.AddRange(dataHarvester.Harvest());

            return dataNodes;

        }
    }
}

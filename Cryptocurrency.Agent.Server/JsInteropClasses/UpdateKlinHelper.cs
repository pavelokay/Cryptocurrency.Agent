using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryptocurrency.Agent.Server.Data;
using Cryptocurrency.Agent.Server.CustomComponents;

namespace Cryptocurrency.Agent.Server.JsInteropClasses
{
    public class UpdateKlinHelper
    {
        public StreamKlinData StreamKlineData { get; set; }
        public DateTime FirstTime { get; set; }
        public List<KlinesData> Klines { get; set; }
        public StockChart ParentRef { get; set; }
        public UpdateKlinHelper(StreamKlinData stream, StockChart parent)
        {
            StreamKlineData = stream;
            ParentRef = parent;
        }

        [JSInvokable]
        public StreamKlin UpdateKlin()
        {
            return StreamKlineData.Data;
        }

        [JSInvokable]
        public async Task<List<KlinesData>> GetOldData()
        {
            Klines = new List<KlinesData>();
            await ParentRef.LoadData();
            if (Klines.Any())
            {
                return Klines;
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptocurrency.Agent.Server.Data
{
    public class StatusMarketCapData
    {
        public DateTime Timestamp { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public int Elapsed { get; set; }
        public int CreditCount { get; set; }
    }
}

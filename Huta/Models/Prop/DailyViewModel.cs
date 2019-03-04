using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huta.Models.Prop
{
    public class DailyViewModel
    {
        public DateTime ReasonDate { get; set; }
        public string MainReason { get; set; }
        public string SubMessage { get; set; }
        public string ImagePath { get; set; }
    }
}

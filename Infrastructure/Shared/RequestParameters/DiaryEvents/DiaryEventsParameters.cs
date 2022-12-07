using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestParameters.DiaryEvents
{
    
    public class DiaryEventsParameters : RequestParameters
    {
        public DiaryEventsParameters() => OrderBy = "EventName";

        public string? SearchEventName { get; set; }
    }
}

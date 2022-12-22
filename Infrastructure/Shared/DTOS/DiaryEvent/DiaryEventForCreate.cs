using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.DiaryEvent
{
    public class DiaryEventForCreate
    {
        public string? EventName { get; set; }

        public string? EventDescription { get; set; }

        public DateTime EventDate { get; set; }

        public int EventDuration { get; set; }
    }
}

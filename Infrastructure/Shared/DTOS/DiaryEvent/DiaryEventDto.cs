using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.DiaryEvent
{
    public record DiaryEventDto()
    {
        public Guid? Id { get; set; }

        public Guid? DiaryId { get; set; }

        public string? EventName { get; set; }

        public string? EventDescription { get; set; }

        public DateTime EventDate { get; set; }

        public int EventDuration { get; set; }


    }
}

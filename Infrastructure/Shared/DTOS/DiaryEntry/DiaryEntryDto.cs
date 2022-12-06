using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.DiaryEntry
{
    public record DiaryEntryDto()
    {
        public DateTime EntryDate { get; set; }

        public string? EntryTitle { get; set; }

        public string? EntryText { get; set; }

        public DateTime DataChanged { get; set; }


    }
}

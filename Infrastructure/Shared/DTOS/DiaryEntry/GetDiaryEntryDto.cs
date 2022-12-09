using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.DiaryEntry
{
    public record GetDiaryEntryDto()
    {
        public Guid? Id { get; set; }
        public Guid? DiaryId { get; set; }
        public string? EntryTitle { get; set; }
        public string? EntryText { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}

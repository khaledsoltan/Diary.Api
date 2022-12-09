using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.DiaryEntry
{
    public record DiaryEntryDtoForCreate()
    {

        [Required(ErrorMessage = "EntryTitle is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Address is 50 characters.")]
        public string? EntryTitle { get; set; }

        [Required(ErrorMessage = "EntryText is a required field.")]
        [MaxLength(2000, ErrorMessage = "Maximum length for the Address is 2000 characters.")]
        public string? EntryText { get; set; }

    }
}

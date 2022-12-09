using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Host.Domain.Entites
{
    public class DiaryEntry
    {
        [Column("DiaryEntryID")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Diary))]
        public Guid DiaryId { get; set; }

        [Required(ErrorMessage = "EntryDate  is a required field.")]
        public DateTime EntryDate { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum length for the FirstName is (50) characters.")]
        public string? EntryTitle { get; set; }

        public string? EntryText { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DiarY? Diary { get; set; }
    }
}
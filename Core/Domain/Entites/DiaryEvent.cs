using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Host.Domain.Entites
{
    public class DiaryEvent
    {
        [Column("EventID")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Diary))]
        public Guid DiaryId { get; set; }

        [Required(ErrorMessage = "EventName name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the FirstName is (50) characters.")]
        public string? EventName { get; set; }

        public string? EventDescription { get; set; }
        
        public DateTime EventDate { get; set; }

        public int EventDuration { get; set; }

        public DiarY? Diary { get; set; }
    }
}
using Domain.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Host.Domain.Entites
{
    public class DiarY
    {
        [Column("DiaryID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "WorkSpace name is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the UserName is (100) characters.")]
        public string? DiaryName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [MaxLength(450)]
        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }

        public User? User { get; set; }

        public ICollection<DiaryEvent>? DiaryEvents { get; set; }
        public ICollection<DiaryEntry>? DiaryEntries { get; set; }
        public ICollection<Contact>? Contacts { get; set; }

    }
}

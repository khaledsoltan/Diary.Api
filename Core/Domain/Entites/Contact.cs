using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Host.Domain.Entites
{
    public class Contact
    {
        
        [Column("ContactID")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Diary))]
        public Guid DiaryId { get; set; }
        [Required(ErrorMessage = "FirstName name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the FirstName is (50) characters.")]
        public string? FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum length for the LastName is (50) characters.")]
        public string? LastName { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum length for the Telephone is (50) characters.")]
        public string? Telephone { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum MobilePhone for the MobilePhone is (50) characters.")]
        public string? MobilePhone { get; set; }

        [MaxLength(250, ErrorMessage = "Maximum Email for the Email is (250) characters.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [MaxLength(150, ErrorMessage = "Maximum AddressLine1 for the Telephone is (150) characters.")]
        public string? AddressLine1 { get; set; }


        [MaxLength(100, ErrorMessage = "Maximum City for the City is (100) characters.")]
        public string? City { get; set; }

        [MaxLength(100, ErrorMessage = "Maximum State for the State is (100) characters.")]
        public string? State { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum PostalCode for the PostalCode is (50) characters.")]
        public string? PostalCode { get; set; }

        public DiarY? Diary { get; set; }


    }
}
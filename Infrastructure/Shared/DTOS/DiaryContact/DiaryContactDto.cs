using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Shared.DTOS.DiaryContact
{
    public record DiaryContactDto()
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Telephone { get; set; }

        public string? MobilePhone { get; set; }

        public string? Email { get; set; }

        public string? AddressLine1 { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? PostalCode { get; set; }


    }

}

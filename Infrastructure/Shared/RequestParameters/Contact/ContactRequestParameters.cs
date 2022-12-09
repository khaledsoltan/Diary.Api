using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestParameters.Contact
{
    public class ContactRequestParameters : RequestParameters
    {
        public ContactRequestParameters() => OrderBy = "DiaryName";

        public string? SearchContactName { get; set; }
    }
}

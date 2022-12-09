using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestParameters.DiaryEntry
{
    public class DiaryEntryParameters : RequestParameters
    {
        public DiaryEntryParameters() => OrderBy = "CreatedDate";

        public string? SearchOrder { get; set; }
    }
}

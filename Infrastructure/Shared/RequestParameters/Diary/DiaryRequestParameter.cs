using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestParameters.Diary
{
    public class DiaryRequestParameter : RequestParameters
    {
        public DiaryRequestParameter() => OrderBy = "DiaryName";

        public DateTime? FromDate{ get; set; }
        public DateTime?ToDate { get; set; } 

        public bool ValidDateRange => ToDate > FromDate;

        public string? SearchDiaryName { get; set; }
    }
}

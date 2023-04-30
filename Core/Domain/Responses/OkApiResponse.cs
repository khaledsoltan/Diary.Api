using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public sealed class OkApiResponse<TResult> : ApiBaseResponse
    {
        public TResult Result { get; set; }

        public OkApiResponse(TResult result) : base(true)
        {
            Result = result;
        }
    }
}

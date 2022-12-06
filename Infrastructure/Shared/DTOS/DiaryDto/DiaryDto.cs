using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.DiaryDto
{
    public record DiaryDto(Guid Id, string DiaryName, string UserId, DateTime CreatedDate , DateTime UpdatedDate);

}

using Api.Host.Domain.Entites;
using Repository.Extensions.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Repository.Diary.Extensions.RepositoryExtensions
{
  
    public static class DiaryRepositoryExtension
    {
        public static IQueryable<DiarY> FilterDiaries(this IQueryable<DiarY> diarys,DateTime? fromDate, DateTime? toDate) =>
            diarys.Where(e => (e.CreatedDate >= fromDate && e.CreatedDate <= toDate));

        public static IQueryable<DiarY> Search(this IQueryable<DiarY> diarys, string searchDiaryName)
        {
            if (string.IsNullOrWhiteSpace(searchDiaryName))
                return diarys;

            var lowerCaseTerm = searchDiaryName.Trim().ToLower();

            return diarys.Where(e => e.DiaryName.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<DiarY> Sort(this IQueryable<DiarY> diaries, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return diaries.OrderBy(e => e.DiaryName);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<DiarY>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return diaries.OrderBy(e => e.DiaryName);

            return diaries.OrderBy(orderQuery);
        }
    }

}

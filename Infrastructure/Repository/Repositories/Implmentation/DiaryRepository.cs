using Api.Host.Domain.Entites;
using Domain.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Diary.Extensions.RepositoryExtensions;
using Repository.Diary.Repositories.Contracts;
using Repository.GenericRepository;
using Shared.PagedList;
using Shared.RequestParameters.Diary;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository.Diary.Repositories.Implmentation
{
   

    internal sealed class DiaryRepository : GenericRepository<DiarY>, IDiaryRepository
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _userId ;

        public DiaryRepository(RepositoryContext repositoryContext , IHttpContextAccessor httpContextAccessor)
            : base(repositoryContext)
        {
            _userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        public async Task<PagedList<DiarY>> GetAllDiariesByUserId(DiaryRequestParameter diaryRequestParameter, bool trackChanges) {

            var employees = await FindByCondition(x => x.UserId == _userId, trackChanges)
                    //.FilterDiaries(diaryRequestParameter.FromDate, diaryRequestParameter.ToDate)
                    .Sort(diaryRequestParameter.OrderBy)
                    .Search(diaryRequestParameter.SearchDiaryName)
                    .ToListAsync();

            return PagedList<DiarY>
                .ToPagedList(employees, diaryRequestParameter.PageNumber, diaryRequestParameter.PageSize);
        }

        public async Task<DiarY> GetDiaryByIdAndUserId(Guid DiaryId, String UserId, bool trackChanges)
            => await FindByCondition(x => x.Id == DiaryId && x.UserId == UserId, trackChanges).SingleOrDefaultAsync();


        public void CreateDiary(DiarY diary) 
        {
            diary.CreatedDate = DateTime.Now;
            diary.UserId = _userId;
            Create(diary);
                
         }

        public void DeleteDiary(DiarY diary) => Delete(diary);
    }

}

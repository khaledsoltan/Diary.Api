using Microsoft.AspNetCore.Http;
using Repository.Context;
using Repository.Diary.Repositories.Contracts;
using Repository.Diary.Repositories.Implmentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UnitOfWork
{
   
    public sealed class UnitOfWork : IUnitOfWork { 


        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IDiaryRepository> _diaryRepository;
        private readonly Lazy<IDiaryEventRepository> _diaryEventRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnitOfWork(RepositoryContext repositoryContext , IHttpContextAccessor httpContextAccessor)
        {
            _repositoryContext = repositoryContext;
            _diaryRepository = new Lazy<IDiaryRepository>(() => new DiaryRepository(repositoryContext, httpContextAccessor));
            _diaryEventRepository = new Lazy<IDiaryEventRepository>(() => new DiaryEventRepository(repositoryContext));
        }

        public IDiaryRepository Diary => _diaryRepository.Value;
        public IDiaryEventRepository DiaryEvent => _diaryEventRepository.Value;
        public async Task CompleteAsync() => await _repositoryContext.SaveChangesAsync();

      
    }
}

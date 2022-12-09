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
        private readonly Lazy<IDiaryEntryRepository> _diaryEntryRepositoryy;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnitOfWork(RepositoryContext repositoryContext , IHttpContextAccessor httpContextAccessor)
        {
            _repositoryContext = repositoryContext;
            _diaryRepository = new Lazy<IDiaryRepository>(() => new DiaryRepository(repositoryContext, httpContextAccessor));
            _diaryEventRepository = new Lazy<IDiaryEventRepository>(() => new DiaryEventRepository(repositoryContext));
            _diaryEntryRepositoryy = new Lazy<IDiaryEntryRepository>(()=> new DiaryEntryRepository(repositoryContext));
        }

        public IDiaryRepository Diary => _diaryRepository.Value;
        public IDiaryEventRepository DiaryEvent => _diaryEventRepository.Value;
        public IDiaryEntryRepository DiaryEntry => _diaryEntryRepositoryy.Value;
        public async Task CompleteAsync() => await _repositoryContext.SaveChangesAsync();

      
    }
}

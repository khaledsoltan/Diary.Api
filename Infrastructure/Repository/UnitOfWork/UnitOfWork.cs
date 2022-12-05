using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UnitOfWork
{
   
    public sealed class UnitOfWork : IUnitOfWork { 


        private readonly RepositoryContext _repositoryContext;
    
        public UnitOfWork(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
          

        }

        public async Task CompleteAsync() => await _repositoryContext.SaveChangesAsync();

    }
}

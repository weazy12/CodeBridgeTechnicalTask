using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.DAL.Data;
using TechnicalTask.DAL.Repositories.Interfaces.Base;
using TechnicalTask.DAL.Repositories.Interfaces.Dog;
using TechnicalTask.DAL.Repositories.Realizations.Dog;

namespace TechnicalTask.DAL.Repositories.Realizations.Base
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly TechnicalTaskDbContext _dbContext;
        private IDogRepository? dogRepository;
        public RepositoryWrapper(TechnicalTaskDbContext context)
        {
            _dbContext = context;
        }

        public IDogRepository DogRepository
        {
            get 
            {
                if(dogRepository is null)
                {
                    dogRepository = new DogRepository(_dbContext);
                }
                return new DogRepository(_dbContext);
            }   
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}

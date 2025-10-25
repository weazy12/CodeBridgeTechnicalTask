using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.DAL.Data;
using TechnicalTask.DAL.Repositories.Interfaces.Dog;
using TechnicalTask.DAL.Repositories.Realizations.Base;

namespace TechnicalTask.DAL.Repositories.Realizations.Dog
{
    public class DogRepository : RepositoryBase<DAL.Entities.Dog>, IDogRepository
    {
        public DogRepository(TechnicalTaskDbContext context) : base(context)
        {
        }
    }
}

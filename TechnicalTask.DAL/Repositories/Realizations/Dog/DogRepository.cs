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

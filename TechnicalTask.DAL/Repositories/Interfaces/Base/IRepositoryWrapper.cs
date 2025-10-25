using TechnicalTask.DAL.Repositories.Interfaces.Dog;

namespace TechnicalTask.DAL.Repositories.Interfaces.Base
{
    public interface IRepositoryWrapper 
    {
        public IDogRepository Dog { get; }
        public int SaveChanges();

        public Task<int> SaveChangesAsync();

    }
}

using TechnicalTask.DAL.Repositories.Interfaces.Dog;

namespace TechnicalTask.DAL.Repositories.Interfaces.Base
{
    public interface IRepositoryWrapper 
    {
        public IDogRepository DogRepository { get; }

        public Task<int> SaveChangesAsync();

    }
}

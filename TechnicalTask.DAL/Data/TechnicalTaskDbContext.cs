using Microsoft.EntityFrameworkCore;
using TechnicalTask.DAL.Entities;

namespace TechnicalTask.DAL.Data
{
    public class TechnicalTaskDbContext : DbContext
    {
        public TechnicalTaskDbContext(DbContextOptions<TechnicalTaskDbContext> options)
            : base(options)
        {
        }
        DbSet<Dog> Dogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TechnicalTaskDbContext).Assembly);
        }
    }
}

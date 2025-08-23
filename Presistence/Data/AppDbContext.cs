using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
namespace Presistence.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyRef).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<RoadMap> RoadMaps { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<Rate> Rates { get; set; }

    }
}

using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Presistence.Data.Configs;
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

        DbSet<Trainee> Trainees { get; set; }
        DbSet<Mentor> Mentors { get; set; }
        DbSet<RoadMap> RoadMaps { get; set; }
        DbSet<Phase> Phases { get; set; }
        DbSet<Rate> Rates { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using DomainLayer.Models;
namespace Presistence.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        DbSet<Trainee> Trainees { get; set; }
        DbSet<Mentor> Mentors { get; set; }
        DbSet<RoadMap> RoadMaps { get; set; }
        DbSet<Phase> Phases { get; set; }
        DbSet<Rate> Rates { get; set; }
    }
}

using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CityRouteContext : DbContext
    {
        public CityRouteContext(DbContextOptions<CityRouteContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new MapImageBuild(modelBuilder.Entity<MapImage>());
        }
    }
}

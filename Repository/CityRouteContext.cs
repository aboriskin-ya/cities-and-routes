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
            new ImageBuild(modelBuilder.Entity<Image>());
            new MapBuild(modelBuilder.Entity<Map>());
            new SettingsBuild(modelBuilder.Entity<Settings>());
            new CityBuild(modelBuilder.Entity<City>());
        }
    }
}

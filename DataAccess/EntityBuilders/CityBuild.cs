using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityBuilders
{
    public class CityBuild
    {
        public CityBuild(EntityTypeBuilder<City> entityBuilder)
        {
            entityBuilder.HasKey(c => c.Id);
            entityBuilder.Property(c => c.Name).IsRequired();
            entityBuilder.Property(c => c.X).IsRequired();
            entityBuilder.Property(c => c.Y).IsRequired();
            entityBuilder.HasOne(c => c.Map)
                .WithMany(m => m.Cities)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
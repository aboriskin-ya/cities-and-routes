using DataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess
{
    public class CityBuild
    {
        public CityBuild(EntityTypeBuilder<City> entityBuilder)
        {
            entityBuilder.HasKey(m => m.Id);
            entityBuilder.HasOne(m => m.Map).WithMany().HasForeignKey("MapId");
            entityBuilder.Property(m => m.Name).IsRequired();
            entityBuilder.Property(m => m.X).IsRequired();
            entityBuilder.Property(m => m.Y).IsRequired();
        }
    }
}

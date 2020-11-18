using DataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityBuilders
{
    public class MapBuild
    {
        public MapBuild(EntityTypeBuilder<Map> entityBuilder)
        {
            entityBuilder.HasKey(m => m.Id);
            entityBuilder.Property(m => m.Name).IsRequired();
            entityBuilder.HasOne(m => m.Image).WithMany();
        }
    }
}

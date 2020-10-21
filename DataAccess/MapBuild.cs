using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess
{
    public class MapBuild
    {
        public MapBuild(EntityTypeBuilder<Map> entityBuilder)
        {
            entityBuilder.HasKey(m => m.Id);
            entityBuilder.Property(m => m.Name).IsRequired();
            entityBuilder.HasOne(m => m.Image).WithMany().HasForeignKey("ImageId").HasConstraintName("FK1");
        }
    }
}

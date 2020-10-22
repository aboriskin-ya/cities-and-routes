using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess
{
    public class ImageBuild
    {
        public ImageBuild(EntityTypeBuilder<Image> entityBuilder)
        {
            entityBuilder.HasKey(m => m.Id);
            entityBuilder.Property(m => m.Data).HasColumnType("image").IsRequired();
            entityBuilder.Property(m => m.ContentType).IsRequired();
        }
    }
}

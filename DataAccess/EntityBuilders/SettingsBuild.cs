using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityBuilders
{
    public class SettingsBuild
    {
        public SettingsBuild(EntityTypeBuilder<Settings> entityBuilder)
        {
            entityBuilder.HasKey(s => s.Id);
            entityBuilder.Property(s => s.DisplayingImage).IsRequired();
            entityBuilder.Property(s => s.DisplayingGraph).IsRequired();
            entityBuilder.Property(s => s.VertexSize).IsRequired();
            entityBuilder.Property(s => s.VertexColor).IsRequired();
            entityBuilder.Property(s => s.EdgeSize).IsRequired();
            entityBuilder.Property(s => s.EdgeColor).IsRequired();
            entityBuilder.HasOne(s => s.Map)
                .WithOne(m => m.Settings)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
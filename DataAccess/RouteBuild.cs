using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess
{
    public class RouteBuild
    {
        public RouteBuild(EntityTypeBuilder<Route> entityBuilder)
        {
            entityBuilder.HasKey(r => r.Id);
            entityBuilder.Property(r => r.Distance).IsRequired();
            entityBuilder.HasOne(r => r.Map).WithMany().HasForeignKey("MapId").HasConstraintName("FK1");
            //entityBuilder.HasOne(r => r.FirstCity).WithMany().HasForeignKey("FirstCityId").HasConstraintName("FK1");
            //entityBuilder.HasOne(r => r.SecondCity).WithMany().HasForeignKey("SecondCityId").HasConstraintName("FK1");
        }
    }
}

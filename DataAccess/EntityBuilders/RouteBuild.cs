using DataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityBuilders
{
    public class RouteBuild
    {
        public RouteBuild(EntityTypeBuilder<Route> entityBuilder)
        {
            entityBuilder.HasKey(r => r.Id);
            entityBuilder.Property(r => r.Distance).IsRequired();
        }
    }
}

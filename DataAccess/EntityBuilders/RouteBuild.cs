using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccess.EntityBuilders
{
    public class RouteBuild
    {
        public RouteBuild(EntityTypeBuilder<Route> entityBuilder)
        {
            entityBuilder.HasKey(r => r.Id);
            entityBuilder.Property(r => r.Distance).IsRequired();
            entityBuilder.HasOne(r => r.Map).WithOne().OnDelete(DeleteBehavior.NoAction);
            entityBuilder.HasOne(r => r.FirstCity).WithOne().OnDelete(DeleteBehavior.NoAction);
            entityBuilder.HasOne(r => r.SecondCity).WithOne().OnDelete(DeleteBehavior.NoAction);
        }
    }
}

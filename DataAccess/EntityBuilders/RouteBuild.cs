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
            entityBuilder.HasOne(r => r.Map)
                .WithMany(m => m.Routes)
                .OnDelete(DeleteBehavior.Cascade);
            entityBuilder.HasOne(r => r.FirstCity)
                .WithMany(c => c.RoutesWhenThisFirst)
                .OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasOne(r => r.SecondCity)
                .WithMany(c => c.RoutesWhenThisSecond)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class PathResolver
    {
        public Guid MapId { get; set; }
        public Guid FirstCityId { get; set; }
        public Guid SecondCityId { get; set; }
    }
}

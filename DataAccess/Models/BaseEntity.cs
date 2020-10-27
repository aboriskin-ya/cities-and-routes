using System;

namespace DataAccess
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreateOnUTC { get; set; }
        public DateTimeOffset UpdatedOnUTC { get; set; }
    }
}

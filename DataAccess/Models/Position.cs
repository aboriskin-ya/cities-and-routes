using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}

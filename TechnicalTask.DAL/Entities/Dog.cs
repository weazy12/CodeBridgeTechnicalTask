using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTask.DAL.Entities
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;

        [Column("tail_length")]
        public int TailLength { get; set; }
        public int Weight { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Masivian.Models
{
    public class objBet
    {
        public int IdUser { get; set; }
        [Range(1,10000)]
        public int Value { get; set; }
        [Range(0,36)]
        public int Number { get; set; }
        public objColor Color { get; set; }
        public decimal Won { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masivian.Models
{
    [Serializable]
    public class objRoulette
    {
        public int Id { get; set; }
        public objState State { get; set; }
        public List<objBet> Bets { get; set; }

        public objRoulette()
        {
            Bets = new List<objBet>();
        }
    }
}

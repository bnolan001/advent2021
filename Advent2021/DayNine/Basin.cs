using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayNine
{
    public class Basin
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public List<int> Walls { get; set; }

        public int GetScore()
        {
            return Walls.Sum();
        }

        public Basin()
        {
            Walls = new List<int>();
        }
    }
}

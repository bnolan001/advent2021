using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTwelve
{
    public class Cave
    {
        public string Name { get; private set; }

        public bool IsBig { get; private set; }

        public Dictionary<string, Cave> Caves { get; private set; } = new Dictionary<string, Cave>();

        public Cave(string name)
        {
            Name = name;
            IsBig = Name.Equals(Name.ToUpper());
        }

        public void LinkCave(Cave cave)
        {
            if (!Caves.ContainsKey(cave.Name))
            {
                Caves.Add(cave.Name, cave);
            }
        }
    }
}

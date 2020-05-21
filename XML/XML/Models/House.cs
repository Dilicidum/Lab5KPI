using System;
using System.Collections.Generic;
using System.Text;

namespace XML.Models
{
    public class House
    {
        public string TypeOfHouse { get; set; }
        public int NumberOfFloors { get; set; }
        public int NumberOfPorches { get; set; }
        public DateTime DateOfBuilded { get; set; }
        public string CodeOfHouse { get; set; }
        public virtual List<Area> Areas { get; set; }
    }
}

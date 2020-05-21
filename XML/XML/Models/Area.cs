using System;
using System.Collections.Generic;
using System.Text;

namespace XML.Models
{
    public class Area
    {
        public string Name { get; set; }
        public string AdressOfLocalAdministration { get; set; }
        public int AmountOfResidentals { get; set; }
        public double SquareOfArea { get; set; }
        public string Code { get; set; }
        public virtual List<House> Houses { get; set; }
    }
}

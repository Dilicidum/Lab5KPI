using System;
using System.Collections.Generic;
using System.Text;

namespace XML.Models
{
    public class ApplicationContext
    {
        public ApplicationContext()
        {
            Houses = new List<House>();
            Areas = new List<Area>();
            ListOfHouses = new List<ListOfHouse>();
            SeedData.EnsureData(this);

        }

        public List<House> Houses { get; set; }
        public List<Area> Areas { get; set; }
        public List<ListOfHouse> ListOfHouses { get; set; }



    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace XML.Models
{
    public static class SeedData
    {
        public static void EnsureData(ApplicationContext context)
        {
            List<House> Houses = new List<House>
        {
            new House {TypeOfHouse="High-rise",NumberOfFloors=15,CodeOfHouse="1",NumberOfPorches=3,DateOfBuilded = new DateTime(2015,7,29)},
            new House {TypeOfHouse="High-rise",NumberOfFloors=18,CodeOfHouse="2",NumberOfPorches=4,DateOfBuilded = new DateTime(2004,2,18)},
            new House {TypeOfHouse="Low-rise",NumberOfFloors=4,CodeOfHouse="3",NumberOfPorches=1,DateOfBuilded = new DateTime(1950,12,3)},
            new House {TypeOfHouse="Low-rise",NumberOfFloors=5,CodeOfHouse="4",NumberOfPorches=1,DateOfBuilded = new DateTime(1946,2,9)},
            new House {TypeOfHouse="High-rise",NumberOfFloors=20,CodeOfHouse="5",NumberOfPorches=4,DateOfBuilded = new DateTime(2019,1,4)},
            new House {TypeOfHouse="Low-rise",NumberOfFloors=4,CodeOfHouse="6",NumberOfPorches=1,DateOfBuilded = new DateTime(2020,1,13)},
            new House {TypeOfHouse="Medium-rise",NumberOfFloors=9,CodeOfHouse="7",NumberOfPorches=3,DateOfBuilded = new DateTime(1999,12,31)},
        };

            List<Area> Areas = new List<Area>
        {
            new Area{Name="Соломянский",AmountOfResidentals=10001,AdressOfLocalAdministration="ул. Хреновая дом 13",SquareOfArea=1016,Code="1"},
            new Area{Name="Шевченковский",AmountOfResidentals=12988,AdressOfLocalAdministration="ул. Паскудная дом 13",SquareOfArea=4912,Code="2"},
            new Area{Name="Святошинский",AmountOfResidentals=24121,AdressOfLocalAdministration="ул. Депрессивная дом 13",SquareOfArea=3761,Code="3"},
            new Area{Name="Печерский",AmountOfResidentals=1900,AdressOfLocalAdministration="ул. Буржуа дом 13",SquareOfArea=2950,Code="13"},
        };

            List<ListOfHouse> listOfHouses = new List<ListOfHouse>
        {
            new ListOfHouse{CodeOfHouse="1",CodeOfArea="2"},
            new ListOfHouse{CodeOfHouse="2",CodeOfArea="2"},
            new ListOfHouse{CodeOfHouse="3",CodeOfArea="1"},
            new ListOfHouse{CodeOfHouse="4",CodeOfArea="1"},
            new ListOfHouse{CodeOfHouse="5",CodeOfArea="3"},
            new ListOfHouse{CodeOfHouse="7",CodeOfArea="3"},
            new ListOfHouse{CodeOfHouse="6",CodeOfArea="13"},
        };

            context.Areas.AddRange(Areas);
            context.Houses.AddRange(Houses);
            context.ListOfHouses.AddRange(listOfHouses);
        }
    }
}

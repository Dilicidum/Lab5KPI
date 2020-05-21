using System;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using XML.Models;
using System.Text;
namespace XML
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationContext context = new ApplicationContext();

            Console.OutputEncoding = Encoding.UTF8;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create("Houses.xml", settings))
            {
                writer.WriteStartElement("Houses");
                foreach (var house in context.Houses)
                {
                    writer.WriteStartElement("house");
                    writer.WriteElementString("CodeOfHouse", house.CodeOfHouse);
                    writer.WriteElementString("DateOfBuilded", house.DateOfBuilded.ToString());

                    writer.WriteElementString("NumberOfFloors", house.NumberOfFloors.ToString());
                    writer.WriteElementString("NumberOfPorches", house.NumberOfPorches.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            using (XmlWriter writer = XmlWriter.Create("Areas.xml", settings))
            {
                writer.WriteStartElement("Areas");
                foreach (var area in context.Areas)
                {
                    writer.WriteStartElement("area");
                    writer.WriteElementString("AreaName", area.Name);
                    writer.WriteElementString("LocalAdministrationAdress", area.AdressOfLocalAdministration);
                    writer.WriteElementString("AreaCode", area.Code);
                    writer.WriteElementString("AmountOfResidentals", area.AmountOfResidentals.ToString());
                    writer.WriteElementString("AreaSquare", area.SquareOfArea.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            using (XmlWriter writer = XmlWriter.Create("HousesAreas.xml", settings))
            {
                writer.WriteStartElement("HousesAreas");
                foreach (var houseArea in context.ListOfHouses)
                {
                    writer.WriteStartElement("HouseArea");
                    writer.WriteElementString("CodeOfArea", houseArea.CodeOfArea);
                    writer.WriteElementString("CodeOfHouse", houseArea.CodeOfHouse);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            XDocument Houses = XDocument.Load("Houses.xml");
            XDocument Areas = XDocument.Load("Areas.xml");
            XDocument ListOfHouses = XDocument.Load("HousesAreas.xml");

            var result1 = from HL in context.ListOfHouses
                          join a in context.Areas on HL.CodeOfArea equals a.Code
                          join h in context.Houses on HL.CodeOfHouse equals h.CodeOfHouse
                          group h.NumberOfFloors by a.Name into g
                          select new { Key = g.Key, Count = g.Count() };


            //1.Вывести название всех районов
            {
                XDocument docum = XDocument.Load("Areas.xml");
                var result = from elem in docum.Element("Areas").Elements("area")
                             select elem.Element("AreaName").Value;
            }

            //2.Вывести перечень домов которые находятся в выбранном районе 
            {
                var result = from houseList in ListOfHouses.Element("HousesAreas").Elements("HouseArea")
                             from house in Houses.Element("Houses").Elements("house")
                             where house.Element("CodeOfHouse").Value ==
                                 houseList.Element("CodeOfHouse").Value
                             from area in Areas.Element("Areas").Elements("area")
                             where houseList.Element("CodeOfArea").Value ==
                                 area.Element("AreaCode").Value
                             where area.Element("AreaName").Value == "Соломянский"
                             select house.Element("CodeOfHouse").Value;
            }

            //3.Вывести дома, название районов которых начинается на С
            {
                var result = from houseList in ListOfHouses.Element("HousesAreas").Elements("HouseArea")
                             from house in Houses.Element("Houses").Elements("house")
                             where house.Element("CodeOfHouse").Value ==
                                 houseList.Element("CodeOfHouse").Value
                             from area in Areas.Element("Areas").Elements("area")
                             where houseList.Element("CodeOfArea").Value ==
                                 area.Element("AreaCode").Value
                             where area.Element("AreaName").Value.First() == 'С'
                             select house.Element("CodeOfHouse").Value;
            }

            //4.Sort Houses by date of building                          
            {
                var result = Houses.Element("Houses").Elements("house").OrderBy(c => DateTime.Parse(c.Element("DateOfBuilded").Value));
            }


            //5.Group Houses by number of floors
            {
                var result = Houses.Element("Houses").Elements("house")
                .GroupBy(c => Int32.Parse(c.Element("NumberOfFloors").Value))
                .Select(g => new { Name = g.Key, Count = g.Count() });
            }

            //6.Group Houses by number of floors and porches
            {
                var result = Houses.Element("Houses").Elements("house")
                .GroupBy(c => new
                {
                    NumberOfFloors = c.Element("NumberOfFloors").Value,
                    NumberOfPorches = c.Element("NumberOfPorches").Value
                })
                    .Select(g => new { Key = g.Key, Count = g.Count() });
            }
            //7.Group Houses by Areas
            {
                var result = from houseList in ListOfHouses.Element("HousesAreas").Elements("HouseArea")
                             from house in Houses.Element("Houses").Elements("house")
                             where house.Element("CodeOfHouse").Value ==
                                 houseList.Element("CodeOfHouse").Value
                             from area in Areas.Element("Areas").Elements("area")
                             where houseList.Element("CodeOfArea").Value ==
                                 area.Element("AreaCode").Value
                             group house by area.Element("AreaCode") into g
                             select new { Key = g.Key, Count = g.Key };
            }

            //8.Group Houses by year of building
            {
                var result = from house in Houses.Element("Houses").Elements("houses")
                             group house by DateTime.Parse(house.Element("DateOfBuilded").Value).Year into g
                             select new { Key = g.Key, Count = g.Count() };
            }

            //9.
            {
                var result = from houseList in ListOfHouses.Element("HousesAreas").Elements("HouseArea")
                             from house in Houses.Element("Houses").Elements("house")
                             where house.Element("CodeOfHouse").Value ==
                                 houseList.Element("CodeOfHouse").Value
                             from area in Areas.Element("Areas").Elements("area")
                             where houseList.Element("CodeOfArea").Value ==
                                 area.Element("AreaCode").Value
                             group house by area.Element("AreaName") into g
                             select new { Key = g.Key, Count = g.Count() };
            }

            //10.Show all Areas which contains house which has 4 floors
            {
                var result = from houseList in ListOfHouses.Element("HousesAreas").Elements("HouseArea")
                             from house in Houses.Element("Houses").Elements("house")
                             where house.Element("CodeOfHouse").Value ==
                                 houseList.Element("CodeOfHouse").Value
                             from area in Areas.Element("Areas").Elements("area")
                             where houseList.Element("CodeOfArea").Value ==
                                 area.Element("AreaCode").Value
                             where Int32.Parse(house.Element("NumberOfFloors").Value) == 4
                             select area.Element("AreaName").Value;
            }
        }
    }
   
}

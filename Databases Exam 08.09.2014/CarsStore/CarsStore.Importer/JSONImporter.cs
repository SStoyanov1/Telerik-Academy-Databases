namespace CarsStore.Importer
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;

    using Newtonsoft.Json;

    using CarsStore.Data;
    using CarsStore.Model;

    public class JSONImporter
    {
        public const string directoryJSONFilesPath = @"..\..\..\JSON Files";
        public const string PathToXml = @"..\..\..\Queries.xml";

        public static void Main()
        {
            CarsStoreDbContex dbContex = new CarsStoreDbContex();
            dbContex.Configuration.AutoDetectChangesEnabled = true;

            ImportFromJSONtoDataBase(dbContex);
            Search(dbContex, PathToXml);
        }

        public static void Search(CarsStoreDbContex dbContex, string pathToXml)
        {
            var result = new XElement("search-results");
            var xmlQueries = XElement.Load(pathToXml).Elements();

            foreach (var xmlQuery in xmlQueries)
            {
                var resultFilePath = xmlQuery.Attribute("OutputFileName").Value;

                if (xmlQuery.Element("OrderBy").Value != null)
                {
                    var orderByValue = xmlQuery.Element("OrderBy").Value;
                }

                var listOfWhereClauses = new List<WhereClause>();
                var element = xmlQuery.Element("WhereClauses");
                var list = element.Elements("WhereClause");
                foreach (var xmlQueryX in list)
                {
                    var whereClauseToAdd = new WhereClause();
                    if (xmlQueryX.Attribute("PropertyName") != null)
                    {
                        whereClauseToAdd.PropertyName = xmlQueryX.Attribute("PropertyName").Value;
                    }
                    else if (xmlQueryX.Attribute("Type") != null)
                    {
                        whereClauseToAdd.Type = xmlQueryX.Attribute("Type").Value;
                    }
                    else if (xmlQueryX.Value != null)
                    {
                        whereClauseToAdd.Value = xmlQueryX.Value;
                    }

                    listOfWhereClauses.Add(whereClauseToAdd);
                    string query = "select c.Model from Cars c ";

                    var carsss = dbContex.Database.SqlQuery<string>(query).FirstOrDefault<string>();
                }
            }
        }

        private static void ImportFromJSONtoDataBase(CarsStoreDbContex dbContex)
        {
            int count = 0;
            List<Car> carsToImport = new List<Car>();
            foreach (var file in Directory.GetFiles(directoryJSONFilesPath))
            {
                var cars = LoadJson(file.ToString());
                foreach (CarHelper car in cars)
                {
                    if (!dbContex.Cities.Any(c => c.Name == car.Dealer.City))
                    {
                        var cityToImport = new City
                        {
                            Name = car.Dealer.City
                        };
                        dbContex.Cities.Add(cityToImport);
                        dbContex.SaveChanges();
                    }

                    if (!dbContex.Dealers.Any(d => d.Name == car.Dealer.Name))
                    {
                        var dealerToImport = new Dealer
                        {
                            Name = car.Dealer.Name
                        };
                        dbContex.Dealers.Add(dealerToImport);
                        dbContex.SaveChanges();
                    }

                    dbContex.Dealers.FirstOrDefault(d => d.Name == car.Dealer.Name).Cities.Add(dbContex.Cities.FirstOrDefault(c => c.Name == car.Dealer.City));
                    if (!dbContex.Manufacturers.Any(m => m.Name == car.ManufacturerName))
                    {
                        var manufacturerToImport = new Manufacturer
                        {
                            Name = car.ManufacturerName
                        };
                        dbContex.Manufacturers.Add(manufacturerToImport);
                        dbContex.SaveChanges();
                    }

                    var carToImport = new Car
                    {
                        Year = car.Year,
                        TransmissionType = car.TransmissionType,
                        ManufacturerId = dbContex.Manufacturers.FirstOrDefault(m => m.Name == car.ManufacturerName).Id,
                        Model = car.Model,
                        Price = car.Price,
                        DealerId = dbContex.Dealers.FirstOrDefault(d => d.Name == car.Dealer.Name).Id
                    };
                    dbContex.Cars.Add(carToImport);
                    if (count % 200 == 0)
                    {
                        dbContex.Configuration.AutoDetectChangesEnabled = true;
                        dbContex.SaveChanges();
                        dbContex.Configuration.AutoDetectChangesEnabled = false;
                        Console.WriteLine("Cars added... {0} / 12000", count);
                    }

                    count++;
                }
            }
        }

        public static List<CarHelper> LoadJson(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                List<CarHelper> cars = JsonConvert.DeserializeObject<List<CarHelper>>(json);
                return cars;
            }
        }

        public class WhereClause
        {
            internal string PropertyName;
            internal string Type;
            internal string Value;
        }

        public class CarHelper
        {
            public int Year;
            public TransmissionType TransmissionType;
            public string ManufacturerName;
            public string Model;
            public decimal Price;
            public DealerHelper Dealer;
        }

        public class DealerHelper
        {
            public string Name;
            public string City;
        }
    }
}
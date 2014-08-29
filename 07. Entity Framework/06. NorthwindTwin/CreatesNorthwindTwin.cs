namespace _06.NorthwindTwin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using _01.NorthwindDbContex;
    using _02.DAO;

    public class CreatesNorthwindTwin
    {
        static void Main()
        {
            //06. Create a database called NorthwindTwin with the same structure as Northwind using the features from DbContext. Find for the API for schema generation in MSDN or in Google.

            //Just changing the database name in the app.congig file makes EF to create it if it does not exist by the model.
            NorthwindEntities northwindTwinDbContex = new NorthwindEntities();
            northwindTwinDbContex.Database.CreateIfNotExists();

            //07. Try to open two different data contexts and perform concurrent changes on the same records. What will happen at SaveChanges()? How to deal with it?

            NorthwindEntities secondNorthwindTwinDbContex = new NorthwindEntities();

            using (northwindTwinDbContex)
            {
                using (secondNorthwindTwinDbContex)
                {
                    DAO.InsertCustomer("MMM", "Microsoft");
                    DAO.UpdateCustomer("MMM", "Telerik");
            
                    Customer customer = northwindTwinDbContex.Customers.Find("MMM");
                    customer.ContactName = "Gosho";
            
                    Customer sameCustomer = secondNorthwindTwinDbContex.Customers.Find("MMM");
                    sameCustomer.ContactName = "Misho";
            
                    northwindTwinDbContex.SaveChanges();
                    secondNorthwindTwinDbContex.SaveChanges();
                }
            }
        }
    }
}

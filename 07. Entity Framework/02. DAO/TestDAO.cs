namespace _02.DAO
{
    using System;

    public class TestDAO
    {
        public static void Main()
        {
            //02. Create a DAO class with static methods which provide functionality for inserting, modifying and deleting customers. Write a testing class.

            DAO.InsertCustomer("PESHO", "Telerik Academy");
            DAO.UpdateCustomer("PESHO", "NESTLE");
            DAO.DeleteCustomer("PESHO");

            //03. Write a method that finds all customers who have orders made in 1997 and shipped to Canada.

            DAO.FindCustomersWithOrdersFrom1997ToCanade();

            //04. Implement previous by using native SQL query and executing it through the DbContext.

            DAO.FindCustomersWithOrdersFrom1997ToCanadeNativeSQL();

            //05. Write a method that finds all the sales by specified region and period (start / end dates).

            DAO.FindSalesByRegionAndPeriod("RJ", "1995-01-01", "1999-09-12");
        }
    }
}

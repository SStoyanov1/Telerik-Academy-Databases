//01. Write a program that retrieves from the Northwind sample database in MS SQL Server
//the number of  rows in the Categories table.

namespace _01.GetsNumberOfRowsInCategories
{
    using System;
    using System.Data.SqlClient;

    public class GetsNumberOfRowsInCategories
    {
        public static void Main()
        {
            SqlConnection dbCon = new SqlConnection("Server=.; " +
            "Database=Northwind; Integrated Security=true");
            dbCon.Open();

            using (dbCon)
            {
                SqlCommand cmdGetNumberCategories = new SqlCommand(
                    "SELECT COUNT(*) FROM Categories", dbCon);

                int categoriesNumber = (int) cmdGetNumberCategories.ExecuteScalar();

                Console.WriteLine("The number of categories is {0}.", categoriesNumber);
            }
        }
    }
}

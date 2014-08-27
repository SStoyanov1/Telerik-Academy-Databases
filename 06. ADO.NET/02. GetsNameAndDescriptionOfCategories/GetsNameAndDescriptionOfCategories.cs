//02. Write a program that retrieves the name and description of all categories in the Northwind DB.

namespace _02.GetsNameAndDescriptionOfCategories
{
    using System;
    using System.Data.SqlClient;

    public class GetsNameAndDescriptionOfCategories
    {
        public static void Main()
        {
            SqlConnection dbCon = new SqlConnection("Server=.; " +
            "Database=Northwind; Integrated Security=true");
            dbCon.Open();

            using (dbCon)
            {
                SqlCommand cmdGetNameAndDescrCategories = new SqlCommand(
                    "SELECT CategoryName, Description FROM Categories", dbCon);

                SqlDataReader reader = cmdGetNameAndDescrCategories.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine
                            ("Name: {0}; Descrpiption: {1}",
                            (string) reader["CategoryName"],
                            (string) reader["Description"]);
                    }
                }
            }
        }
    }
}

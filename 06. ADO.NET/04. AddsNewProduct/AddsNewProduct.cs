//04. Write a method that adds a new product in the products table in the Northwind database.
//Use a parameterized SQL command.

namespace _04.AddsNewProduct
{
    using System;
    using System.Data.SqlClient;

    public class AddsNewProduct
    {
        public static void Main()
        {
            string productName = "Derby";
            string supplierId = "2";
            string categoryId = "2";

            SqlConnection dbCon = new SqlConnection("Server=.; " +
            "Database=Northwind; Integrated Security=true");

            SqlCommand cmdInsertProduct = new SqlCommand(
                "INSERT INTO Products(ProductName, SupplierID, CategoryID) " +
                "VALUES (@name, @supplierid, @categoryid)", dbCon);
            cmdInsertProduct.Parameters.AddWithValue("@name", productName);
            cmdInsertProduct.Parameters.AddWithValue("@supplierid", supplierId);
            cmdInsertProduct.Parameters.AddWithValue("@categoryid", categoryId);

            dbCon.Open();

            using (dbCon)
            {
                cmdInsertProduct.ExecuteNonQuery();

                SqlCommand cmdSelectIdentity = new SqlCommand("SELECT @@Identity", dbCon);
                int insertedRecordId = (int)(decimal)cmdSelectIdentity.ExecuteScalar();
                Console.WriteLine("Product with name '{0}', SupplierID '{1}' and CategoryID '{2}' is inserted with ID '{3}'.",
                    productName, supplierId, categoryId, insertedRecordId);
            }
        }
    }
}

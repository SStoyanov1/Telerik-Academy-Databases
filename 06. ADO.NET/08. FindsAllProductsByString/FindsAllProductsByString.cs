//08. Write a program that reads a string from the console and finds all products that contain
//this string. Ensure you handle correctly characters like ', %, ", \ and _.

namespace _08.FindsAllProductsByString
{
    using System;
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;

    public class FindsAllProductsByString
    {
        public static void Main()
        {
            string inputString = Console.ReadLine();

            if (Regex.IsMatch(inputString, "[^0-9a-zA-Z]", RegexOptions.CultureInvariant))
            {
                inputString = "[" + inputString + "]";
            }

            SqlConnection dbCon = new SqlConnection("Server=.; " +
                "Database=Northwind; Integrated Security=true");

            SqlParameter inputParam = new SqlParameter();
            inputParam.ParameterName = "@inputParam";
            inputParam.Value = string.Format("%{0}%", inputString);

            SqlCommand cmdGetProductByString = new SqlCommand(
                "SELECT ProductName FROM Products " +
                "WHERE ProductName LIKE @inputParam", dbCon);

            cmdGetProductByString.Parameters.Add(inputParam);

            try
            {
                dbCon.Open();
                using (dbCon)
                {
                    SqlDataReader reader = cmdGetProductByString.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine((string)reader["ProductName"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

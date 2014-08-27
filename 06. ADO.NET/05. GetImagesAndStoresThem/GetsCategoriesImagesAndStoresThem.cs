//05. Write a program that retrieves the images for all categories in the Northwind database
//and stores them as JPG files in the file system.

namespace _05.GetsCategoriesImagesAndStoresThem
{
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;

    public class GetsCategoriesImagesAndStoresThem
    {
        private const string DB_CONNECTION_STRING = "Server=.; " +
            "Database=Northwind; Integrated Security=true";

        public static void Main()
        {
            byte[] imageFromDB;
            string categoryName;

            SqlConnection dbCon = new SqlConnection(DB_CONNECTION_STRING);
            dbCon.Open();

            using (dbCon)
            {
                SqlCommand cmdGetCategoriesImages = new SqlCommand(
                "SELECT CategoryName, Picture " +
                "FROM Categories", dbCon);
                SqlDataReader reader = cmdGetCategoriesImages.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        imageFromDB = (byte[])reader["Picture"];
                        categoryName = (string)reader["CategoryName"];
                        categoryName = categoryName.Replace("/", string.Empty);
                        WriteBinaryFile(@"..\..\" + categoryName + ".JPG", imageFromDB);
                    }
                }
            }
        }

        private static void WriteBinaryFile(string fileName, byte[] fileContents)
        {
            FileStream stream = File.OpenWrite(fileName);
            using (stream)
            {
                stream.Write(fileContents, 78, fileContents.Length - 78);
            }
        }
    }
}

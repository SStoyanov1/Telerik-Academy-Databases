//10. Re-implement the previous task with SQLite embedded DB (see http://sqlite.phxsoftware.com).

namespace _10.SQLiteBookDBOperations
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data.SQLite;

    public class SQLiteDBOperations
    {
        static void Main(string[] args)
        {
            var sQLiteCon = new SQLiteConnection("Data Source=..\\..\\..\\books-database.s3db;Version=3;");
            //AddBooks("Imperial Bedrooms", "2345", sQLiteCon);
            ListAllBooks(sQLiteCon);

            string titleToFind = "Lord";
            FindBookByName(sQLiteCon, titleToFind);
        }

        private static void FindBookByName(SQLiteConnection findBookCon, string title)
        {
            findBookCon.Open();

            string findBookSql = "SELECT BookId, Title, ISBN FROM Books WHERE Title LIKE @bookName";

            SQLiteCommand cmdFindBook = new SQLiteCommand(findBookSql, findBookCon);
            cmdFindBook.Parameters.AddWithValue("@bookName", "%" + title + "%" as string);

            SQLiteDataReader reader = cmdFindBook.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    Console.WriteLine("{0} | {1} | {2}", reader[0], reader[1], reader[2]);
                }
            }
            findBookCon.Close();
        }

        private static void AddBooks(string title, string isbn, SQLiteConnection addCon)
        {
            addCon.Open();
            string sql = "insert into Books (Title, ISBN) values (@title, @isbn)";
            SQLiteCommand command = new SQLiteCommand(sql, addCon);
            command.Parameters.AddWithValue("@title", title as string);
            command.Parameters.AddWithValue("@isbn", isbn as string);
            command.ExecuteNonQuery();
            addCon.Close();
        }

        private static void ListAllBooks(SQLiteConnection listBooksCon)
        {
            listBooksCon.Open();

            string listBooksSql = "SELECT BookId, Title, ISBN FROM Books";

            SQLiteCommand cmdListBooks = new SQLiteCommand(listBooksSql, listBooksCon);

            SQLiteDataReader reader = cmdListBooks.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    Console.WriteLine("{0} | {1} | {2}", reader[0], reader[1], reader[2]);
                }
            }
            listBooksCon.Close();
        }
    }
}

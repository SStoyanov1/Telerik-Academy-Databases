//07. Implement appending new rows to the Excel file.

namespace _07.AppendNewRowExcel
{
    using System;
    using System.Data;
    using System.Data.OleDb;

    public class AppendNewRowExcel
    {
        static void Main()
        {
            Console.WriteLine("Name to be inserted:");
            string name = Console.ReadLine();
            Console.WriteLine("Score to be inserted for {0}:", name);
            string score = Console.ReadLine();

            string fileName = "UserDB.xlsx";

            OleDbConnectionStringBuilder connectionBuilder = new OleDbConnectionStringBuilder();
            connectionBuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
            connectionBuilder.DataSource = @"..\..\..\" + fileName;
            connectionBuilder.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES");

            OleDbConnection connectionExcel = new OleDbConnection(connectionBuilder.ConnectionString);

            OleDbCommand cmdAppendRow = new OleDbCommand(
                "INSERT INTO [UserDB$](Name, Score) VALUES('" + name + "','" + score + "')",
                connectionExcel);

            try
            {
                connectionExcel.Open();

                using (connectionExcel)
                {
                    int rowsAffected = cmdAppendRow.ExecuteNonQuery();
                    Console.WriteLine("{0} rows affected", rowsAffected);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

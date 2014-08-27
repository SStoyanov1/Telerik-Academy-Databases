//06. Write a program that reads your MS Excel file through the OLE DB data provider
//and displays the name and score row by row.

namespace _06.ReadsXlsxAndDisplays
{
    using System;
    using System.Data;
    using System.Data.OleDb;

    public class ReadsXlsxAndDisplays
    {
        public static void Main()
        {
            string fileName = "UserDB.xlsx";

            OleDbConnectionStringBuilder connectionBuilder = new OleDbConnectionStringBuilder();
            connectionBuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
            connectionBuilder.DataSource = @"..\..\..\" + fileName;
            connectionBuilder.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES");
            OleDbConnection connectionExcel = new OleDbConnection(connectionBuilder.ConnectionString);
            OleDbCommand cmdGetNamesAndScores = new OleDbCommand("SELECT * FROM [UserDB$]", connectionExcel);
            connectionExcel.Open();
            using (connectionExcel)
            {
                OleDbDataReader reader = cmdGetNamesAndScores.ExecuteReader();

                while (reader.Read())
                {
                    string name = (string)reader["Name"];
                    double score = (double)reader["Score"];
                    Console.WriteLine("{0} -> {1} points", name, score);
                }
            }
        }
    }
}

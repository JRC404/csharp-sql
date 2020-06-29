using System;
using System.Data.SqlClient;

namespace SQLtemplate
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "localhost,1401";
                builder.UserID = "sa";
                builder.Password = "YourStrong!Passw0rd";
                builder.InitialCatalog = "CodeNation"; // database name you want to connect to
                // without the builder, it would look like the next line:
                // “DataSource=localhost,1401;UserID=sa;Password=YourStrong!Passw0rd;Initial Catalog=Codenation;”

                // what would you like to send to SQL?
                //string SELECT = "SELECT * FROM Students"; // select everything from the table 'students' from 'CodeNation'
                string INSERT; // warning will be present
                string UPDATE; // warning will be present
                string DELETE = "DELETE FROM Students WHERE ID=17"; // warning will be present

                // Writing to the console BEFORE the connection is attempted... 
                Console.WriteLine("Connecting to SQL server, let's see if it works. Fingers crossed, Harry.");

                // creating a new instance of the SqlConnection to actually connect to our DB. 
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                using (connection) // may be modifying this...
                {
                    SqlCommand cmd = new SqlCommand(DELETE, connection);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            for(int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.WriteLine(reader.GetValue(i));
                            }
                        }
                    }

                    Console.WriteLine("Done.");
                }
            }
            catch (SqlException error)
            {
                Console.WriteLine(error.ToString());
            }

            Console.Write("All done. All sorted. I'm finished. I'm going home.");
            Console.ReadKey();

        }
    }
}

/*
 * SQL to C# - it takes a few things to connect
 * 1. DataSource = localhost... if you're on Windows, it is simply localhost
 *      If you're using docker, it's not simply localhost... 
 *      For now, MY docker container connects to 1401... yours may be different.
 *      Making my DataSource = localhost,1401
 * 2. UserID = "sa"
 * 3. Password = "YourStrong!Passw0rd"
 * 4. Initial Catalog = "CodeNation"
 */

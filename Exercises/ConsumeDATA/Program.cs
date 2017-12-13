using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Win32;

namespace ConsumeDATA
{
    //ADO.NET
    //connected , sql , connection,command datareader
    //disconnect dataset/datatable,dataadapter
    //program=>provider=>db

    //orm impedance mismatch
    class Program
    {
        static string connectionstring = "";

        static void Main(string[] args)
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            Connect(connection);
            Console.WriteLine("fine");
            Console.ReadLine();
        }
        //ADO net appling connection pooling
        static void Connect(string connectionstring)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionstring))
                    {
                        User user = new User();
                        string query = @"select * from utenti";
                        SqlCommand command = new SqlCommand(query);
                        connection.Open();
                        command.Connection = connection;
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["nome"]);
                        }

                        while (reader.HasRows)
                        {
                            Console.WriteLine("\t{0}\t{1}", reader.GetName(0),
                                reader.GetName(1));

                            while (reader.Read())
                            {
                                Console.WriteLine("\t{0}\t{1}", reader.GetInt32(0),
                                    reader.GetString(1));
                            }
                            reader.NextResult();
                        }
                        reader.Close();
                        string queryInsert = @"insert into  utenti (nome) values (@nome)";
                        SqlCommand command1 = new SqlCommand(queryInsert);
                        command1.Parameters.AddWithValue("@nome", "stefano");
                        command1.Connection = connection;
                        command1.ExecuteNonQuery();
                        throw new Exception();
                        string queryInsert1 = @"insert into  utenti (nome) values (@nome)";
                        SqlCommand command2 = new SqlCommand(queryInsert1);
                        command2.Parameters.AddWithValue("@nome", "mimmo");
                        command2.Connection = connection;
                        command2.ExecuteNonQuery();
                    }
                    scope.Complete();//commit
                }
                catch (Exception ex)
                {
                    //implicity roll back
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    public class User
    {
        public string name;
    }
}

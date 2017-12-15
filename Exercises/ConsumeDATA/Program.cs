using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
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

        static void Main(string[] args)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            
            //Connect(connectionString);

            Connect1(connectionString);

            //ConnectOrm();
          

            //WCF

            //XML AND JSON


            Console.WriteLine("fine");
            Console.ReadLine();
        }

        private static void ConnectOrm()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    //mi crea un db ConsumeDATA.dbrobContext (progetto.nomeClasseContext
                    //e una tabella Users (plurale)
                    using (dbrobContext ctx = new dbrobContext())
                    {
                        ctx.User.Add(new User() { Name = "Rob" });
                        ctx.SaveChanges();
                        scope.Complete();//commit
                    }
                }
                catch (Exception ex)
                {
                    //implicity rollback
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void Connect1(string connectionString)
        {
            using (SqlConnection connection1 = new SqlConnection(connectionString))
            {
                connection1.Open();
                SqlTransaction transaction = connection1.BeginTransaction();

                try
                {
                    string queryInsert1 = @"insert into  Users (Name) values (@nome)";
                    SqlCommand command = new SqlCommand(queryInsert1);
                    command.Connection = connection1;
                    command.Parameters.AddWithValue("@nome", "test2");
                    command.Transaction = transaction;
                    command.ExecuteNonQuery();
                    throw new Exception();
                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
                finally
                {
                    connection1.Close();
                }
            }
        }

        //ADO net appling connection pooling
        static void Connect(string connectionstring)
        {
            //GUIDA TRANSACTION
            //https://msdn.microsoft.com/en-us/library/ms973865.aspx
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
                            user.Name = reader["nome"].ToString();
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
                        string queryInsert1 = @"insert into  utenti (nome) values (@nome)";
                        SqlCommand command2 = new SqlCommand(queryInsert1);
                        command2.Parameters.AddWithValue("@nome", "mimmo");
                        command2.Connection = connection;
                        command2.ExecuteNonQuery();
                        scope.Complete();//commit
                    }
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
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class dbrobContext : DbContext
    {
        public dbrobContext()
        : base("name=MyDB")
        {
        }

        public IDbSet<User> User { get; set; }
    }



}

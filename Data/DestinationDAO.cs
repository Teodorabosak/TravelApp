using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TravelApp.Models;

namespace TravelApp.Data
{
    internal class DestinationDAO
    {
        private string connectionString = @"Data Source=DESKTOP-COA51EH;Initial Catalog=Destination;Integrated Security=True";
        public List<Destination> FetchAll()
        {
            List<Destination> returnList = new List<Destination>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * from [dbo].[DestinationData]";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Destination destination = new Destination();
                        destination.Id = reader.GetInt32(0);
                        destination.Name = reader.GetString(1);
                        destination.Description = reader.GetString(2);
                        destination.DateGo = reader.GetDateTime(3);
                        destination.DateBack = reader.GetDateTime(4);
                        destination.Price = reader.GetInt32(5);

                        returnList.Add(destination);
                    }
                }
            }

            return returnList;

        }

        internal int Delete(int id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "";



               
                    sqlQuery = "DELETE FROM dbo.DestinationData WHERE Id = @Id ";
         


                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Id", System.Data.SqlDbType.Int, 1000).Value = id;
               
                connection.Open();

                int deletedId = command.ExecuteNonQuery();


                return deletedId;
            }
        }
    

        public Destination FetchOne(int id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * from [dbo].[DestinationData] WHERE Id = @id";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Destination destination = new Destination();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        destination.Id = reader.GetInt32(0);
                        destination.Name = reader.GetString(1);
                        destination.Description = reader.GetString(2);
                        destination.DateGo = reader.GetDateTime(3);
                        destination.DateBack = reader.GetDateTime(4);
                        destination.Price = reader.GetInt32(5);


                    }
                }
                return destination;
            }
        }

            public int CreateOrUpdate(Destination newDestination)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                string sqlQuery = "";



                if (newDestination.Id < 0)
                {
                    sqlQuery = "INSERT INTO dbo.DestinationData Values(@Name, @Description, @DateGo, @DateBack, @Price)";
                }
                else
                {
                    sqlQuery = "UPDATE dbo.DestinationData SET Name = @Name, Description = @Description, DateGo = @DateGo, DateBack = @DateBack, Price = @Price WHERE Id = @Id";
                }
            
                
                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    command.Parameters.Add("@Id", System.Data.SqlDbType.Int, 1000).Value = newDestination.Id;
                    command.Parameters.Add("@Name", System.Data.SqlDbType.VarChar, 1000).Value = newDestination.Name;
                    command.Parameters.Add("@Description", System.Data.SqlDbType.VarChar, 1000).Value = newDestination.Description;
                    command.Parameters.Add("@DateGo", System.Data.SqlDbType.DateTime, 1000).Value = newDestination.DateGo;
                    command.Parameters.Add("@DateBack", System.Data.SqlDbType.DateTime, 1000).Value = newDestination.DateBack;
                    command.Parameters.Add("@Price", System.Data.SqlDbType.Int, 1000).Value = newDestination.Price;


                connection.Open();

                    int newId = command.ExecuteNonQuery();


                    return newId;
                }
            }
        }
    }

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TravelApp.Models;


namespace TravelApp.Data
{
    internal class ClientsDAO
    {
        private string connectionString = @"Data Source=DESKTOP-COA51EH;Initial Catalog=Destination;Integrated Security=True";

        public List<Clients> FetchAll()
        {
            List<Clients> returnList = new List<Clients>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * from [dbo].[Clients]";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Clients client = new Clients();
                        client.ClientID = reader.GetInt32(0);
                        client.FirstName = reader.GetString(1);
                        client.LastName = reader.GetString(2);
                        client.Email = reader.GetString(3);
                        client.Phone = reader.GetString(4);
                        client.Username = reader.GetString(5);
                        client.Password = reader.GetString(6);

                        returnList.Add(client);
                    }
                }
            }

            return returnList;
        }

        public Clients FetchOne(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM [dbo].[Clients] WHERE ClientID = @id";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Ispravite ime parametra ovde na @id
                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Clients client = new Clients();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        client.ClientID = reader.GetInt32(0);
                        client.FirstName = reader.GetString(1);
                        client.LastName = reader.GetString(2);
                        client.Email = reader.GetString(3);
                        client.Phone = reader.GetString(4);
                        client.Username = reader.GetString(5);
                        client.Password = reader.GetString(6);
                    }
                }

                return client;
            }
        }

        public int CreateOrUpdate(Clients newClient)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "";

                if (newClient.ClientID < 0)
                {
                    // INSERT
                    sqlQuery = "INSERT INTO dbo.Clients (FirstName, LastName, Email, Phone, Password, Username) VALUES (@FirstName, @LastName, @Email, @Phone, @Password, @Username)";
                }
                else
                {
                    // UPDATE
                    sqlQuery = "UPDATE dbo.Clients SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone";

                    if (!string.IsNullOrEmpty(newClient.Username))
                    {
                        // Dodajte @Username u UPDATE samo ako postoji vrednost za Username
                        sqlQuery += ", Username = @Username";
                    }

                    sqlQuery += " WHERE ClientID = @ClientID";
                }

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@ClientID", System.Data.SqlDbType.Int).Value = newClient.ClientID;
                command.Parameters.Add("@FirstName", System.Data.SqlDbType.VarChar, 1000).Value = newClient.FirstName;
                command.Parameters.Add("@LastName", System.Data.SqlDbType.VarChar, 1000).Value = newClient.LastName;
                command.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, 1000).Value = newClient.Email;
                command.Parameters.Add("@Phone", System.Data.SqlDbType.VarChar, 1000).Value = newClient.Phone;

                // Dodajte @Username samo ako postoji vrednost za Username
                if (!string.IsNullOrEmpty(newClient.Username))
                {
                    command.Parameters.Add("@Username", System.Data.SqlDbType.VarChar, 1000).Value = newClient.Username;
                }

                command.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 1000).Value = newClient.Password;

                connection.Open();

                int affectedRows = command.ExecuteNonQuery();

                return affectedRows;
            }
        }

        public int Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "DELETE FROM dbo.Clients WHERE ClientID = @ClientID";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@ClientID", System.Data.SqlDbType.Int, 1000).Value = id;

                connection.Open();

                int deletedId = command.ExecuteNonQuery();

                return deletedId;
            }
        }
    }
}

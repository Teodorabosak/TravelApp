using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TravelApp.Models;

namespace TravelApp.Data
{
    internal class ReservationDAO
    {
        private string connectionString = @"Data Source=DESKTOP-COA51EH;Initial Catalog=Destination;Integrated Security=True";

        public List<Reservation> FetchAll()
        {
            List<Reservation> returnList = new List<Reservation>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * from [dbo].[Reservation]";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Reservation reservation = new Reservation();
                        reservation.ReservationID = reader.GetInt32(0);
                        reservation.ReservationDate = reader.GetDateTime(1);
                        reservation.ClientID = reader.GetInt32(2);
                        reservation.DestinationID = reader.GetInt32(3);

                        returnList.Add(reservation);
                    }
                }
            }

            return returnList;
        }

        public Reservation FetchOne(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * from [dbo].[Reservation] WHERE ReservationID = @id";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Reservation reservation = new Reservation();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reservation.ReservationID = reader.GetInt32(0);
                        reservation.ReservationDate = reader.GetDateTime(1);
                        reservation.ClientID = reader.GetInt32(2);
                        reservation.DestinationID = reader.GetInt32(3);
                    }
                }

                return reservation;
            }
        }

        public int CreateOrUpdate(Reservation newReservation)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "";

                if (newReservation.ReservationID < 0)
                {
                    sqlQuery = "INSERT INTO dbo.Reservation (ReservationDate, ClientID, DestinationID) VALUES (@ReservationDate, @ClientID, @DestinationID)";
                }
                else
                {
                    sqlQuery = "UPDATE dbo.Reservation SET ReservationDate = @ReservationDate, ClientID = @ClientID, DestinationID = @DestinationID WHERE ReservationID = @ReservationID";
                }

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@ReservationID", System.Data.SqlDbType.Int, 1000).Value = newReservation.ReservationID;
                command.Parameters.Add("@ReservationDate", System.Data.SqlDbType.DateTime, 1000).Value = newReservation.ReservationDate;
                command.Parameters.Add("@ClientID", System.Data.SqlDbType.Int, 1000).Value = newReservation.ClientID;
                command.Parameters.Add("@DestinationID", System.Data.SqlDbType.Int, 1000).Value = newReservation.DestinationID;

                connection.Open();

                int newId = command.ExecuteNonQuery();

                return newId;
            }
        }

        public int Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "DELETE FROM dbo.Reservation WHERE ReservationID = @ReservationID";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@ReservationID", System.Data.SqlDbType.Int, 1000).Value = id;

                connection.Open();

                int deletedId = command.ExecuteNonQuery();

                return deletedId;
            }
        }
    }
}
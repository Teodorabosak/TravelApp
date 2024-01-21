using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TravelApp.Models;

namespace TravelApp.Data
{
    internal class ReviewDAO
    {
        private string connectionString = @"Data Source=DESKTOP-COA51EH;Initial Catalog=Destination;Integrated Security=True";

        public List<Reviews> FetchAll()
        {
            List<Reviews> returnList = new List<Reviews>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * from [dbo].[Reviews]";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Reviews review = new Reviews();
                        review.ReviewID = reader.GetInt32(0);
                        review.ClientID = reader.GetInt32(1);
                        review.DestinationID = reader.GetInt32(2);
                        review.Rating = reader.GetString(3);
                        review.Comment = reader.GetString(4);
                        review.Date = reader.GetDateTime(5);

                        returnList.Add(review);
                    }
                }
            }

            return returnList;
        }

        public Reviews FetchOne(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * from [dbo].[Reviews] WHERE ReviewID = @id";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Reviews review = new Reviews();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        review.ReviewID = reader.GetInt32(0);
                        review.ClientID = reader.GetInt32(1);
                        review.DestinationID = reader.GetInt32(2);
                        review.Rating = reader.GetString(3);
                        review.Comment = reader.GetString(4);
                        review.Date = reader.GetDateTime(5);
                    }
                }

                return review;
            }
        }

        public int CreateOrUpdate(Reviews newReview)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "";

                if (newReview.ReviewID < 0)
                {
                    sqlQuery = "INSERT INTO dbo.Reviews (ClientID, DestinationID, Rating, Comment, Date) VALUES (@ClientID, @DestinationID, @Rating, @Comment, @Date)";
                }
                else
                {
                    sqlQuery = "UPDATE dbo.Reviews SET ClientID = @ClientID, DestinationID = @DestinationID, Rating = @Rating, Comment = @Comment, Date = @Date WHERE ReviewID = @ReviewID";
                }

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@ReviewID", System.Data.SqlDbType.Int, 1000).Value = newReview.ReviewID;
                command.Parameters.Add("@ClientID", System.Data.SqlDbType.Int, 1000).Value = newReview.ClientID;
                command.Parameters.Add("@DestinationID", System.Data.SqlDbType.Int, 1000).Value = newReview.DestinationID;
                command.Parameters.Add("@Rating", System.Data.SqlDbType.VarChar, 1000).Value = newReview.Rating;
                command.Parameters.Add("@Comment", System.Data.SqlDbType.VarChar, 1000).Value = newReview.Comment;
                command.Parameters.Add("@Date", System.Data.SqlDbType.DateTime, 1000).Value = newReview.Date;

                connection.Open();

                int newId = command.ExecuteNonQuery();

                return newId;
            }
        }

        public int Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "DELETE FROM dbo.Reviews WHERE ReviewID = @ReviewID";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@ReviewID", System.Data.SqlDbType.Int, 1000).Value = id;

                connection.Open();

                int deletedId = command.ExecuteNonQuery();

                return deletedId;
            }
        }
    }
}

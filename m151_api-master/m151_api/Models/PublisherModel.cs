using m151_api.Classes;
using m151_api.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace m151_api.Models
{
    public class PublisherModel
    {
        private DBHelper dbHelper;

        public PublisherModel()
        {
            dbHelper = DBHelper.Instance;
        }

        public Publisher getPublisher(string name)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_publisher '" + name + "'";
            cmd.Connection = dbHelper.connection;
            Publisher publisher;
            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();
                publisher = mapPublisher(reader);
            }
            return publisher;
        }

        public List<Publisher> getPublishers()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_publishers";
            cmd.Connection = dbHelper.connection;
            List<Publisher> publishers = new List<Publisher>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    publishers.Add(mapPublisher(reader));
                }
            }
            return publishers;
        }

        public Publisher mapPublisher(SqlDataReader reader)
        {
            return new Publisher()
            {
                Name = Convert.ToString(reader["Name"]),
                Location = Convert.ToString(reader["Ort"]),
                Zip = Convert.ToString(reader["Postleitzahl"]),
                Street = Convert.ToString(reader["Strasse"]),
                StreetNumber = Convert.ToString(reader["Hausnummer"])
            };
        }

        public void setPublisher(Publisher publisher)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC set_publisher '" + publisher.Name + "', '" + publisher.Location + "', '" + publisher.Zip + "', '" + publisher.Street + "', '" + publisher.StreetNumber + "'";
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }

        public Publisher newPublisher(Publisher publisher)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC new_publisher '" + publisher.Name + "', '" + publisher.Location + "', '" + publisher.Zip + "', '" + publisher.Street + "', '" + publisher.StreetNumber + "'";
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
            return publisher;
        }

        public void deletePublisher(Publisher publisher)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC delete_publisher '" + publisher.Name + "'";
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }
    }
}
using m151_api.Classes;
using m151_api.Entities;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace m151_api.Models
{
    public class OrderModel
    {
        private DBHelper dbHelper;
        private PublisherModel publisherModel;

        public OrderModel()
        {
            dbHelper = DBHelper.Instance;
            publisherModel = new PublisherModel();
        }

        public Order getOrder(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_order " + id;
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Order order = mapOrder(reader);
            reader.Close();
            return order;
        }

        public List<Order> getOrders()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_orders";
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            List<Order> orders = new List<Order>();
            while (reader.Read())
            {
                orders.Add(mapOrder(reader));
            }
            reader.Close();
            return orders;
        }

        public Order mapOrder(SqlDataReader reader)
        {
            return new Order()
            {
                OrderNumber = Convert.ToInt32(reader["Bestellnummer"]),
                Publisher = publisherModel.getPublisher(Convert.ToString(reader["Verlag"]))
            };
        }

        public void setOrder(Order order)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC set_order " + order.OrderNumber + ", '" + order.Publisher.Name + "'";
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }

        public Order newOrder(Order order)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC set_order '" + order.Publisher.Name + "'";
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            order.OrderNumber = Convert.ToInt32(reader["Bestellnummer"]);
            reader.Close();
            return order;
        }

        public void deleteOrder(Order order)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC delete_order " + order.OrderNumber;
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }
    }
}
using m151_api.Classes;
using m151_api.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace m151_api.Models
{
    public class CustomerModel
    {
        private DBHelper dbHelper;

        public CustomerModel()
        {
            dbHelper = DBHelper.Instance;
        }

        public Customer getCustomer(string email)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_customer " + email;
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Customer customer = mapCustomer(reader);
            reader.Close();
            return customer;
        }

        public List<Customer> getCustomers()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_customers";
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            List<Customer> customers = new List<Customer>();
            while (reader.Read())
            {
                customers.Add(mapCustomer(reader));
            }
            reader.Close();
            return customers;
        }

        public Customer mapCustomer(SqlDataReader reader)
        {
            return new Customer()
            {
                Email = Convert.ToString(reader["Email"]),
                Number = Convert.ToInt32(reader["Kundennummer"]),
                Surname = Convert.ToString(reader["Name"]),
                Prename = Convert.ToString(reader["Vorname"]),
                PhoneNumber = Convert.ToString(reader["Telefonnummer"]),
                City = Convert.ToString(reader["Ort"]),
                ZipCode = Convert.ToString(reader["Postleitzahl"]),
                Street = Convert.ToString(reader["Strasse"]),
                StreetNumber = Convert.ToString(reader["Hausnummer"]),
                IsPremiumMember = Convert.ToBoolean(reader["Mitgliederstatus"])
            };
        }

        public void setCustomer(Customer customer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC set_customer '" + customer.Email + "', " + customer.Number + ", '" + customer.Surname + "', '" + customer.Prename + "', '" + customer.PhoneNumber + "', '" + customer.City + "', '" + customer.ZipCode + "', '" + customer.Street + "', '" + customer.StreetNumber + "', " + customer.IsPremiumMember;
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }

        public Customer newCustomer(Customer customer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC set_customer '" + customer.Email + "', " + customer.Number + ", '" + customer.Surname + "', '" + customer.Prename + "', '" + customer.PhoneNumber + "', '" + customer.City + "', '" + customer.ZipCode + "', '" + customer.Street + "', '" + customer.StreetNumber + "', " + customer.IsPremiumMember;
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
            return customer;
        }

        public void deleteCustomer(Customer customer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC set_customer '" + customer.Email + "'";
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }
    }
}
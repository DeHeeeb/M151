using m151_api.Classes;
using m151_api.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace m151_api.Models
{
    public class UserModel
    {
        private DBHelper dbHelper;
        private CustomerModel customerModel;
        private EmployeeModel employeeModel;
        private ViceModel viceModel;
        private OrganisationLeaderModel organisationLeaderModel;

        public UserModel()
        {
            dbHelper = DBHelper.Instance;
            customerModel = new CustomerModel();
            employeeModel = new EmployeeModel();
            viceModel = new ViceModel();
            organisationLeaderModel = new OrganisationLeaderModel();
        }

        public User getUser(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_user " + id;
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            User user = mapUser(reader);
            reader.Close();
            return user;
        }

        public List<User> getUsers()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_users";
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            List<User> users = new List<User>();
            while (reader.Read())
            {
                users.Add(mapUser(reader));
            }
            reader.Close();
            return users;
        }

        public User mapUser(SqlDataReader reader)
        {
            return new User()
            {
                Id = Convert.ToInt32(reader["ID"]),
                Email = Convert.ToString(reader["Email"]),
                Password = Convert.ToString(reader["Passwort"]),
                Customer = customerModel.getCustomer(Convert.ToString(reader["Kunde"])),
                Employee = employeeModel.getEmployee(Convert.ToInt32(reader["Mitarbeiter"])),
                Vice = viceModel.getVice(Convert.ToInt32(reader["Stellvertreter"])),
                OrganisationLeader = organisationLeaderModel.getOrganisationLeader(Convert.ToInt32(reader["Filialleiter"]))
            };
        }

        public void setUser(User user)
        {
            string customerEmail = "null";
            int employeeId = 0;
            int viceId = 0;
            int organisationLeaderId = 0;
            if (user.Customer != null)
                customerEmail = user.Customer.Email;
            if (user.Employee != null)
                employeeId = user.Employee.Id;
            if (user.Vice != null)
                viceId = user.Vice.Id;
            if (user.OrganisationLeader != null)
                organisationLeaderId = user.OrganisationLeader.Id;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC set_user " + user.Id + ", '" + user.Email + "', '" + user.Password + "', '" + customerEmail + "', " + employeeId + ", " + viceId + ", " + organisationLeaderId;
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }

        public User newUser(User user)
        {
            string customerEmail = "null";
            int employeeId = 0;
            int viceId = 0;
            int organisationLeaderId = 0;
            if (user.Customer != null)
                customerEmail = user.Customer.Email;
            if (user.Employee != null)
                employeeId = user.Employee.Id;
            if (user.Vice != null)
                viceId = user.Vice.Id;
            if (user.OrganisationLeader != null)
                organisationLeaderId = user.OrganisationLeader.Id;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC new_user '" + user.Email + "', '" + user.Password + "', '" + customerEmail + "', " + employeeId + ", " + viceId + ", " + organisationLeaderId;
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            user.Id = Convert.ToInt32(reader["ID"]);
            reader.Close();
            return user;
        }

        public void deleteUser(User user)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC delete_user " + user.Id;
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }
    }
}
using m151_api.Classes;
using m151_api.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace m151_api.Models
{
    public class EmployeeModel
    {
        private DBHelper dbHelper;
        private OrganisationModel organisationModel;

        public EmployeeModel()
        {
            dbHelper = DBHelper.Instance;
            organisationModel = new OrganisationModel();
        }

        public Employee getEmployee(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_employee " + id;
            cmd.Connection = dbHelper.connection;
            Dictionary<string, string>employeeData;
            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();
                employeeData = ExtractInformation(reader);
            }
        
            return mapEmployee(employeeData);
        }

        public List<Employee> getEmployees()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_employees";
            cmd.Connection = dbHelper.connection;
            List<Employee> employees = new List<Employee>();
            var employeesData = new List<Dictionary<string, string>>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    employeesData.Add(ExtractInformation(reader));
                }
            }
            foreach (var employeeData in employeesData)
            {
                employees.Add(mapEmployee(employeeData));
            }
            return employees;
        }
        public Dictionary<string, string> ExtractInformation(SqlDataReader reader)
        {
            return new Dictionary<string, string>()
            {
                ["ID"] = Convert.ToString(reader["ID"]),
                ["Mitarbeiternummer"] = Convert.ToString(reader["Mitarbeiternummer"]),
                ["Standort"] = Convert.ToString(reader["Standort"]),
                ["Name"] = Convert.ToString(reader["Name"]),
                ["Vorname"] = Convert.ToString(reader["Vorname"])
            };
        }

        public Employee mapEmployee(Dictionary<string, string> reader)
        {
            return new Employee
            {
                Id = Convert.ToInt32(reader["ID"]),
                Number = Convert.ToInt32(reader["Mitarbeiternummer"]),
                Organisation = organisationModel.getOrganisation(Convert.ToInt32(reader["Standort"])),
                Surname = Convert.ToString(reader["Name"]),
                Prename = Convert.ToString(reader["Vorname"])
            };
        }

        public void setEmployee(Employee employee)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC set_employee " + employee.Id + ", " + employee.Number + ", " + employee.Organisation.Id + ", '" + employee.Surname + "', '" + employee.Prename + "'";
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }

        public Employee newEmployee(Employee employee)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC set_employee " + employee.Number + ", " + employee.Organisation.Id + ", '" + employee.Surname + "', '" + employee.Prename + "'";
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            employee.Id = Convert.ToInt32(reader["Id"]);
            reader.Close();
            return employee;
        }

        public void deleteEmployee(Employee employee)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC delete_employee " + employee.Id;
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }
    }
}
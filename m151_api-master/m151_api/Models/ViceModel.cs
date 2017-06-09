using m151_api.Classes;
using m151_api.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace m151_api.Models
{
    public class ViceModel
    {
        private DBHelper dbHelper;
        private OrganisationModel organisationModel;

        public ViceModel()
        {
            dbHelper = DBHelper.Instance;
            organisationModel = new OrganisationModel();
        }

        public Vice getVice(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_vice " + id;
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Vice vice = mapVice(reader);
            reader.Close();
            return vice;
        }

        public List<Vice> getVices()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_vices";
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            List<Vice> vices = new List<Vice>();
            while (reader.Read())
            {
                vices.Add(mapVice(reader));
            }
            reader.Close();
            return vices;
        }

        public Vice mapVice(SqlDataReader reader)
        {
            return new Vice()
            {
                Id = Convert.ToInt32(reader["ID"]),
                EmployeeNumber = Convert.ToInt32(reader["Mitarbeiternummer"]),
                Organisation = organisationModel.getOrganisation(Convert.ToInt32(reader["Standort"])),
                Surname = Convert.ToString(reader["Name"]),
                Prename = Convert.ToString(reader["Vorname"])
            };
        }

        public void setVice(Vice vice)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC set_vice " + vice.Id + ", " + vice.EmployeeNumber + ", " + vice.Organisation.Id + ", '" + vice.Surname + "', '" + vice.Prename + "'";
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }

        public Vice newVice(Vice vice)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC new_vice " + vice.EmployeeNumber + ", " + vice.Organisation.Id + ", '" + vice.Surname + "', '" + vice.Prename + "'";
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            vice.Id = Convert.ToInt32(reader["ID"]);
            return vice;
        }

        public void deleteVice(Vice vice)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC delete_vice " + vice.Id;
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Sql;
using m151_api.Classes;

namespace m151_api.Entities
{
    public class OrganisationModel
    {
        private DBHelper dbHelper;

        public OrganisationModel()
        {
            dbHelper = DBHelper.Instance;
        }

        public Organisation getOrganisation(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_organisation " + id;
            cmd.Connection = dbHelper.connection;
            Organisation organisation;
            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();
                organisation = mapOrganisation(reader);
            }
            return organisation;
        }

        public List<Organisation> getOrganisations()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_organisations ";
            cmd.Connection = dbHelper.connection;
            List<Organisation> organisations = new List<Organisation>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    organisations.Add(mapOrganisation(reader));
                }
            }
            return organisations;
        }

        public Organisation mapOrganisation(SqlDataReader reader)
        {
            return new Organisation()
            {
                Id = Convert.ToInt32(reader["ID"]),
                Name = Convert.ToString(reader["Name"]),
                Location = Convert.ToString(reader["Ort"]),
                Zip = Convert.ToString(reader["Postleitzahl"]),
                Street = Convert.ToString(reader["Strasse"]),
                Nr = Convert.ToString(reader["Hausnummer"])
            };
        }

        public void setOrganisation(Organisation organisation)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC set_organisation '" + organisation.Id + "', '" + organisation.Name+ "', '" + organisation.Location + "', '" + organisation.Zip + "', '" + organisation.Street + "', '" + organisation.Nr + "'";
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }

        public Organisation newOrganisation(Organisation organisation)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC new_organisation '" + organisation.Name + "', '" + organisation.Location + "', '" + organisation.Zip + "', '" + organisation.Street + "', '" + organisation.Nr + "'";
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            organisation.Id = Convert.ToInt32(reader["ID"]);
            reader.Close();
            return organisation;
        }

        public void deleteOrganisation(Organisation organisation)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC delete_organisation " + organisation.Id;
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }
    }
}
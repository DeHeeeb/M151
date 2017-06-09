using m151_api.Classes;
using m151_api.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace m151_api.Models
{
    public class OrganisationLeaderModel
    {
        private DBHelper dbHelper;
        private OrganisationModel organisationModel;

        public OrganisationLeaderModel()
        {
            dbHelper = DBHelper.Instance;
            organisationModel = new OrganisationModel();
        }

        public OrganisationLeader getOrganisationLeader(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_organisationleader " + id;
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            OrganisationLeader organisationLeader = mapOrganisationLeader(reader);
            reader.Close();
            return organisationLeader;
        }

        public List<OrganisationLeader> getOrganisationLeaders()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_organisationleaders";
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            List<OrganisationLeader> organisationLeaders = new List<OrganisationLeader>();
            while (reader.Read())
            {
                organisationLeaders.Add(mapOrganisationLeader(reader));
            }
            reader.Close();
            return organisationLeaders;
        }

        public OrganisationLeader mapOrganisationLeader(SqlDataReader reader)
        {
            return new OrganisationLeader()
            {
                Id = Convert.ToInt32(reader["ID"]),
                EmployeeNumber = Convert.ToInt32(reader["Mitarbeiternummer"]),
                Organisation = organisationModel.getOrganisation(Convert.ToInt32(reader["Standort"])),
                Surname = Convert.ToString(reader["Name"]),
                Prename = Convert.ToString(reader["Vorname"]),
                IsChairman = Convert.ToBoolean(reader["IstVorsitzender"]),
                IsBoard = Convert.ToBoolean(reader["IstVorstand"])
            };
        }

        public void setOrganisationLeader(OrganisationLeader organisationLeader)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC set_organisationleader " + organisationLeader.Id + ", " + organisationLeader.EmployeeNumber + ", " + organisationLeader.Organisation.Id + ", '" + organisationLeader.Surname + "', '" + organisationLeader.Prename + "', " + organisationLeader.IsChairman + ", " + organisationLeader.IsBoard;
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }

        public OrganisationLeader newOrganisationLeader(OrganisationLeader organisationLeader)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC new_organisationleader " + organisationLeader.EmployeeNumber + ", " + organisationLeader.Organisation.Id + ", '" + organisationLeader.Surname + "', '" + organisationLeader.Prename + "', " + organisationLeader.IsChairman + ", " + organisationLeader.IsBoard;
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            organisationLeader.Id = Convert.ToInt32(reader["ID"]);
            reader.Close();
            return organisationLeader;
        }

        public void deleteOrganisationLeader(OrganisationLeader organisationLeader)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC delete_organisationleader " + organisationLeader.Id;
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }
    }
}
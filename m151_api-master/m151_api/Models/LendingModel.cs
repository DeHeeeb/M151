using m151_api.Classes;
using m151_api.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace m151_api.Models
{
    public class LendingModel
    {
        private DBHelper dbHelper;
        private CustomerModel customerModel;
        private GameModel gameModel;

        public LendingModel()
        {
            dbHelper = DBHelper.Instance;
            customerModel = new CustomerModel();
            gameModel = new GameModel();
        }

        public Lending getLending(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_lending " + id;
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Lending lending = mapLending(reader);
            reader.Close();
            return lending;
        }

        public List<Lending> getLendings()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_lendings";
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            List<Lending> lendings = new List<Lending>();
            while (reader.Read())
            {
                lendings.Add(mapLending(reader));
            }
            reader.Close();
            return lendings;
        }

        public Lending mapLending(SqlDataReader reader)
        {
            Lending lending = new Lending()
            {
                Number = Convert.ToInt32(reader["Ausleihnummer"]),
                Customer = customerModel.getCustomer(Convert.ToString(reader["Kunde"])),
                From = Convert.ToDateTime(reader["VonDatum"]),
                To = Convert.ToDateTime(reader["BisDatum"])
            };
            if (reader["Spiel1"] != null)
            {
                lending.Games.Add(gameModel.GetGame(Convert.ToInt32(reader["Spiel1"])));
            }
            if (reader["Spiel2"] != null)
            {
                lending.Games.Add(gameModel.GetGame(Convert.ToInt32(reader["Spiel2"])));
            }
            if (reader["Spiel3"] != null)
            {
                lending.Games.Add(gameModel.GetGame(Convert.ToInt32(reader["Spiel3"])));
            }
            return lending;
        }

        public void setLending(Lending lending)
        {
            int idGame1 = 0;
            int idGame2 = 0;
            int idGame3 = 0;
            if (lending.Games.Count > 0)
                idGame1 = lending.Games[0].Id;
            if (lending.Games.Count > 1)
                idGame2 = lending.Games[1].Id;
            if (lending.Games.Count > 2)
                idGame3 = lending.Games[2].Id;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_lending " + lending.Number + ", '" + lending.Customer.Email + "', '" + lending.From.ToString("yyyy-MM-dd") + "', '" + lending.To.ToString("yyyy-MM-dd") + "', " + idGame1 + ", " + idGame2 + ", " + idGame3;
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }

        public Lending newLending(Lending lending)
        {
            return lending;
            int idGame1 = 0;
            int idGame2 = 0;
            int idGame3 = 0;
            if (lending.Games.Count > 0)
                idGame1 = lending.Games[0].Id;
            if (lending.Games.Count > 1)
                idGame2 = lending.Games[1].Id;
            if (lending.Games.Count > 2)
                idGame3 = lending.Games[2].Id;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC new_lending " + lending.Number + ", '" + lending.Customer.Email + "', '" + lending.From.ToString("yyyy-MM-dd") + "', '" + lending.To.ToString("yyyy-MM-dd") + "', " + idGame1 + ", " + idGame2 + ", " + idGame3; ;
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            lending.Number = Convert.ToInt32(reader["Number"]);
            reader.Close();
            return lending;
        }

        public void deleteLending(Lending lending)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC delete_lending " + lending.Number;
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }
    }
}
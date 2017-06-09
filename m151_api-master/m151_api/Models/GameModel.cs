using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using m151_api.Entities;
using m151_api.Classes;
using System.Data.SqlClient;

namespace m151_api.Models
{
    public class GameModel
    {
        private DBHelper dbHelper;
        private OrganisationModel organisationModel;
        private PublisherModel publisherModel;

        public GameModel()
        {
            dbHelper = DBHelper.Instance;
            organisationModel = new OrganisationModel();
            publisherModel = new PublisherModel();
            ;
        }

        public List<Game> GetGames()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_games";
            cmd.Connection = dbHelper.connection;
            List<Game> games = new List<Game>();
            var gamesData = new List<Dictionary<string, string>>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    gamesData.Add(ExtractInformation(reader));

                }

            }
            foreach (var gameData in gamesData)
            {
                games.Add(MapGame(gameData));
            }

            return games;
        }

        private Dictionary<string, string> ExtractInformation(SqlDataReader reader)
        {
            return new Dictionary<string, string>
            {
                ["ID"] = Convert.ToString(reader["ID"]),
                ["Spielnummer"] = Convert.ToString(reader["Spielnummer"]),
                ["Standort"] = Convert.ToString(reader["Standort"]),
                ["Bezeichnung"] = Convert.ToString(reader["Bezeichnung"]),
                ["Tarifkategorie"] = Convert.ToString(reader["Tarifkategorie"]),
                ["Alterskategorie"] = Convert.ToString(reader["Alterskategorie"]),
                ["Verlag"] = Convert.ToString(reader["Verlag"]),
                ["Verfuegbarkeit"] = Convert.ToString(reader["Verfuegbarkeit"])
            };
        }

        public Game GetGame(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC get_game " + id;
            cmd.Connection = dbHelper.connection;
            Dictionary<string, string> gameData;
            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();
                 gameData = ExtractInformation(reader);
            }
            Game game = MapGame(gameData);
            return game;
        }

        private Game MapGame(Dictionary<string, string> gameData)
        {
            Game game = new Game();
            game.Id = Convert.ToInt32(gameData["ID"]);
            game.Number = Convert.ToInt32(gameData["Spielnummer"]);
            game.Organisation = organisationModel.getOrganisation(Convert.ToInt32(gameData["Standort"]));
            game.Name = Convert.ToString(gameData["Bezeichnung"]);
            game.Tariff = Convert.ToInt32(gameData["Tarifkategorie"]);
            game.Age = Convert.ToString(gameData["Alterskategorie"]);
            game.Publisher = publisherModel.getPublisher(Convert.ToString(gameData["Verlag"]));
            game.IsAvailable = Convert.ToBoolean(gameData["Verfuegbarkeit"]);
            return game;
        }

        public void SetGame(Game game)
        {
            int organisationId = 0;
            string publisherName = "null";
            if (game.Publisher != null)
            {
                publisherName = game.Publisher.Name;
            }
            if (game.Organisation != null)
            {
                organisationId = game.Organisation.Id;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC set_game " + game.Id + ", " + game.Number + ", " + organisationId + ", '" + game.Name + "', " + game.Tariff + ", '" + game.Age + "', '" + publisherName + "', " + game.IsAvailable;
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }

        public Game NewGame(Game game)
        {
            int organisationId = 0;
            string publisherName = "null";
            if (game.Publisher != null)
            {
                publisherName = game.Publisher.Name;
            }
            if (game.Organisation != null)
            {
                organisationId = game.Organisation.Id;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC new_game " + game.Number + ", " + organisationId + ", '" + game.Name + "', " + game.Tariff + ", '" + game.Age + "', '" + publisherName + "', " + game.IsAvailable;
            cmd.Connection = dbHelper.connection;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            game.Id = Convert.ToInt32(reader["ID"]);
            reader.Close();
            return game;
        }

        public void DeleteGame(Game game)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC delete_game " + game.Id;
            cmd.Connection = dbHelper.connection;
            cmd.ExecuteNonQuery();
        }
    }
}
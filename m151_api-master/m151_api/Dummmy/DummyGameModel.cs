using m151_api.Entities;
using System.Collections.Generic;

namespace m151_api.Dummmy
{
    public class DummyGameModel
    {
        private static readonly Organisation Organisation = new Organisation() { Name = "SpielGut" };
        private static readonly Publisher Publisher = new Publisher() { Name = "HasBro" };

        private readonly List<Game> _games = new List<Game>(new[]{
            new Game()
            {
                Number = 1,
                Age = "12",
                Tariff = 8,
                IsAvailable = true,
                Name = "Eile mit Weile",
                Organisation = Organisation,
                Publisher = Publisher
            },
            new Game()
            {
                Number = 2,
                Age = "12",
                Tariff = 8,
                IsAvailable = true,
                Name = "Eile mit Weile",
                Organisation = Organisation,
                Publisher = Publisher
            },
            new Game()
            {
                Number = 3,
                Age = "12",
                Tariff = 8,
                IsAvailable = false,
                Name = "Eile mit Weile",
                Organisation = Organisation,
                Publisher = Publisher
            },
            new Game()
            {
                Number = 4,
                Age = "12",
                Tariff = 8,
                IsAvailable = true,
                Name = "Eile mit Weile",
                Organisation = Organisation,
                Publisher = Publisher
            },  new Game()
            {
                Number = 5,
                Age = "12",
                Tariff = 8,
                IsAvailable = true,
                Name = "Eile mit Weile",
                Organisation = Organisation,
                Publisher = Publisher
            },  new Game()
            {
                Number = 6,
                Age = "12",
                Tariff = 8,
                IsAvailable = true,
                Name = "Eile mit Weile",
                Organisation = Organisation,
                Publisher = Publisher
            },  new Game()
            {
                Number = 7,
                Age = "12",
                Tariff = 8,
                IsAvailable = false,
                Name = "Eile mit Weile",
                Organisation = Organisation,
                Publisher = Publisher
            }
        });

        public List<Game> GetGames()
        {
            return _games;
        }

        public Game GetGame(int id)
        {
            return _games[0];
        }

        public Game NewGame(Game game)
        {
            return game;
        }


        public void DeleteGame(Game game)
        {

        }
    }
}
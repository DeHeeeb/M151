using System.Collections.Generic;
using System.Web.Http;
using m151_api.Entities;
using m151_api.Middleware;
using m151_api.Dummmy;
using m151_api.Models;

namespace m151_api.Controllers
{
    [AuthorizationHandler]
    public class GamesController : ApiController

    {
        private readonly GameModel _gameModel = new GameModel();


        public IEnumerable<Game> Get()
        {
            return _gameModel.GetGames();
        }
        public Game Get(int id)
        {
            return _gameModel.GetGame(id);
        }
        public Game Post(Game game)
        {
            return _gameModel.NewGame(game);
        }
    }
}
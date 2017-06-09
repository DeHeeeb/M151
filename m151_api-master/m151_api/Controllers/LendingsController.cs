using m151_api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using m151_api.Models;

namespace m151_api.Controllers
{
    public class LendingsController : ApiController
    {
        LendingModel _lendingModel = new LendingModel();
        
        public IEnumerable<Lending> Get()
        {
          return _lendingModel.getLendings();
        }
        
        public Lending Get(int id)
        {
            return _lendingModel.getLending(id);
        }
        
        public HttpResponseMessage Post([FromBody]Lending lending)
        {
            _lendingModel.newLending(lending);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
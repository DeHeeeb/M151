using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using m151_api.Classes;
using m151_api.Entities;

namespace m151_api.Controllers
{
    public class CustomersController : ApiController
    {
        public Customer Get(Token token)
        {
            var customerToReturn = TemporaryFunctions.getCustomerByToken();
            if (customerToReturn == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return customerToReturn;
        }
        public HttpResponseMessage Post([FromBody]Customer customer)
        {
            return new HttpResponseMessage(TemporaryFunctions.customerCouldBeSaved() ? HttpStatusCode.Created : HttpStatusCode.Conflict);
        }
        public HttpResponseMessage Put([FromBody]Customer customer)
        {
            return new HttpResponseMessage(TemporaryFunctions.customerCouldBeUpdated() ? HttpStatusCode.Created : HttpStatusCode.Conflict);
        }
    }
}
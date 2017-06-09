using m151_api.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m151_api.Controllers
{
    public class LoginController : ApiController
    {
        public Token Post([FromBody]Credentials credentials)
        {
            if (TemporaryFunctions.credentialsAreValid())
                return new Token() { Value = "THIS_RANDOM_VALUE_HAS_TO_GET_WRITTEN_INTO_THE_DATABSE" };
            else
                throw new HttpResponseException(HttpStatusCode.Forbidden);
        }
    }
}
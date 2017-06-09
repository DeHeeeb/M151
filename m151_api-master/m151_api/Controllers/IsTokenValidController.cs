using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using m151_api.Classes;

namespace m151_api.Controllers
{
    public class IsTokenValidController : ApiController
    {
        public void Get(Token token)
        {
            if (!TemporaryFunctions.tokenIsValid())
                throw new HttpResponseException(HttpStatusCode.Forbidden);
        }
    }
}
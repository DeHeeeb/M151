using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.Practices.Unity;

namespace m151_api.Middleware
{
    public class AuthorizationHandler: ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var tokenValue = actionContext.Request.Headers.Authorization;
            if (!TemporaryFunctions.userIsAuthorized())
                throw new HttpResponseException(HttpStatusCode.Forbidden);
        }
    }
}
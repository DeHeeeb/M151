using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using m151_api.Entities;

namespace m151_api.Controllers
{
    public class OrganisationsController : ApiController
    {
        private readonly OrganisationModel _organisationModel = new OrganisationModel();
        public IEnumerable<Organisation> Get()
        {
            return _organisationModel.getOrganisations();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using m151_api.Classes;
using m151_api.Entities;
using m151_api.Models;

namespace m151_api.Controllers
{
    public class EmployeesController : ApiController
    {
        private static readonly EmployeeModel _employeeModel = new EmployeeModel();
        public IEnumerable<Employee> Get()
        {
            return _employeeModel.getEmployees();
        }
        public Employee Get(int id)
        {
            return _employeeModel.getEmployee(id);
        }
        public HttpResponseMessage Post([FromBody]Employee employee)
        {
            _employeeModel.newEmployee(employee);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
        public HttpResponseMessage Put(int id, [FromBody]Employee employee)
        {
            _employeeModel.setEmployee(employee);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
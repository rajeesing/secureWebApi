using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SecureApi.Controllers
{
    public class EmployeeController : ApiController
    {
        // GET: Employee
        [System.Web.Mvc.HttpGet]
        [System.Web.Http.Authorize]
        [System.Web.Mvc.Route("/api/GetEmployee")]
        public string GetEmployee()
        {
            return "Employee Details";
        }
    }
}
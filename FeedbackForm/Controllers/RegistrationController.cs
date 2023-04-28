using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace FeedbackForm.Controllers
{

    [System.Web.Http.Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ApiController
    {
        private readonly IConfiguration _configuration;

        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("registration")]
        public string registration(RegistrationController registration)
        {
            return "";
        }
    }
    
}

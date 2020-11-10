using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace APIADS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "ADS", DateTime.Now.Year.ToString() };
        }
    }
}

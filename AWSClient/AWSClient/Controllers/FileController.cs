using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MUS.API.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {

        [HttpGet("PostData")]
        public string PostData()
        {
            return "Test";
        }
    }
}

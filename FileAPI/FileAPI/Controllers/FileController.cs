using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FileHandler.Implementation;
using FileHandler.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MUS.API.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private static IFileHandler fh = new FileHandlerImpl();

        public FileController(IFileHandler fh) {
            fh = new FileHandlerImpl();
        }

        [Route("File")]
        //returning file to client
        [HttpGet]
        public HttpResponseMessage Generate()
        {
            var stream = new MemoryStream();
            // processing the stream.

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = "program.cs"
            };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            return result;
        }

        // POST api/values
        [Route("CreateFile")]
        [HttpPost]
        public bool Post([FromBody]string name)
        {
            return fh.CreateFile(name);
        }


        // POST api/values
        [Route("CreateClass")]
        [HttpPost]
        public void Post([FromBody]string[] value)
        {

        }

         // POST api/values
        [Route("CreateWhile")]
        [HttpPost]
        public void CreateWhile([FromBody]string[] value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

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

    public FileController(IFileHandler fh)
    {
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
    [Route("CreateIf")]
    [HttpPost]
    public void CreateIf([FromBody] dynamic data)
    {
      string varName = data.varName;
      string compareType = data.varName;
      string number = data.varName;

      List<string> l = new List<string>();
      


    }



    // POST api/values
    [Route("SetColumn")]
    [HttpPost]
    public bool SetColumn([FromBody] dynamic data)
    {
      fh.ColumnNumber = data.columnNumber;
      return true;
    }


    // POST api/values
    [Route("SetRow")]
    [HttpPost]
    public bool SetRow([FromBody] dynamic data)
    {
      fh.RowNumber = data.lineNumber;
      return true;
    }


    // POST api/values
    [Route("DeleteLine")]
    [HttpPost]
    public string DeleteLine([FromBody] dynamic data)
    {
      return fh.DeleteRange(data.lineNr, 1);
    }
    

    // POST api/values
    [Route("ReadFile")]
    [HttpPost]
    public string ReadFile()
    {
      return fh.ReadFile();
    }
  }
}

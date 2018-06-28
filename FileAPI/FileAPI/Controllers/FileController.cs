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

    static FileController()
    {
      CreateFile();
    }

    public FileController(IFileHandler fh)
    {
    }
    
    private static void CreateFile()
    {
      List<string> l = new List<string>();
      l.Add("using System;");
      l.Add("using System.Collections.Generic;");
      l.Add("using System.Linq;");
      l.Add("using System.Text;");
      l.Add("using System.Threading.Tasks;");
      l.Add("");
      l.Add("namespace ConsoleApp1");
      l.Add("{");
      l.Add("  class program");
      l.Add("  {");
      l.Add("    static void Main(string[] args)");
      l.Add("    {");
      l.Add("      ");
      l.Add("    {");
      l.Add("  }");
      l.Add("}");
      fh.AppendToFile(l);
    }

    // POST api/values
    [Route("CreateFor")]
    [HttpPost]
    public bool CreateFor([FromBody] dynamic data)
    {
      string varName = data.varName;
      string lowerBound = data.lowerBound;
      string upperBound = data.upperBound;
      string stepWidth = data.stepWidth;


      List<string> l = new List<string>();
      string s1 = "for(" + varName + " = " + lowerBound + "; " + varName + " < " + upperBound + "; " + varName + "+=" + stepWidth + ")";
      l.Add(s1);
      l.Add("{");
      l.Add("");
      l.Add("}");
      l.Add("");

      fh.AppendFromLine(fh.currentLine, l);
      fh.currentLine += 2;
      
      return true;
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
      fh.currentPositionInLine = data.columnNumber;
      return true;
    }


    // POST api/values
    [Route("SetRow")]
    [HttpPost]
    public bool SetRow([FromBody] dynamic data)
    {
      fh.currentLine = data.lineNumber;
      return true;
    }


    // POST api/values
    [Route("DeleteLine")]
    [HttpPost]
    public bool DeleteLine([FromBody] dynamic data)
    {
      string s = data.lineNumber.ToString();
      int temp = Convert.ToInt32(s);
      return fh.DeleteRange(temp, 1);
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FileHandler.Implementation;
using FileHandler.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [Route("CreateIf")]
        [HttpPost]
        public void CreateIf([FromBody] dynamic data)
        {
            string varName = data.varName;
            string compareType = data.compareType;
            string number = data.number;

            List<string> l = new List<string>();
            l.Add("if(" + varName + " " + compareType + ")");
            l.Add("{");
            l.Add("");
            l.Add("}");
            l.Add("");

            fh.AppendFromLine(fh.currentLine, l);
            fh.currentLine += 2;
        }

        [Route("CreateWhile")]
        [HttpPost]
        public void CreateWhile([FromBody] dynamic data)
        {
            string varName = data.varName;
            string compareType = data.compareType;
            string number = data.number;

            List<string> l = new List<string>();
            l.Add("while(" + varName + " " + compareType + ")");
            l.Add("{");
            l.Add("");
            l.Add("}");
            l.Add("");

            fh.AppendFromLine(fh.currentLine, l);
            fh.currentLine += 2;
        }

        // POST api/values
        [Route("ConsoleWriteLine")]
        [HttpPost]
        public bool ConsoleWriteLine([FromBody] dynamic data)
        {
            List<string> l = new List<string>();
            l.Add("Console.WriteLine(" + data.text + ")");
            l.Add("");  
            fh.currentLine += 1;
            return fh.AppendFromLine(fh.currentLine, l); 
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
        public string DeleteLine([FromBody] dynamic data)
        {
            
            return fh.DeleteRange(data.lineNumber, 1);
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

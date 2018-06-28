using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FileHandler.Interface;

namespace FileHandler.Implementation
{
  public class FileHandlerImpl : IFileHandler
  {
    public int ColumnNumber { get; set; }
    public int RowNumber { get; set; }
    FileStream fileStream = null;


    public FileHandlerImpl()
    {

    }

    public bool CreateFile(string name)
    {
      fileStream = new FileStream(name, FileMode.OpenOrCreate);
      try
      {
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
          writer.WriteLine("using System;");
          writer.WriteLine("using System.Collections.Generic;");
          writer.WriteLine("using System.Linq;");
          writer.WriteLine("using System.Text;");
          writer.WriteLine("using System.Threading.Tasks;");
          writer.WriteLine("");
          writer.WriteLine("namespace ConsoleApp1");
          writer.WriteLine("{");
          writer.WriteLine($"  class {name}");
          writer.WriteLine("  {");
          writer.WriteLine("    static void Main(string[] args)");
          writer.WriteLine("    {");
          writer.WriteLine("      ");
          writer.WriteLine("    {");
          writer.WriteLine("  }");
          writer.WriteLine("}");
        }
        ColumnNumber = 6;
        RowNumber = 13;
      }
      catch (SystemException ex)
      {
        Console.WriteLine(ex.Message);
        return false;
      }

      return true;
    }

    public bool AppendFromLine(int lineNumber, string content)
    {
      using (StreamWriter writer = new StreamWriter(fileStream))
      {

      }
      catch (SystemException ex)
      {
        Console.WriteLine(ex.Message);
        return false;
      }
      return true;
    }

    public bool AppendToFile(string name, string content)
    {
      throw new NotImplementedException();
    }

    public bool DeleteFile(string name)
    {
      throw new NotImplementedException();
    }

    public bool DeleteFileContent(string name)
    {
      throw new NotImplementedException();
    }

    public bool PrependToFile(string name, string content)
    {
      throw new NotImplementedException();
    }

    public bool ReplaceLineInFile(int lineNumber, string content)
    {
      throw new NotImplementedException();
    }

    public bool ReplaceRangeInFile(int startLineNumber, int endLineNumber, string[] lineArray)
    {
      throw new NotImplementedException();
    }

    public string ReadFile()
    {
      throw new NotImplementedException();
    }

    public bool DeleteRange(int startLineNr, int count)
    {
      throw new NotImplementedException();
    }
  }
}

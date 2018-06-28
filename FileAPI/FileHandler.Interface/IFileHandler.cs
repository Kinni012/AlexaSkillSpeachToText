using System;
using System.Collections.Generic;
using System.Text;

namespace FileHandler.Interface
{
  public interface IFileHandler
  {
    int ColumnNumber { get; set; }
    int RowNumber { get; set; }
    string ReadFile();
    bool CreateFile(string name);
    bool DeleteFile(string name);
    bool DeleteFileContent(string name);
    bool AppendToFile(string name, string content);
    bool PrependToFile(string name, string content);
    bool AppendFromLine(int lineNumber, string content);
    bool ReplaceLineInFile(int lineNumber, string content);
    bool ReplaceRangeInFile(int startLineNumber, int endLineNumber, string[] lineArray);
    bool DeleteRange(int startLineNr, int count);
  }
}

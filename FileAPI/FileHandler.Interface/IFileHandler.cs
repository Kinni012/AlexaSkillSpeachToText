using System;
using System.Collections.Generic;
using System.Text;

namespace FileHandler.Interface
{
    public interface IFileHandler
    {
        bool CreateFile(string name);
        bool DeleteFile(string name);
        bool DeleteFileContent(string name);
        bool AppendToFile(string name, string content);
        bool PrependToFile(string name, string content);
        bool AppendFromLine(int lineNumber, string content);
        bool ReplaceLineInFile(int lineNumber, string content);
        bool ReplaceRangeInFile(int startLineNumber, int endLineNumber, string[] lineArray);
    }
}

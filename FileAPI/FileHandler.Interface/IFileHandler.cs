using System;
using System.Collections.Generic;
using System.Text;

namespace FileHandler.Interface
{
    public interface IFileHandler
    {
        int currentLineCount { get; set; }
        int currentLine { get; set; }
        int currentPositionInLine { get; set; }
        string ReadFile();
        bool DeleteFile();
        bool AppendToFile(List<string> content);
        bool AppendFromLine(int lineNumber, List<string> content);
        bool ReplaceLineInFile(int lineNumber, string content);
        bool ReplaceRangeInFile(int startLineNumber, List<string> lineArray);
        bool DeleteRange(int startLineNumber, int count);
    }
}

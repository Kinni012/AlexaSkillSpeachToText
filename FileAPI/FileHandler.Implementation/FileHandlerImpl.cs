using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FileHandler.Interface;

namespace FileHandler.Implementation
{
    public class FileHandlerImpl : IFileHandler
    {
        public int currentLineCount { get; set; }
        public int currentLine { get; set; }
        public int currentPositionInLine { get; set; }
        public List<string> lines;
        public FileHandlerImpl()
        {
            lines = new List<string>();
            currentLineCount = 15;
            currentLine = 12;
            currentPositionInLine = 6;
        }
        public string ReadFile()
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;
            foreach (string l in lines)
            {
                StringBuilder lb = new StringBuilder();
                lb.Append(index++);
                lb.Append(l);
                lb.Append("<br>");
                sb.Append(lb.ToString());
            }

            return sb.ToString();
        }

        public bool DeleteFile()
        {
            lines?.Clear();
            currentLine = 12;
            if (lines.Count == 0)
                return true;
            else
                return false;
        }

        public bool DeleteRange(int startLineNumber, int count)
        {
            currentLineCount -= count;
            lines.RemoveRange(startLineNumber, count);
            return lines.Count == currentLineCount;
        }

        public bool AppendFromLine(int lineNumber, List<string> content)
        {
            lines.InsertRange(lineNumber, content);
            currentLineCount += content.Count;
            return currentLineCount == lines.Count;
        }

        public bool AppendToFile(List<string> content)
        {
            lines.AddRange(content);
            currentLineCount += content.Count;
            return currentLineCount == lines.Count;
        }

        public bool ReplaceRangeInFile(int startLineNumber, List<string> content)
        {
            lines.RemoveRange(startLineNumber, content.Count);
            lines.InsertRange(startLineNumber, content);
            return currentLineCount == lines.Count;
        }

        public bool ReplaceLineInFile(int lineNumber, string content)
        {
            return ReplaceRangeInFile(lineNumber, new List<string>() { content });
        }

    }

}

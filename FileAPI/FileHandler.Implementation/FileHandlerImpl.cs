using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FileHandler.Interface;

namespace FileHandler.Implementation
{
    public class FileHandlerImpl : IFileHandler
    {
        public FileHandlerImpl()
        {

        }

        public bool CreateFile(string name)
        {
            FileStream fileStream = new FileStream(name, FileMode.OpenOrCreate);
            try
            {
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    writer.WriteLine("test");
                }
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
            throw new NotImplementedException();
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
    }
}

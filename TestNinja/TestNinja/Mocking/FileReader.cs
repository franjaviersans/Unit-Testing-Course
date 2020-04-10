using System.IO;

namespace TestNinja.Mocking
{
    public class FileReader : IFileReader
    {
        public string ReadFile(string fileName)
        {
            return File.ReadAllText(fileName);
        }
    }
}
namespace SoftwareTesting.Mocking
{
    public interface IFileReader
    {
        public string Read(string path);
    }
    public class FileReader : IFileReader
    {
        public string Read(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
using System.Net;

namespace SoftwareTesting.Mocking
{
    public interface IFileDownloader
    {
        void Download(string url, string path);
    }

    public class FileDownloader : IFileDownloader
    {
        public WebClient client = new WebClient();
        
        public void Download(string url, string path)
        {
            client.DownloadFile(url, path);

        }
    }
}
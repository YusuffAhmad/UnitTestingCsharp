using System.Net;

namespace SoftwareTesting.Mocking
{
    public class InstallerHelper
    {
        private string _setupDestinstionFile;
        private IFileDownloader _downloader;

        public InstallerHelper(IFileDownloader downloader)
        {
            _downloader = downloader;
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                _downloader.Download(string.Format("http://example.com/{0}/{1}",
                        customerName,
                        installerName),
                    _setupDestinstionFile);
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}
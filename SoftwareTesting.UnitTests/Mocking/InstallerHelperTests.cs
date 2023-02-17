using System.Net;
using Moq;
using SoftwareTesting.Mocking;

namespace SoftwareTesting.UnitTests.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private Mock<IFileDownloader> _client;
        private InstallerHelper _installer; 
        [SetUp]
        public void SetUp()
        {
            _client = new Mock<IFileDownloader>();
            _installer = new InstallerHelper(_client.Object);
        }

        [Test]
        public void DownloadInstaller_WhenTheDownloadFail_ReturnFalse()
        {
            _client.Setup(d => d.Download(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

            var result = _installer.DownloadInstaller("","");

            Assert.That(result, Is.EqualTo(true));
        }
        
        [Test]
        public void DownloadInstaller_WhenTheDownloadSucceed_ReturnTrue()
        {
            _client.Setup(d => d.Download("","")).Throws<WebException>();

            var result = _installer.DownloadInstaller("","");

            Assert.That(result, Is.EqualTo(true));
        }
    }
}
using Moq;
using SoftwareTesting.Mocking;

namespace SoftwareTesting.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _videoRepository;
        private VideoService _service;
        [SetUp]
        public void Setup()
        {
            _fileReader = new Mock<IFileReader>();
            _videoRepository = new Mock<IVideoRepository>();
            _service = new VideoService(_fileReader.Object, _videoRepository.Object);
        }
        [Test]
        public void ReadVideoTittle_EmptyFile_ReturnError()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _service.ReadVideoTittle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetunprocessedvidoeAsCsv_AllVideosAreProcessed_ReturnAnEmptyString()
        {
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _service.GetunprocessedvidoeAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetunprocessedvidoeAsCsv_AFewUnprocessedVideos_ReturnAStringWithIdsOfUnprocessedVideos()
        {
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>()
            {
                new Video { Id = 1 },
                new Video { Id = 2 },
                new Video { Id = 3 }
            });

            var result = _service.GetunprocessedvidoeAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
           
        }
    }
}

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SoftwareTesting.Mocking
{
    public class VideoService
    {
        // public IFileReader FileReader { get; set; } // Property Injection
        private IFileReader _fileReader;
        private IVideoRepository _videoRepository;

        public VideoService(IFileReader fileReader = null, IVideoRepository videoRepository = null)
        {
            this._videoRepository = videoRepository ?? new VideoRepository();
            _fileReader = fileReader ?? new FileReader();
        }
        public string ReadVideoTittle(/*IFileReader fileReader*/)
        {
            // var str = fileReader.Read("video.txt");
            var str = _fileReader.Read("video.txt");
            // var video = JsonSerializer.Deserialize<Video>(str);
            var video = JsonSerializer.Deserialize<Video>(str);
            if (video == null)
                return "Error parsing the video,";
            return video.Tittle;
        }

        public string GetunprocessedvidoeAsCsv()
        {
            var videoIds = new List<int>();

            var videos = _videoRepository.GetUnprocessedVideos();
            foreach (var v in videos)
                videoIds.Add(v.Id);

            return String.Join(",", videoIds);
        }

    }

    public class Video
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}
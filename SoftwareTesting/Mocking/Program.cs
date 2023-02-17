namespace SoftwareTesting.Mocking
{
    public class Program
    {
        public static void Main()
        {
            var videoService = new  VideoService();

            var Tittle = videoService.ReadVideoTittle();
        }
    }
}


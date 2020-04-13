using Moq;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TestNinja.Mocking;

namespace TestNinja.UnitTest.Mocking
{
    [TestFixture]
    class VideoServiceTests
    {
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _videoRepository;
        private VideoService _videoService;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _videoRepository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object, _videoRepository.Object);
        }

        [Test]
        public void ReadVideoTitle_FileDoesntExist_ErrorParsing()
        {
            _fileReader.Setup(fr => fr.ReadFile("video.txt")).Returns("");

            var resultString = _videoService.ReadVideoTitle();

            Assert.That(resultString, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_HasSomeVideos_ListOfUnprocessedVideoFieldsAsString()
        {
            var videos = new List<Video> {
                new Video { Id = 1 },
                new Video { Id = 10 }
            };
            _videoRepository.Setup(foo => foo.GetVideoRepostory()).Returns(videos);


            var resultString = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(resultString, Is.EqualTo("1,10"));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_WithNoVideos_ReturnsEmptyString()
        {
            IEnumerable<Video> videos = new List<Video> ();
            _videoRepository.Setup(foo => foo.GetVideoRepostory()).Returns(videos);

            var resultString = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(resultString, Is.EqualTo(""));
        }
    }
}

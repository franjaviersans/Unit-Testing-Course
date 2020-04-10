using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public class VideoRepository : IVideoRepository
    {
        public IEnumerable<Video> GetVideoRepostory()
        {
            using (var context = new VideoContext())
            {
                return
                    (from video in context.Videos
                     where !video.IsProcessed
                     select video).ToList();
            }
        }
    }
}

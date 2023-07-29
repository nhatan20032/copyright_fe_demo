using System.Collections.Generic;

namespace copyrights_fe.Model
{
    public class Music
    {
        public List<film_video> filmvideos { set; get; }
        public List<package> packages { set; get; }
        public List<film_banner> banners { set; get; }
        public int countview { set; get; }
    }
}

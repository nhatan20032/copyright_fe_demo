using copyrights_fe.Model;
using ServiceStack.DataAnnotations;
using System.Collections.Generic;


namespace copyrights_fe.Model
{
    public partial class film_menu
    {
        [Ignore]
        public List<film_menu> child { get; set; }
    }

    public partial class film_show_case
    {
        [Ignore]
        public List<film_show_case_item> items { get; set; }

    }

    public partial class film_show_case_item
    {
        [Ignore]
        public string href { get; set; }
        [Ignore]
        public string episode_title
        {
            get
            {
                return this.episode > 0 ? (this.episode + "") : "";
            }
        }
        [Ignore]
        public string thumb { get; set; }
        [Ignore]
        public string icon { get; set; }
        [Ignore]
        public string title { get; set; }
        /// <summary>
        /// Tập phim
        /// </summary>
        [Ignore]
        public int? episode { get; set; }
        /// <summary>
        /// Tập hiện tại
        /// </summary>
        [Ignore]
        public int? episode_current { get; set; }
        /// <summary>
        /// Tập hiện tại
        /// </summary>
        [Ignore]
        public string imdb { get; set; }
        [Ignore]
        public string duration { get; set; }
        [Ignore]
        public double rating { get; set; }

        /// <summary>
        /// comment - content
        /// </summary>
        [Ignore]
        public string content { get; set; }
        /// <summary>
        /// comment - user_fullname
        /// </summary>
        [Ignore]
        public string user_fullname { get; set; }
        [Ignore]
        public string catalog_title { get; set; }
        [Ignore]
        public string shortTitle { get; set; }
    }

    public partial class film_catalog
    {
        [Ignore]
        public List<vw_film_video> videos { get; set; }
        [Ignore]
        public int total_videos { get; set; }
    }


    public partial class vw_film_video
    {
        [Ignore]
        public List<vw_film_video> Items { get; set; }

        [Ignore]
        public List<film_catalog> catalogs { get; set; }

        [Ignore]
        public List<film_comment> comments { get; set; }
        [Ignore]
        public film_comment comment_item { get; set; }
    }
    public partial class vw_film_video_item
    {
        [Ignore]
        public string href { get; set; }
        [Ignore]
        public string episode_title
        {
            get
            {
                return this.episode > 0 ? (this.episode + "") : "";
            }
        }
        [Ignore]
        public string thumb { get; set; }
        [Ignore]
        public string icon { get; set; }
        [Ignore]
        public string title { get; set; }
        /// <summary>
        /// Tập phim
        /// </summary>
        [Ignore]
        public int? episode { get; set; }
        /// <summary>
        /// Tập hiện tại
        /// </summary>
        [Ignore]
        public int? episode_current { get; set; }
        /// <summary>
        /// Tập hiện tại
        /// </summary>
        [Ignore]
        public string imdb { get; set; }
        [Ignore]
        public string duration { get; set; }
        [Ignore]
        public double rating { get; set; }

        /// <summary>
        /// comment - content
        /// </summary>
        [Ignore]
        public string content { get; set; }
        /// <summary>
        /// comment - user_fullname
        /// </summary>
        [Ignore]
        public string user_fullname { get; set; }

        [Ignore]
        public string catalog_tilte { get; set; }
    }

    public partial class film_cp_extend
    {
        [Ignore]
        public film_cp film_cp { set; get; }
        [Ignore]
        public vw_film_cp vw_film_cp { set; get; }
        [Ignore]
        public List<film_video> list_film_video { set; get; }
        [Ignore]
        public List<vw_film_video> list_vwfilm_video { set; get; }
    }
}

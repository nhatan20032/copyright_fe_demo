namespace copyrights_fe.Services.Utilities
{
    public class PagingModel
    {
        public static int LIMIT = 12;
        public int offset;
        public int limit;
        public string search;
        public int current;
        public int catalog_song_id;
        public int catalog_id;
    }
}

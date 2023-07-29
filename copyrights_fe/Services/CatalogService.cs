using copyrights_fe.Model;
using copyrights_fe.Services.Connection;
using copyrights_fe.Services.Utilities;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Legacy;

namespace copyrights_fe.Services
{
    public class CatalogService : AppConnection
    {
        public const int Is_Menu = 1;
        public const int Is_Catalog = 1;
        public const int Is_Footer = 1;
        public List<film_catalog> GetAll(PagingModel page = null)
        {
            if (page == null) page = new PagingModel() { offset = 0, limit = 4 };
            if (page.search == null) page.search = "";
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<film_catalog>();
                query.OrderByDescending(x => x.datecreated);

                int offset = 0; try { offset = page.offset; }
                catch { }

                int limit = 10;//int.Parse(Request.Params["limit"]);
                try { limit = page.limit; }
                catch { }
                //query=query.Where(e => (e.title.Contains(page.search)));
                /**/


                List<film_catalog> rows = db.Select(query)
                    .Skip(offset).Take(limit).ToList();
                return rows;
            }
        }
        public List<vw_film_catalog> GetViewAll(PagingModel page = null)
        {
            if (page == null) page = new PagingModel() { offset = 0, limit = 4 };
            if (page.search == null) page.search = "";
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<vw_film_catalog>();
                query.OrderByDescending(x => x.id);
                int offset = 0; try { offset = page.offset; }
                catch { }

                int limit = 10;
                try { limit = page.limit; }
                catch { }
                List<vw_film_catalog> rows = db.Select(query)
                    .Skip(offset).Take(limit).ToList();
                return rows;
            }
        }

        //public List<film_catalog> GetAll()
        //{
        //    using (var db = _connectionFilmLala.OpenDbConnection())
        //    {
        //        var query = db.From<film_catalog>();
        //        query.OrderBy(x => x.position);


        //        List<film_catalog> rows = db.Select(query).ToList();
        //        return rows;
        //    }
        //}
        public film_catalog ById(int id)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {

                return db.Select<film_catalog>(x => x.Id == id).FirstOrDefault();
            }
        }

        // paging here
        public film_catalog ByIdInfo(int id, int current, PagingModel page = null)
        {
            if (page == null) page = new PagingModel()
            {
                limit = PagingModel.LIMIT * current,
                current = current,
                offset = (current - 1) * PagingModel.LIMIT
            };

            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var cate = db.Select<film_catalog>(x => x.Id == id).FirstOrDefault();
                if (cate == null)
                    return new film_catalog();
                // query 
                var query = db.From<vw_film_video>()
                    .Join<film_catalog_film>((x, y) => x.Id == y.filmid && x.status == 2)
                    .Where<film_catalog_film>(x => x.catalogid == id)
                    // tung cmt
                    .GroupBy<vw_film_video>(x => x.Id)
                    .OrderByDescending<vw_film_video>(x => x.datecreated);

                int offset = page.offset;
                int limit = page.limit;
                //int offset = (current - 1) * PagingModel.LIMIT; try { offset = page.offset; }
                //catch { }
                //int limit = (PagingModel.LIMIT * current);
                //try { limit = page.limit; }
                //catch { }

                var count = db.Count(query);
                cate.total_videos = (int)count;
                cate.videos = db.Select(query)
                    .Skip(offset).Take(PagingModel.LIMIT)
                    .ToList();

                // catalog by film
                var film_ids = cate.videos.Select(x => x.Id).ToList();
                if (film_ids.Count > 0)
                {
                    var catalogs = db.Select<film_catalog>().ToList();
                    var film_catalog = db.Select<film_catalog_film>(x => film_ids.Contains(x.filmid ?? 0)).ToList();
                    foreach (var vd in cate.videos)
                    {
                        var cata_ids = film_catalog.Where(x => x.filmid == vd.Id).Select(x => x.catalogid ?? 0).ToList();
                        if (cata_ids == null || cata_ids.Count == 0)
                        {
                            continue;
                        }
                        vd.catalogs = catalogs.Where(x => cata_ids.Contains(x.Id)).ToList();
                    }
                }
                return cate;
            }
        }




        public film_catalog filmOther(int catalog_id, int film_id, PagingModel page = null)
        {
            if (page == null) page = new PagingModel() { offset = 0, limit = PagingModel.LIMIT };

            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var cate = db.Select<film_catalog>(x => x.Id == catalog_id).FirstOrDefault();
                if (cate == null)
                    return new film_catalog();

                // vw_film_video
                var query = db.From<vw_film_video>()
                    .Join<film_catalog_film>((x, y) => x.Id == y.filmid && x.status == 2)
                    .Where<film_catalog_film>(x => x.catalogid == catalog_id)
                    .Where<film_catalog_film>(x => x.filmid != film_id)
                    .GroupBy<vw_film_video>(x => x.Id)
                    .OrderByDescending<vw_film_video>(x => x.datecreated);

                int offset = 0; try { offset = page.offset; }
                catch { }
                int limit = PagingModel.LIMIT;
                try { limit = page.limit; }
                catch { }
                var count = db.Count(query);
                cate.total_videos = (int)count;
                cate.videos = db.Select(query)
                    .Skip(offset).Take(limit)
                    .ToList();

                // vw_film_video by film
                var film_ids = cate.videos.Select(x => x.Id).ToList();
                if (film_ids.Count > 0)
                {
                    var catalogs = db.Select<film_catalog>().ToList();
                    var film_catalog = db.Select<film_catalog_film>(x => film_ids.Contains(x.filmid ?? 0)).ToList();
                    foreach (var vd in cate.videos)
                    {
                        var cata_ids = film_catalog.Where(x => x.filmid == vd.Id).Select(x => x.catalogid ?? 0).ToList();
                        if (cata_ids == null || cata_ids.Count == 0)
                        {
                            continue;
                        }
                        vd.catalogs = catalogs.Where(x => cata_ids.Contains(x.Id)).ToList();
                    }
                }
                return cate;
            }
        }
    }
}

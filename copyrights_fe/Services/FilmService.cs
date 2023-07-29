using copyrights_fe.Model;
using copyrights_fe.Services.Connection;
using copyrights_fe.Services.Utilities;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Legacy;

namespace copyrights_fe.Services
{
    public class FilmService : AppConnection
    {
        public const int Is_Menu = 1;
        public const int Is_Catalog = 1;
        public const int Is_Footer = 1;
        public List<film_video> GetAll(PagingModel pageModel)
        {
            if (pageModel == null) pageModel = new PagingModel() { offset = 0, limit = 100 };
            if (pageModel.search == null) pageModel.search = "";
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<film_video>().Where(x => x.status == 2);
                query.OrderByDescending(x => x.datecreated);

                int offset = 0; try { offset = pageModel.offset; }
                catch { }
                int limit = 10;
                try { limit = pageModel.limit; }
                catch { }
                List<film_video> rows = db.Select(query)
                    .Skip(offset).Take(limit).ToList();
                return rows;
            }
        }

        public vw_film_video GetInfoById(int id)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var f = db.Select<vw_film_video>(x => x.Id == id).FirstOrDefault();
                if (f == null)
                    return new vw_film_video();

                // catalog
                var query = db.From<film_catalog>()
                    .Join<film_catalog_film>((x, y) => x.Id == y.catalogid)
                    .Where<film_catalog_film>(x => x.filmid == id)
                     .GroupBy<film_catalog>(x => x.Id)
                     .OrderByDescending<film_catalog>(x => x.position);

                f.catalogs = db.Select<film_catalog>(query)
                    .ToList();
                // comment
                var query_comment = db.From<film_comment>()
                    .Where(x => x.film_id == id)
                     .OrderByDescending(x => x.datecreated);
                f.comments = db.Select(query_comment)
                   .ToList();

                return f;
            }
        }

        public vw_film_video GetInfo(int id)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<vw_film_video>();
                query.Where(e => e.Id == id);
                return db.Select(query).SingleOrDefault();
            }
        }



        public List<vw_film_video> search(PagingModel page = null)
        {
            if (page == null) page = new PagingModel() { offset = 0, limit = 15 };
            if (page.search == null) page.search = "";


            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<vw_film_video>();
                query.Where(x => x.title.Contains(page.search) || x.actor.Contains(page.search) || x.director.Contains(page.search));
                query.Where(e => e.status == 2);
                List<vw_film_video> rows = db.Select(query)
                  .Skip(page.offset).Take(page.limit)
                  .ToList();

                return rows;
            }
        }

        public List<vw_film_video> full_search(PagingModel page = null)
        {
            if (page == null) page = new PagingModel() { offset = 0, limit = 15 };
            if (page.search == null) page.search = "";

            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<vw_film_video>();
                query.Where(x => x.title.Contains(page.search) || x.status == 2 || x.actor.Contains(page.search) || x.director.Contains(page.search));

                List<vw_film_video> rows = db.Select(query)
                  .Skip(page.offset).Take(page.limit)
                  .ToList();
                if (!rows.Any())
                    return rows;

                // catalog 
                foreach (var item in rows)
                {
                    var query_catalog = db.From<film_catalog>()
                    .Join<film_catalog_film>((x, y) => x.Id == y.catalogid)
                    .Where<film_catalog_film>(x => x.filmid == item.Id)
                     .GroupBy<film_catalog>(x => x.Id)
                     .OrderByDescending<film_catalog>(x => x.position);

                    item.catalogs = db.Select<film_catalog>(query_catalog)
                        .ToList();
                }

                return rows;
            }
        }
        public List<film_video> GetOtherVideoByEpisode(int film_id)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var f = db.Select<film_video>(x => x.Id == film_id).FirstOrDefault();
                int episode = f.episode_id;
                if (episode == 0)
                {
                    return null;
                }
                var query = db.From<film_video>();
                query.Where(x => x.episode_id == episode);
                query.OrderByDescending(x => x.episode_current);
                List<film_video> rows = db.Select(query);
                return rows;
            }
        }

        public object GetAllSong()
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<vw_film_video>();
                query.OrderByDescending(x => x.Id);
                query = query.Where(e => e.status == 1);
                List<vw_film_video> rows = db.Select(query).ToList();
                return rows;
            }
        }
        public object GetAllSongNoCheckStatus()
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<vw_film_video>();
                query.OrderByDescending(x => x.Id);
                List<vw_film_video> rows = db.Select(query).ToList();
                return rows;
            }
        }

        public List<film_video> GetViewAllItemByFilmCpId(PagingModel page, int film_cpId)
        {
            if (page == null) page = new PagingModel() { offset = 0, limit = 3 };
            if (page.search == null) page.search = " ";
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<film_video>();
                query.OrderByDescending(x => x.Id);
                int offset = 0; try { offset = page.offset; }
                catch { }
                int limit = 10;
                try { limit = page.limit; }
                catch { }
                //query = query.Where(e => (e.title.Contains(page.search)));
                query = query.Where(e => e.cpid == film_cpId);
                List<film_video> rows = db.Select(query)
                    .Skip(offset).Take(limit).ToList();
                return rows;
            }
        }

        public List<vw_film_video> GetAllViewByUserId(PagingModel page, int userId)
        {
            if (page == null) page = new PagingModel() { offset = 0, limit = 6 };
            if (page.search == null) page.search = " ";
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<vw_film_video>();
                query.OrderByDescending(x => x.Id);
                int offset = 0; try { offset = page.offset; }
                catch { }
                int limit = 10;
                try { limit = page.limit; }
                catch { }
                query = query.Where(e => e.userid == userId);
                query = query.Where(e => e.status == 1);
                List<vw_film_video> rows = db.Select(query)
                    .Skip(offset).Take(limit).ToList();
                return rows;
            }
        }

        public List<vw_film_video> GetAllViewByCPID(PagingModel page, int cpId)
        {
            if (page == null) page = new PagingModel() { offset = 0, limit = 6 };
            if (page.search == null) page.search = " ";
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<vw_film_video>();
                query.OrderByDescending(x => x.Id);
                int offset = 0; try { offset = page.offset; }
                catch { }
                int limit = 10;
                try { limit = page.limit; }
                catch { }
                query = query.Where(e => e.cpid == cpId);
                query = query.Where(e => e.status == 1);
                List<vw_film_video> rows = db.Select(query)
                    .Skip(offset).Take(limit).ToList();
                return rows;
            }
        }

        public film_video InitEmpty()
        {
            var obj = new film_video();
            obj.Id = 0;
            return obj;
        }
        public int find_indb(film_video obj, int userid)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<film_video>().Where(e => e.upload_file.Contains(obj.upload_file));
                query.Where(e => e.userid == userid);
                var objUpdate = db.Select(query).SingleOrDefault();
                if (objUpdate != null)
                {
                    return 1;
                }
                return -1;
            }
        }

        public int UpdateOrInsert(film_video obj)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                if (obj.Id > 0)
                {
                    var query = db.From<film_video>().Where(e => e.Id == obj.Id);
                    var objUpdate = db.Select(query).SingleOrDefault();
                    if (objUpdate != null)
                    {
                        objUpdate.Id = obj.Id;
                        objUpdate.title = obj.title;
                        objUpdate.desc = obj.desc;
                        objUpdate.lyric = obj.lyric;
                        objUpdate.datecreated = obj.datecreated;
                        objUpdate.userid = obj.userid;
                        objUpdate.actor = obj.actor;
                        objUpdate.director = obj.director;
                        objUpdate.catalog_id = obj.catalog_id;
                        objUpdate.upload_file = obj.upload_file;
                        objUpdate.thumb_file = obj.thumb_file;
                        objUpdate.status = obj.status;
                        objUpdate.cpid = obj.cpid;
                        return db.Update(objUpdate);
                    }
                    return -1;
                }
                else
                {
                    var objUpdate = InitEmpty();
                    objUpdate.Id = obj.Id;
                    objUpdate.title = obj.title;
                    objUpdate.desc = obj.desc;
                    objUpdate.lyric = obj.lyric;
                    objUpdate.datecreated = obj.datecreated;
                    objUpdate.userid = obj.userid;
                    objUpdate.actor = obj.actor;
                    objUpdate.director = obj.director;
                    objUpdate.catalog_id = obj.catalog_id;
                    objUpdate.upload_file = obj.upload_file;
                    objUpdate.thumb_file = obj.thumb_file;
                    objUpdate.status = obj.status;
                    objUpdate.cpid = obj.cpid;
                    return (int)db.Insert(objUpdate, selectIdentity: true);
                }
            }
        }
    }
}

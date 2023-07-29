using copyrights_fe.Model;
using copyrights_fe.Services.Connection;
using copyrights_fe.Services.Utilities;
using Microsoft.IdentityModel.Tokens;
using ServiceStack.OrmLite;

namespace copyrights_fe.Services
{
    public class film_cpServices : AppConnection
    {
        private film_cp InitEmpty()
        {
            var obj = new film_cp();
            obj.Id = 0;
            return obj;
        }
        public List<film_cp> GetAllItem(PagingModel page)
        {
            if (page == null) page = new PagingModel() { offset = 0, limit = 100 };
            if (page.search == null) page.search = "";
            if (page.catalog_id.ToString() == null) { };
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<film_cp>();
                query.OrderByDescending(x => x.Id);
                int offset = 0; try { offset = page.offset; }
                catch { }
                int limit = 10;
                try { limit = page.limit; }
                catch { }
                query = query.Where(e => (e.title.Contains(page.search)));
                List<film_cp> rows = db.Select(query)
                    .Skip(offset).Take(limit).ToList();
                return rows;
            }
        }
        public List<vw_film_cp> GetAllViewItem(PagingModel page)
        {
            if (page == null) page = new PagingModel() { offset = 0, limit = 100 };
            if (page.search == null) page.search = "";

            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<vw_film_cp>();
                query.OrderByDescending(x => x.Id);
                int offset = 0; try { offset = page.offset; }
                catch { }
                int limit = 10;
                try { limit = page.limit; }
                catch { }
                //query = query.Where(e => (e.fullname.Contains(page.search)));
                List<vw_film_cp> rows = db.Select(query)
                    .Skip(offset).Take(limit).ToList();
                return rows;
            }
        }
        public List<vw_film_cp> GetViewAllByCatalogId(/*Nullable<int> catalog_id,*/ PagingModel page)
        {
            if (page == null) page = new PagingModel() { offset = 0, limit = 100 };
            if (page.search == null) page.search = "";

            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<vw_film_cp>();
                query.OrderByDescending(x => x.Id);
                int offset = 0; try { offset = page.offset; }
                catch { }
                int limit = 10;
                try { limit = page.limit; }
                catch { }
                query = query.Where(e => (e.title.Contains(page.search)));
                if (page.catalog_id.ToString() == null)
                {

                }
                else
                {
                    query = query.Where(e => e.catalog_id == page.catalog_id);
                }
                List<vw_film_cp> rows = db.Select(query)
                    .Skip(offset).Take(limit).ToList();
                return rows;
            }
        }
        public long CountAll(PagingModel page)
        {
            if (page.search == null) page.search = "";
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<film_cp>();
                int offset = 0; try { offset = page.offset; }
                catch { }

                int limit = 10;
                try { limit = page.limit; }
                catch { }
                query = query.Where(e => (e.title.Contains(page.search)));
                return db.Count(query);
            }
        }
        public vw_film_cp GetViewInfoById(string id)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var vw_Art = db.Select<vw_film_cp>(x => x.Id == int.Parse(id)).FirstOrDefault();
                //if (f == null)
                //{
                //    return new vw_film_cp();
                //}
                return vw_Art;
            }
        }
        public vw_film_cp GetViewByShortname(string shortname)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var vw_Art = db.Select<vw_film_cp>(x => x.shortname == shortname).LastOrDefault();
                return vw_Art;
            }
        }

        public vw_film_cp searchByUrl(string url)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<vw_film_cp>();
                List<vw_film_cp> rows = db.Select(query).ToList();
                var art = rows.Where(x => Utilities.CommService.ConvertTextToSlug(x.shortname) == url).FirstOrDefault();
                return art;
            }
        }


        public film_cp get(int id, string title)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<film_cp>();
                if (id > 0) { query = query.Where(e => e.Id == id); }
                if (!string.IsNullOrEmpty(title)) { query = query.Where(e => e.title == title); }
                var rows = db.Select(query).FirstOrDefault();
                return rows;
            }
        }
    }
}

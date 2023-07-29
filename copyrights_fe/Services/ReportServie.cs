using copyrights_fe.Model;
using copyrights_fe.Services.Connection;
using copyrights_fe.Services.Utilities;
using ServiceStack;
using ServiceStack.OrmLite;

namespace copyrights_fe.Services
{
    public class reportService : AppConnection
    {

        private report InitEmpty()
        {
            var obj = new report();
            obj.Id = 0;
            return obj;
        }

        public List<report> GetAllItem(PagingModel page)
        {
            if (page == null) page = new PagingModel() { offset = 0, limit = 100 };
            if (page.search == null) page.search = "";
            if (page.catalog_id.ToString() == null) { };
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<report>();
                query.OrderByDescending(x => x.Id);
                int offset = 0; try { offset = page.offset; }
                catch { }
                int limit = 10;
                try { limit = page.limit; }
                catch { }
                query = query.Where(e => (e.link.Contains(page.search)));
                List<report> rows = db.Select(query)
                    .Skip(offset).Take(limit).ToList();
                return rows;
            }
        }
        public List<vw_report> GetAllViewItem(PagingModel page)
        {
            if (page == null) page = new PagingModel() { offset = 0, limit = 100 };
            if (page.search == null) page.search = "";

            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<vw_report>();
                query.OrderByDescending(x => x.Id);
                int offset = 0; try { offset = page.offset; }
                catch { }
                int limit = 10;
                try { limit = page.limit; }
                catch { }
                //query = query.Where(e => (e.fullname.Contains(page.search)));
                List<vw_report> rows = db.Select(query)
                    .Skip(offset).Take(limit).ToList();
                return rows;
            }
        }
        public List<vw_report> GetVw_ReportsByUserId(PagingModel page, int userId)
        {
            if (page == null) page = new PagingModel() { offset = 0, limit = 100 };
            if (page.search == null) page.search = "";

            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<vw_report>();
                query.OrderByDescending(x => x.Id);
                int offset = 0; try { offset = page.offset; }
                catch { }
                int limit = 10;
                try { limit = page.limit; }
                catch { }
                query = query.Where(e => (e.link.Contains(page.search)));
                query = query.Where(e => (e.userid == userId));
                List<vw_report> rows = db.Select(query)
                    .Skip(offset).Take(limit).ToList();
                return rows;
            }
        }

        public long CountAll(PagingModel page)
        {
            if (page.search == null) page.search = "";
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<report>();
                int offset = 0; try { offset = page.offset; }
                catch { }

                int limit = 10;
                try { limit = page.limit; }
                catch { }
                query = query.Where(e => (e.link.Contains(page.search)));
                return db.Count(query);
            }
        }

        public vw_report GetViewInfoById(string id)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var vw_Art = db.Select<vw_report>(x => x.Id == int.Parse(id)).FirstOrDefault();
                //if (f == null)
                //{
                //    return new vw_report();
                //}
                return vw_Art;
            }
        }
    }
}

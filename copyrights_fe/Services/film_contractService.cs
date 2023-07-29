using copyrights_fe.Model;
using copyrights_fe.Services.Connection;
using copyrights_fe.Services.Utilities;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Legacy;

namespace copyrights_fe.Services
{
    public class film_contractService : AppConnection
    {
        public List<film_contract> GetAll(PagingModel pageModel)
        {
            if (pageModel == null) pageModel = new PagingModel() { offset = 0, limit = 100 };
            if (pageModel.search == null) pageModel.search = "";
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<film_contract>().Where(x => x.status == 2);
                query.OrderByDescending(x => x.date_created);

                int offset = 0; try { offset = pageModel.offset; }
                catch { }
                int limit = 10;
                try { limit = pageModel.limit; }
                catch { }
                List<film_contract> rows = db.Select(query)
                    .Skip(offset).Take(limit).ToList();
                return rows;
            }
        }
        public List<vw_film_contract> GetAllByUserId(PagingModel pageModel, int userid)
        {
            if (pageModel == null) pageModel = new PagingModel() { offset = 0, limit = 100 };
            if (pageModel.search == null) pageModel.search = "";
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<vw_film_contract>().Where(x => x.status == 1);
                query.OrderByDescending(x => x.date_created);

                int offset = 0; try { offset = pageModel.offset; }
                catch { }
                int limit = 10;
                try { limit = pageModel.limit; }
                catch { }
                query.Where(e => e.userid == userid);
                List<vw_film_contract> rows = db.Select(query).Skip(offset).Take(limit).ToList();
                return rows;
            }
        }
        public film_contract InitEmpty()
        {
            var obj = new film_contract();
            obj.Id = 0;
            return obj;
        }
        public int UpdateOrInsert(film_contract obj)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                if (obj.Id > 0)
                {
                    var query = db.From<film_contract>().Where(e => e.Id == obj.Id);
                    var objUpdate = db.Select(query).SingleOrDefault();
                    if (objUpdate != null)
                    {
                        objUpdate.Id = obj.Id;
                        objUpdate.title = obj.title;
                        objUpdate.userid = obj.userid;
                        objUpdate.status = obj.status;
                        objUpdate.upload_file = obj.upload_file;
                        objUpdate.thumb_file = obj.thumb_file;
                        objUpdate.date_updated = DateTime.Now;
                        return db.Update(objUpdate);
                    }
                    return -1;
                }
                else
                {
                    var objUpdate = InitEmpty();
                    objUpdate.Id = obj.Id;
                    objUpdate.title = obj.title.ToString();
                    objUpdate.userid = obj.userid;
                    objUpdate.status = obj.status;
                    objUpdate.upload_file = obj.upload_file;
                    objUpdate.thumb_file = obj.thumb_file;
                    objUpdate.date_created = DateTime.Now;
                    objUpdate.date_updated = DateTime.Now;
                    return (int)db.Insert(objUpdate, selectIdentity: true);
                }
            }
        }
        public vw_film_contract GetInfoById(int id)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<vw_film_contract>().Where(e => e.Id == id);
                return db.Select(query).SingleOrDefault();
            }
        }
    }
}

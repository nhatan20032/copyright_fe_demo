using copyrights_fe.Services.Connection;
using lamlt.data;
using ServiceStack.OrmLite;

namespace lamlt.webservice.Services
{
    public class web_configService : AppConnection
    {
        public string GetBykey(string key)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<web_config>().Where(e => e.key == key);
                return db.Select(query).LastOrDefault().value;
            }
        }
        public List<web_config> Get(int id, string key)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<web_config>();
                if (id > 0) { query = query.Where(e => e.id == id); }
                if (!string.IsNullOrEmpty(key)) { query = query.Where(e => e.key == key); }
                return db.Select(query).ToList();
            }
        }

        internal object Delete(int id)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<web_config>();
                if (id > 0) { query = query.Where(e => e.id == id); }
                return db.Delete(query);
            }
        }

        public int UpdateBykey(string key, string value)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<web_config>().Where(e => e.key == key);
                web_config config = db.Select(query).LastOrDefault();
                if (config == null)
                {
                    config = new web_config
                    {
                        key = key,
                        value = value
                    };
                    return (int)db.Insert(config, selectIdentity: true);
                }
                config.value = value;
                return db.Update(config);
            }
        }
    }
}

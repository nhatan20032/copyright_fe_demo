using copyrights_fe.App;
using copyrights_fe.Model;
using copyrights_fe.Services.Connection;
using ServiceStack.OrmLite;

namespace copyrights_fe.Services
{
    public class AccountService : AppConnection
    {
        public AccountService() { }
        public film_user UpdateOrInsertFromSSO(film_user obj)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<film_user>().Where(e => e.username == obj.username || e.email == obj.email);
                var objCurrent = db.Select(query).LastOrDefault();
                if (objCurrent != null)
                {
                    objCurrent.token = obj.token;
                    objCurrent.note = obj.note;
                    db.Update(objCurrent);
                    return objCurrent;
                }
                else
                {
                    var objInit = InitEmpty();
                    objInit.username = obj.username;
                    objInit.password = Utilities.Md5.md5(obj.password);
                    objInit.fullname = obj.fullname;
                    objInit.email = obj.email;
                    objInit.type = obj.type;
                    objInit.datecreated = DateTime.Now;
                    objInit.token = obj.token;
                    objInit.note = obj.note;
                    objInit.ssoid = obj.ssoid;
                    switch (obj.type)
                    {
                        case "artist":
                            film_cp cp = new film_cp
                            {
                                title = obj.fullname,
                                shortname = obj.username,
                                owner_name = obj.fullname
                            };
                            int rscp = (int)db.Insert(cp, selectIdentity: true);
                            break;
                        case "guest":
                        default:
                            break;
                    }
                    int rs = (int)db.Insert(objInit, selectIdentity: true);
                    if (rs > 0)
                    {
                        objInit.Id = rs;
                        return objInit;
                    }
                    return null;
                }
            }
        }
        public film_user CheckLoginSSO(string token, string username, string scope, string ssoid)
        {

            if (string.IsNullOrEmpty(token)) token = "";
            if (string.IsNullOrEmpty(username)) username = "";
            if (string.IsNullOrEmpty(scope)) scope = "";
            if (string.IsNullOrEmpty(ssoid)) ssoid = "";
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<film_user>().Where(e => e.username == username);
                film_user user = db.Select(query).FirstOrDefault();
                if (user != null)
                {
                    Comm.SetUserId(user.Id);
                    Comm.SetUserName(user.username);
                    Comm.SetUserFullName(user.fullname);
                    Comm.SetUser(user);
                    Comm.SetToken(token);
                    Comm.SetPhone(user.phone);
                    return user;
                }
                return null;
            }
        }

        public film_user InitEmpty()
        {
            var obj = new film_user();
            obj.Id = 0;
            return obj;
        }
    }
}
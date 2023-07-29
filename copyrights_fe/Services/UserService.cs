using copyrights_fe.Model;
using copyrights_fe.Services.Connection;
using lamlt.webservice.Services;
using ServiceStack.OrmLite;

namespace copyrights_fe.Services
{
    public class UserService : AppConnection
    {

        public vw_film_user getInfo(int userId)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<vw_film_user>();
                query.Where(x => x.Id == userId);
                vw_film_user user = db.Select(query).SingleOrDefault();
                return user;
            }
        }

        public film_user getUserName(string userName)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<film_user>();
                query.Where(x => x.username == userName);
                film_user user = db.Select(query).FirstOrDefault();
                return user;
            }
        }

        public bool checkPassword(film_user user, string password)
        {
            var md5_password = copyrights_fe.Services.Utilities.Md5.md5(password);
            if (user.password == md5_password)
                return true;
            return false;

        }

        public bool checkUsername(string username)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<film_user>();
                query.Where(x => x.username == username);
                film_user row = db.Select(query).FirstOrDefault();
                if (row == null)
                    return false;
                return true;

            }
        }

        public object insert(string fullname, string username, string password, string type, string email)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                // Check tồn tại tài khoản
                var q = db.From<film_user>().Where(e => e.username == username || e.email == email);
                var r = db.Select(q).LastOrDefault();
                if (r != null)
                {
                    return "duplicate";
                }
                var md5_password = Utilities.Md5.md5(password);
                film_user user = new film_user
                {
                    username = username,
                    password = md5_password,
                    fullname = fullname,
                    email = email,
                    type = type,
                    datecreated = DateTime.Now
                };
                switch (type)
                {
                    case "artist":
                        film_cp cp = new film_cp
                        {
                            title = fullname,
                            shortname = username,
                            owner_name = fullname
                        };
                        int rscp = (int)db.Insert(cp, selectIdentity: true);
                        break;
                    case "guest":
                    default:
                        break;
                }
                int rs = (int)db.Insert(user, selectIdentity: true);
                if (rs > 0)
                {
                    user.Id = rs;
                    return user;
                }
                return null;
            }
        }
        public film_user inserts(string username, string sub_type)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                film_user user = new film_user();
                user.username = username;
                user.fullname = username;
                // user.sub_type = sub_type;

                var id = db.Insert(user);
                if (id > 0)
                {
                    user.Id = (int)id;
                    return user;
                }
                return null;

            }
        }
        public bool update(film_user model)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                film_user user = db.Select<film_user>(x => x.username == model.username).LastOrDefault();
                if (!string.IsNullOrEmpty(model.password)) { user.password = Helpers.HelpUtils.ToMd5(model.password); }
                if (!string.IsNullOrEmpty(model.fullname)) { user.fullname = model.fullname; }
                if (!string.IsNullOrEmpty(model.email)) { user.email = model.email; }
                if (!string.IsNullOrEmpty(model.phone)) { user.phone = model.phone; }
                if (model.gender > 0) { user.gender = model.gender; }
                if (model.birthday > DateTime.MinValue) { user.birthday = model.birthday; }
                var c = db.Update(user);
                copyrights_fe.App.Comm.SetUser(user);
                copyrights_fe.App.Comm.SetUserFullName(user.fullname);
                copyrights_fe.App.Comm.SetPhone(user.phone);
                return c > 0;
            }

        }
        public bool changePassword(film_user model, string password)
        {
            Console.WriteLine(password);
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                //try
                //{
                film_user user = db.Select<film_user>(x => x.username == model.username).FirstOrDefault();
                var md5_new = copyrights_fe.Services.Utilities.Md5.md5(password);
                user.password = md5_new;

                var c = db.Update(user);
                return c > 0;
                //}
                //catch(Exception ex)
                //{
                //    Console.WriteLine(ex.ToString());
                //    Console.WriteLine(ex.StackTrace);
                //        return false;
                //}

            }
        }

        public long CountAllFilmVideoView(int userid)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                //var query = db.From<film_video_view>().Where(e => e.videoid == videoId).GroupBy(e => e.userid == userid).Select(e => e.videoid);
                var query = db.From<film_video_view>().Where(e => e.userid == userid);
                return db.Count(query);
            }
        }

        public List<vw_film_video_view> GetAllByUserId(int userid)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<vw_film_video_view>().Where(e => e.userid == userid);
                List<vw_film_video_view> rows = db.Select(query).ToList();
                return rows;
            }
        }

        internal object Forget(string email)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<film_user>().Where(e => e.email == email);
                var rows = db.Select(query).ToList();
                if (rows.Count > 0)
                {
                    try
                    {
                        web_configService myconfig = new web_configService();
                        string user = myconfig.GetBykey("lalatv_gmail_user");
                        string pass = myconfig.GetBykey("lalatv_gmail_pass");
                        string newpass = HelpUtil.RandomString(6, true);
                        film_user newInfo = rows[0];
                        newInfo.password = Helpers.HelpUtils.ToMd5(newpass);
                        db.Update(newInfo);
                        var body = "Chào <b>" + email + "</b>, ";
                        body += "chúng tôi đã xác nhận có một sự thay đổi về mật khẩu của bạn vào lúc <b>" + DateTime.Now.ToString("HH:ss:mm") + " ngày " + DateTime.Now.ToString("dd/mm/yyyy") + "</b><br /><br />"; ;
                        body += "Mật khẩu mới <span style='color:red;font-size:16px;font-weight:600;border:1px solid;padding:10px;'>" + newpass + "</span>";
                        body += "<br /><br />Vui lòng đăng nhập và thay đổi lại mật khẩu qua đường dẫn sau: <a href='https://copyrights.vn/Login' tagget='_blank'>Đăng nhập</a><br /><br />";
                        body += "Xin cảm ơn bạn!";
                        HelpUtil.SendEmail(user, pass, email, "Mật khẩu khôi phục copyrights.vn của bạn", body, null);
                        return "Thành công";
                    }
                    catch (Exception)
                    {
                        return "Lỗi gửi email do tài khoản của hệ thống";
                    }
                }
                return null;
            }
        }
    }
}

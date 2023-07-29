using Microsoft.AspNetCore.Mvc;
using copyrights_fe.App;
using copyrights_fe.Model;
using copyrights_fe.Services;

namespace copyrights_fe.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly Services.UserService _userSv;
        public AccountController(IConfiguration configuration)
        {
            _config = configuration;
            _userSv = new Services.UserService();
        }
        //Thông tin cá nhân
        [Route("account/thong-tin-ca-nhan")]
        [copyrights_fe.Helpers.User]
        public IActionResult _AccountLayout()
        {
            ViewData["Title"] = "Thông tin cá nhân";
            return View();
        }

        #region ========Login========
        [Route("account/login")]
        [Route("login")]
        [HttpGet]
        public IActionResult AccLogin()
        {
            return View();
        }

        [Route("account/login")]
        [Produces("application/json")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Login()
        {
            if (Comm.GetUserId() > 0)
            {
                return new JsonResult(JsonResponse.success("login", new
                {
                    id = Comm.GetUserId(),
                    token = Comm.GetToken(),
                    username = Comm.GetUser(),
                    phone = Comm.GetPhone(),
                    href = "/account/thong-tin-ca-nhan"
                }));
            }
            var user_name = Request.Form["username"];
            var password = Request.Form["password"];
            string usernamme = user_name;

            var user = _userSv.getUserName(usernamme);
            if (user != null)
            {
                var checkPwd = _userSv.checkPassword(user, password);
                if (checkPwd)
                {
                    var token = Comm.GenerateToken(user, _config["Tokens:Key"], _config["Tokens:Issuer"], _config["Tokens:Expires"]);
                    Comm.SetUserId(user.Id);
                    Comm.SetUserName(user.username);
                    Comm.SetUserFullName(user.fullname);
                    Comm.SetUser(user);
                    Comm.SetToken(token);
                    Comm.SetPhone(user.phone);
                    return new JsonResult(JsonResponse.success("success", new { userid = user.Id, token, href = "/account/thong-tin-ca-nhan" }));
                }
            }
            return new JsonResult(JsonResponse.error("Could not create token!"));
        }
        [Route("loginapi")]
        [HttpPost]
        public JsonResult loginapi(string username, string password)
        {
            return new JsonResult(new { code = 200, message = "success", username = username, password = password });
        }
        [Route("/LoginFromSSO")]
        [HttpPost]
        public ActionResult LoginFromSSO(string token, string username, string scope, string ssoid)
        {
            if (Comm.GetUserId() > 0)
            {
                return new JsonResult(JsonResponse.success("login", new
                {
                    id = Comm.GetUserId(),
                    token = Comm.GetToken(),
                    username = Comm.GetUser(),
                    phone = Comm.GetPhone(),
                    href = "/account/thong-tin-ca-nhan"
                }));
            }
            //var user_name = Request.Form["username"];
            //var password = Request.Form["password"];
            //string usernamme = user_name;

            film_user user = new AccountService().CheckLoginSSO(token, username, scope, ssoid);
            if (user != null)
            {
                //var _token = Comm.GenerateToken(user, _config["Tokens:Key"], _config["Tokens:Issuer"], _config["Tokens:Expires"]);
                return new JsonResult(JsonResponse.success("success", new { userid = user.Id, token, href = "/account/thong-tin-ca-nhan" }));
            };
            return new JsonResult(JsonResponse.error("Could not create token!"));
        }
        [Route("/Web/LoginSSO")]
        public PartialViewResult LoginSSO()
        {
            return PartialView("LoginSSO");
        }

        [Route("account/logout")]
        public IActionResult logout()
        {
            Comm.ClearSession();
            //return View("Index");
            return new JsonResult(JsonResponse.success("longld"));
        }
        [HttpGet]
        [Route("account/get")]
        public JsonResult get(int id, string username)
        {
            var user = _userSv.getUserName(username);
            return Json(new { user });
        }

        [Route("account/register")]
        [Produces("application/json")]
        [HttpPost]
        public JsonResult register(string fullname, string username, string password, string type, string email)
        {
            var model = _userSv.insert(fullname, username, password, type, email);
            if (model != null)
            {
                if (model.ToString() == "duplicate")
                {
                    return Json("duplicate");
                }
                else
                {
                    var user = (film_user)model;
                    string token = Comm.GenerateToken(user, _config["Tokens:Key"], _config["Tokens:Issuer"], _config["Tokens:Expires"]);
                    Comm.SetUserId(user.Id);
                    Comm.SetUserName(user.username);
                    Comm.SetUserFullName(user.fullname);
                    Comm.SetUser(user);
                    Comm.SetToken(token);
                    return Json(new { message = "Đăng ký tài khoản thành công.", id = user.Id, token = token });
                }
            }
            return Json("Lỗi hệ thống đăng ký vui lòng thử lại sau!");
        }
        #endregion

        #region =====Info=====
        [HttpGet]
        [Route("account/info")]
        public PartialViewResult AccInfo(string username)
        {
            var userInfo = new Services.UserService().getUserName(username);
            return PartialView("AccInfo", userInfo);
        }

        #endregion  =====Info=====

        #region =====List Song=====
        [HttpGet]
        [Route("account/list-song")]
        public PartialViewResult AccListSong(string username)
        {
            var userInfo = new Services.film_cpServices().GetViewByShortname(username);
            if (userInfo != null)
            {
                var listSong = new Services.FilmService().GetAllViewByCPID(null, userInfo.Id);
                return PartialView("AccListSong", listSong);
            }
            return PartialView("AccListSong", null);
        }
        #endregion =====List Song=====

        #region =====ALL SONG=====
        [HttpGet]
        [Route("account/allsong")]
        public PartialViewResult AllSong(string username)
        {
            var listSong = new Services.FilmService().GetAllSongNoCheckStatus();
            ViewData["userid"] = Comm.GetUserId();
            return PartialView("AllSong", listSong);
        }
        #endregion =====List Song=====

        #region =====Report=====
        [HttpGet]
        [Route("account/report")]
        public PartialViewResult AccReport(string username)
        {
            var userReport = new Services.UserService().getUserName(username);
            var _reportSv = new Services.reportService();
            var reportByUserId = _reportSv.GetVw_ReportsByUserId(null, userReport.Id);
            return PartialView("AccReport", reportByUserId);
        }
        #endregion =====Report=====

        #region =====Upload File=====
        [HttpGet]
        [Route("account/upload-file")]
        public PartialViewResult AccUploadFile(string username)
        {
            //var userInfo = new Services.UserService().getUserName(username);
            var _filmSV = new Services.FilmService();
            var initFilm = _filmSV.InitEmpty();
            return PartialView("AccUploadFile", initFilm);
        }

        [Route("account/upload_file")]
        [HttpPost]
        //[DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            { return Content("file not selected"); }
            string destinationPath = file.FileName /*+ "_" + DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName)*/;
            string sourcePath = "/home/amnhacsaigon/musics/uploads";
            //string sourcePath = "H:\\New folder";
            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), sourcePath, destinationPath);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Json(new { file = "uploads/" + destinationPath });
        }

        #endregion =====Upload File=====

        #region =====Contract=====
        [HttpGet]
        [Route("/account/contract")]
        public PartialViewResult AccContract(string username)
        {
            var user = new Services.UserService().getUserName(username);
            var _film_contractSv = new Services.film_contractService();
            var film_contractByUser = _film_contractSv.GetAllByUserId(null, user.Id);
            return PartialView("AccContract", film_contractByUser);
        }

        [Route("account/contract_upload_view")]
        [HttpGet]
        public ViewResult AccContract_UploadFile([FromQuery(Name = "t")] string t, [FromQuery(Name = "callback")] string callback)
        {
            dynamic mymodel = new System.Dynamic.ExpandoObject();
            mymodel.t = t;
            mymodel.callback = callback;
            return View("AccContract_UploadFile", mymodel);
        }
        [HttpGet]
        [Route("account/forget")]
        public string forget(string email)
        {
            try
            {
                var user = _userSv.Forget(email);
                return user == null ? "Lỗi Email không tồn tại" : "Vui lòng kiểm tra email, mật khẩu đã được gửi vào email của bạn";
            }
            catch (System.Exception)
            {
                return "Lỗi Email không tồn tại hoặc không khôi phục được mật khẩu";
            }
        }
        [HttpGet]
        [Route("account/update_info")]
        public string update_info(string username, string password, string phone, int gender, DateTime birthday, string fullname, string email)
        {
            try
            {
                film_user objuser = new film_user()
                {
                    username = username,
                    password = password,
                    email = email,
                    phone = phone,
                    fullname = fullname,
                    birthday = birthday,
                    gender = gender,
                };
                bool obj = _userSv.update(objuser);
                return "Cập nhập  " + (obj ? "thành công" : "thất bại");
            }
            catch (System.Exception)
            {
                return "Lỗi cập nhật thông tin cá nhân";
            }
        }
        #endregion =====Contract=====
    }
}

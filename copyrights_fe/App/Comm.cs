using Microsoft.IdentityModel.Tokens;
using ServiceStack;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace copyrights_fe.App
{
    public static class Comm
    {
        public static readonly int ERROR_NOT_EXIST = -3;
        public static readonly int ERROR_EXIST = -2;
        public static readonly int ERROR_GENERAL = -1;

        public static int GetUserId()
        {
            if (copyrights_fe.App.AppContext.Current == null) return -1;
            if (copyrights_fe.App.AppContext.Current.Session == null) return -1;
            if (copyrights_fe.App.AppContext.Current.Session.GetInt32(Model.Config.SESSION_USERID) == null) return -1;
            return copyrights_fe.App.AppContext.Current.Session.GetInt32(Model.Config.SESSION_USERID) ?? -1;
        }

        public static copyrights_fe.Model.film_user GetUser()
        {
            if (copyrights_fe.App.AppContext.Current == null) return null;
            if (copyrights_fe.App.AppContext.Current.Session == null) return null;
            if (copyrights_fe.App.AppContext.Current.Session.GetString(Model.Config.SESSION_USER) == null) return null;
            var str = AppContext.Current.Session.GetString(Model.Config.SESSION_USER).ToString();
            // var str1 = (Lala.App.AppContext.Current.Session.Id);
            //return Newtonsoft.Json.JsonConvert.DeserializeObject<Lala.Model.film_user>(str);
            return str.FromJson<copyrights_fe.Model.film_user>();
        }
        //get user=>session theo id
        public static copyrights_fe.Model.film_user GetSession()
        {
            if (copyrights_fe.App.AppContext.Current == null) return null;
            if (copyrights_fe.App.AppContext.Current.Session == null) return null;
            if (copyrights_fe.App.AppContext.Current.Session.GetString(Model.Config.SESSION_Id) == null) return null;
            var str = copyrights_fe.App.AppContext.Current.Session.Id;
            return str.FromJson<copyrights_fe.Model.film_user>();
        }
        public static string GetUserName()
        {
            if (copyrights_fe.App.AppContext.Current == null) return "";
            if (copyrights_fe.App.AppContext.Current.Session.GetString(Model.Config.SESSION_USERNAME) == null) return "";
            return copyrights_fe.App.AppContext.Current.Session.GetString(Model.Config.SESSION_USERNAME).ToString();
        }
        public static string GetUserFullName()
        {
            if (copyrights_fe.App.AppContext.Current == null) return "";
            if (copyrights_fe.App.AppContext.Current.Session.GetString(Model.Config.SESSION_USERFULLNAME) == null) return "";
            return copyrights_fe.App.AppContext.Current.Session.GetString(Model.Config.SESSION_USERFULLNAME).ToString();
        }
        public static string GetToken()
        {
            if (copyrights_fe.App.AppContext.Current == null) return "";
            if (copyrights_fe.App.AppContext.Current.Session.GetString(Model.Config.SESSION_TOKEN) == null) return "";
            return copyrights_fe.App.AppContext.Current.Session.GetString(Model.Config.SESSION_TOKEN).ToString();
        }
        public static string GetPhone()
        {
            if (copyrights_fe.App.AppContext.Current == null) return "";
            if (copyrights_fe.App.AppContext.Current.Session.GetString(Model.Config.SESSION_PHONE) == null) return "";
            return copyrights_fe.App.AppContext.Current.Session.GetString(Model.Config.SESSION_PHONE).ToString();
        }






        public static void SetUserId(int model)
        {
            copyrights_fe.App.AppContext.Current.Session.SetInt32(Model.Config.SESSION_USERID, model);
        }

        public static void SetUser(copyrights_fe.Model.film_user model)
        {
            var str = model.ToJson();
            copyrights_fe.App.AppContext.Current.Session.SetString(Model.Config.SESSION_USER, str);
        }

        public static void SetUserName(string model)
        {
            copyrights_fe.App.AppContext.Current.Session.SetString(Model.Config.SESSION_USERNAME, model);
        }
        public static void SetUserFullName(string model)
        {
            copyrights_fe.App.AppContext.Current.Session.SetString(Model.Config.SESSION_USERFULLNAME, model);
        }
        public static void SetToken(string model)
        {
            copyrights_fe.App.AppContext.Current.Session.SetString(Model.Config.SESSION_TOKEN, model);
        }
        public static void SetPhone(string phone)
        {
            copyrights_fe.App.AppContext.Current.Session.SetString(Model.Config.SESSION_PHONE, phone);
        }
        public static void ClearUserSession()
        {
            copyrights_fe.App.AppContext.Current.Session.Remove(Model.Config.SESSION_USER);
        }
        public static void ClearFullNameSession()
        {
            copyrights_fe.App.AppContext.Current.Session.Remove(Model.Config.SESSION_USERFULLNAME);
        }
        public static void ClearSession()
        {
            copyrights_fe.App.AppContext.Current.Session.Remove(Model.Config.SESSION_USER);
            copyrights_fe.App.AppContext.Current.Session.Remove(Model.Config.SESSION_USERID);
            copyrights_fe.App.AppContext.Current.Session.Remove(Model.Config.SESSION_USERNAME);
            copyrights_fe.App.AppContext.Current.Session.Remove(Model.Config.SESSION_USERFULLNAME);
            copyrights_fe.App.AppContext.Current.Session.Remove(Model.Config.SESSION_TOKEN);
            copyrights_fe.App.AppContext.Current.Session.Clear();
        }



        public static string GenerateToken(copyrights_fe.Model.film_user user, string tokens_key, string tokens_issuer, string tokens_expires)
        {
            var claims = new[]
                   {
                        new Claim("Id",  $"{user.Id}"),
                        new Claim(JwtRegisteredClaimNames.NameId, user.username),
                        new Claim(JwtRegisteredClaimNames.Sub, user?.fullname),
                        new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokens_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(tokens_issuer, tokens_issuer,
                claims,
                expires: DateTime.Now.AddMinutes(int.Parse(tokens_expires)),
                signingCredentials: creds);
            string token_key = new JwtSecurityTokenHandler().WriteToken(token);
            return token_key;
        }
        public static string SerializeObject(object value)
        {
            string sb = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            string replacement = Regex.Replace(sb, @"\\t|\\n|\\r", "");
            return replacement.ToString();
        }


    }
}

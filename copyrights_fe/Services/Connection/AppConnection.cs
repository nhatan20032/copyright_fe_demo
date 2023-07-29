using Microsoft.Extensions.Configuration;
using ServiceStack;
using ServiceStack.OrmLite;
using System;

namespace copyrights_fe.Services.Connection

{
    public interface IYourAppConnectionFactory : ServiceStack.Data.IDbConnectionFactory
    {
    }
    public class YourAppConnectionFactory : OrmLiteConnectionFactory, IYourAppConnectionFactory
    {
        public YourAppConnectionFactory(string s) : base(s) { }
        public YourAppConnectionFactory(string s, IOrmLiteDialectProvider provider) : base(s, provider) { }


    }

    public class AppConnection
    {
        protected OrmLiteConnectionFactory _connectionFilmLala;
        public AppConnection()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            var string_connect = configuration.GetConnectionString("lalatv_film");

            OrmLiteConfig.DialectProvider = MySqlDialect.Provider;
            //Insert and Update unicode with serviceStack   
            //OrmLiteConfig.DialectProvider.GetStringConverter().UseUnicode = true;

            _connectionFilmLala = new OrmLiteConnectionFactory(string_connect, OrmLiteConfig.DialectProvider, true);
        }



    }


    public class ServiceStackHelper
    {
        public static void ActivateLicensing()
        {
            var lk = new LicenseKey { Expiry = DateTime.MaxValue, Hash = string.Empty, Name = "n3t3h", Ref = "1", Type = LicenseType.Enterprise };
            Licensing.RegisterLicense(lk.ToString());
        }

    }
}

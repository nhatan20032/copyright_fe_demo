#pragma warning disable 1591

using ServiceStack.DataAnnotations;
using ServiceStack.Model;
using System;

namespace copyrights_fe.Model
{
    [Alias("film_transactions")]
    public partial class film_transactions : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string txtid { get; set; }
        public string address { get; set; }
        public double? amount1 { get; set; }
        public double? amount2 { get; set; }
        public string currency1 { get; set; }
        public string currency2 { get; set; }
        public string status_url { get; set; }
        public string qrcore_url { get; set; }
        public DateTime? datecreated { get; set; }

        public string idRef { get; set; }
        public string keyRef { get; set; }
        public string typeTrans { get; set; }
        public double? rateUSD1 { get; set; }
        public double? rateUSD2 { get; set; }
        public string addressWS { get; set; }
        public int? memberid { get; set; }
        public string txthash { get; set; }
        public int? statusWS1 { get; set; }
        public string txtHashFee { get; set; }
        public int? statusFee { get; set; }
        public int? memberidTo { get; set; }
        public string addressTo { get; set; }
        public string addressFrom { get; set; }
        public string typeName { get; set; }
        public string txtHash1 { get; set; }
        public string username { get; set; }
        public string note { get; set; }
        public string bankid { get; set; }
        public string statusBanking { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string urlTemp { get; set; }
        public string statusBankingResult { get; set; }
        public string orderIdRef { get; set; }
    }

    [Alias("vw_film_transactions")]
    public partial class vw_film_transactions
    {
        public int Id { get; set; }
        public string txtid { get; set; }
        public string address { get; set; }
        public double? amount1 { get; set; }
        public double? amount2 { get; set; }
        public string currency1 { get; set; }
        public string currency2 { get; set; }
        public string status_url { get; set; }
        public string qrcore_url { get; set; }
        public DateTime? datecreated { get; set; }

        public string idRef { get; set; }
        public string keyRef { get; set; }
        public string typeTrans { get; set; }
        public double? rateUSD1 { get; set; }
        public double? rateUSD2 { get; set; }
        public string addressWS { get; set; }
        public int? memberid { get; set; }
        public string txthash { get; set; }
        public int? statusWS1 { get; set; }
        public string txtHashFee { get; set; }
        public int? statusFee { get; set; }
        public int? memberidTo { get; set; }
        public string addressTo { get; set; }
        public string addressFrom { get; set; }
        public string typeName { get; set; }
        public string txtHash1 { get; set; }
        public string username { get; set; }
        public string note { get; set; }
        public string bankid { get; set; }
        public string statusBanking { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string urlTemp { get; set; }
        public string statusBankingResult { get; set; }
        public string orderIdRef { get; set; }

    }
}
#pragma warning restore 1591

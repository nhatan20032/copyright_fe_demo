using ServiceStack.DataAnnotations;
using ServiceStack.Model;
using System;
using System.Collections.Generic;

namespace lamlt.data
{

    [Alias("film_transactions")]
    public partial class film_transactions : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public int artist_id { get; set; }
        public int film_id { get; set; }
        public int report_id { get; set; }
        public int report_status { get; set; }
        public int source_id { get; set; }
        public int fee1 { get; set; }
        public string months1 { get; set; }
        public int fee2 { get; set; }
        public string months2 { get; set; }
        public DateTime? datecreated { get; set; }
        public DateTime? dateupdated { get; set; }
        public string payment { get; set; }
        public int user_id { get; set; }
        public string payer { get; set; }
        public int status { get; set; }
        public DateTime? date_chuyentien { get; set; }
        public DateTime? date_ky_bien_ban { get; set; }
        public DateTime? date_gui_bien_ban { get; set; }
        public DateTime? date_nhanbienban { get; set; }
        public DateTime? date_thaoco { get; set; }
        public string payer_email { get; set; }
        public string payer_phone { get; set; }
        public string payer_address { get; set; }
        public int vat { get; set; }
        public int giam_tru_chi_phi { get; set; }
        public string note { get; set; }

    }
    [Alias("vw_film_transactions")]
    public partial class vw_film_transactions
    {
        public int Id { get; set; }
        public int report_id { get; set; }
        public int report_status { get; set; }
        public string channel { get; set; }
        public string link { get; set; }
        public int film_id { get; set; }
        public string film_title { get; set; }
        public int artist_id { get; set; }
        public string artist_name { get; set; }
        public int fee1 { get; set; }
        public string months1 { get; set; }
        public int fee2 { get; set; }
        public string months2 { get; set; }
        public DateTime? datecreated { get; set; }
        public DateTime? dateupdated { get; set; }
        public string payment { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string payer { get; set; }
        public string payer_phone { get; set; }
        public string payer_address { get; set; }
        public string payer_email { get; set; }
        public int status { get; set; }
        public DateTime? date_gui_bien_ban { get; set; }
        public DateTime? date_chuyentien { get; set; }
        public DateTime? date_ky_bien_ban { get; set; }
        public DateTime? date_nhanbienban { get; set; }
        public DateTime? date_thaoco { get; set; }
        public int vat { get; set; }
        public int giam_tru_chi_phi { get; set; }
        public string note { get; set; }
        public string source { get; set; }
    }

    public class multi_transaction_export
    {
        public string date1 { get; set; }
        public string date2 { get; set; }
        public List<vw_film_transactions> list_filmtransactions { get; set; }
    }
    public class multi_transactions
    {
        public string start_time { get; set; }
        public string end_time { get; set; }
        public List<vw_film_transactions> list_filmtransactions { get; set; }
    }

    [Alias("film_transactions_log")]
    public partial class film_transactions_log : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public int transaction_id { get; set; }
        public int artist_id { get; set; }
        public int film_id { get; set; }
        public int report_id { get; set; }
        public int report_status { get; set; }
        public int source_id { get; set; }
        public int fee1 { get; set; }
        public string months1 { get; set; }
        public int fee2 { get; set; }
        public string months2 { get; set; }
        public DateTime? datecreated { get; set; }
        public DateTime? dateupdated { get; set; }
        public string payment { get; set; }
        public int user_id { get; set; }
        public string payer { get; set; }
        public int status { get; set; }
        public DateTime? date_chuyentien { get; set; }
        public DateTime? date_ky_bien_ban { get; set; }
        public DateTime? date_gui_bien_ban { get; set; }
        public DateTime? date_nhanbienban { get; set; }
        public DateTime? date_thaoco { get; set; }
        public string payer_email { get; set; }
        public string payer_phone { get; set; }
        public string payer_address { get; set; }
        public int vat { get; set; }
        public int giam_tru_chi_phi { get; set; }
        public string note { get; set; }

    }
    [Alias("vw_film_transactions_log")]
    public partial class vw_film_transactions_log
    {
        public int Id { get; set; }
        public int transaction_id { get; set; }
        public int report_id { get; set; }
        public int report_status { get; set; }
        public string channel { get; set; }
        public string link { get; set; }
        public int film_id { get; set; }
        public string film_title { get; set; }
        public int artist_id { get; set; }
        public string artist_name { get; set; }
        public int fee1 { get; set; }
        public string months1 { get; set; }
        public int fee2 { get; set; }
        public string months2 { get; set; }
        public DateTime? datecreated { get; set; }
        public DateTime? dateupdated { get; set; }
        public string payment { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string payer { get; set; }
        public string payer_phone { get; set; }
        public string payer_address { get; set; }
        public string payer_email { get; set; }
        public int status { get; set; }
        public DateTime? date_gui_bien_ban { get; set; }
        public DateTime? date_chuyentien { get; set; }
        public DateTime? date_ky_bien_ban { get; set; }
        public DateTime? date_nhanbienban { get; set; }
        public DateTime? date_thaoco { get; set; }
        public int vat { get; set; }
        public int giam_tru_chi_phi { get; set; }
        public string note { get; set; }
        public string source { get; set; }
    }
}

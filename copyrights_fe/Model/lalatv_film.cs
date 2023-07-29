using ServiceStack.DataAnnotations;
using ServiceStack.Model;
using System;

namespace copyrights_fe.Model
{
    [Alias("film_article")]
    public partial class film_article : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string url { get; set; }
        public int? ordinal { get; set; }
        public string target { get; set; }
        public int? status { get; set; }
        public DateTime? created_time { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
        public string content { get; set; }
    }
    [Alias("package")]
    public partial class package : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int? price { get; set; }
        public string note { get; set; }
        public DateTime? date_created { get; set; }
    }
    [Alias("film_banner")]
    public partial class film_banner : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        [Required]
        public string title { get; set; }
        public string redirect_url { get; set; }
        public int? position { get; set; }
        public int? prior { get; set; }
        public string file { get; set; }
        public string html { get; set; }
        public string desc { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
    }
    [Alias("film_catalog")]
    public partial class film_catalog : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string title { get; set; }
        public int? catalogid { get; set; }
        public int? position { get; set; }
        public string desc { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
    }
    [Alias("film_catalog_film")]
    public partial class film_catalog_film : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public int? catalogid { get; set; }
        public int? filmid { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
    }
    [Alias("film_channel")]
    public partial class film_channel : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
    }
    [Alias("film_comments")]
    public partial class film_comment : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        [Required]
        public int userid { get; set; }
        [Required]
        public int film_id { get; set; }
        public string full_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string content { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid_created { get; set; }
        [Required]
        public int like { get; set; }
        [Required]
        public int dislike { get; set; }
    }
    [Alias("film_contact")]
    public partial class film_contact : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public int? userid { get; set; }
        [Required]
        public string full_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string content { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid_created { get; set; }
    }
    [Alias("film_country")]
    public partial class film_country : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
    }
    [Alias("film_cp")]
    public partial class film_cp : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string title { get; set; }
        public string shortname { get; set; }
        public string address { get; set; }
        public string rep { get; set; }
        public string email { get; set; }
        public string contractname { get; set; }
        public string contractphone { get; set; }
        public string contractemail { get; set; }
        public string desc { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
        public int? status { get; set; }
        public string scan { get; set; }
        public string thumb_file { get; set; }
        public DateTime? birthday { get; set; }
        public int? gender { get; set; }
        public int? country_id { get; set; }
        public int? catalog_id { get; set; }
        public int? owner_id { get; set; }
        public int stt { get; set; }
        public string owner_name { get; set; }
    }
    [Alias("film_manufacture")]
    public partial class film_manufacture : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
        public string contact { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }
    [Alias("film_media")]
    public partial class film_medium : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
        public int? seriid { get; set; }
        public string imdb { get; set; }
        public int? format { get; set; }
        public int? sub_type { get; set; }
        public int? publish_year { get; set; }
        public int? publish_countryid { get; set; }
        public string duration { get; set; }
        public string actor { get; set; }
        public string director { get; set; }
        public int? film_type { get; set; }
        public int? episode { get; set; }
        public int? episode_current { get; set; }
        public string contract_copyright { get; set; }
        public DateTime? contract_exprired { get; set; }
        public string contract_appendix { get; set; }
        public string copyright_appendix { get; set; }
        public DateTime? copyright_expired { get; set; }
        public int? catalog_id { get; set; }
        public string upload_file { get; set; }
        public string thumb_file { get; set; }
        public int? exclusive { get; set; }
        public float? price { get; set; }
        public int? status { get; set; }
        public int? cpid { get; set; }
    }
    [Alias("film_menu")]
    public partial class film_menu : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string title { get; set; }
        public string url_rewrite { get; set; }
        public int? position { get; set; }
        public sbyte? is_header { get; set; }
        public sbyte? is_catalog { get; set; }
        public sbyte? is_footer { get; set; }
        public int? position_footer { get; set; }
        public string child_type { get; set; }
        public int? catalogid { get; set; }
        public int? parent_id { get; set; }
        public string desc { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
    }
    [Alias("film_program")]
    public partial class film_program : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
        public int? channelid { get; set; }
    }
    [Alias("film_role")]
    public partial class film_role : IHasId<int>
    {
        public string title { get; set; }
        public string code { get; set; }
        public DateTime? datecreated { get; set; }
        public string desc { get; set; }
        public int? userid { get; set; }
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
    }
    [Alias("film_seri")]
    public partial class film_seri : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
        public int? programid { get; set; }
    }
    [Alias("film_show_case")]
    public partial class film_show_case : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        [Required]
        public string title { get; set; }
        public string redirect_url { get; set; }
        public int? position { get; set; }
        [Required]
        public sbyte type { get; set; }
        [Required]
        public sbyte data_type { get; set; }
        [Required]
        public string view_type { get; set; }
        public string query { get; set; }
        public sbyte? status { get; set; }
        public string desc { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
        [Ignore]
        public string diff { get; set; }
        [Ignore]
        public string countview { get; set; }


    }
    [Alias("film_show_case_item")]
    public partial class film_show_case_item : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        [Required]
        public int show_case_id { get; set; }
        [Required]
        public int item_id { get; set; }
        public int view_id { get; set; }
        public int? position { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
        [Ignore]
        public string diff { get; set; }
        [Ignore]
        public string countview { get; set; }
    }
    [Alias("film_users")]
    public partial class film_user : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string avatar { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string fullname { get; set; }
        public string desc { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
        public int? cpid { get; set; }
        public int? roleid { get; set; }
        public int? gender { get; set; }
        public DateTime? birthday { get; set; }
        public int? sub_type { get; set; }
        public int? sub_state { get; set; }
        public string type { get; set; }
        public string token { get; set; }
        public string note { get; set; }
        public int ssoid { get; set; }
        public string ssoid_role { get; set; }
    }
    [Alias("film_users_role")]
    public partial class film_users_role : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public int? userid { get; set; }
        public int? roleid { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid_created { get; set; }
    }
    [Alias("film_video")]
    public partial class film_video : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string lyric { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
        public int? seriid { get; set; }
        public string imdb { get; set; }
        public int? format { get; set; }
        public int? sub_type { get; set; }
        public int? publish_year { get; set; }
        public int? publish_countryid { get; set; }
        public string duration { get; set; }
        public string actor { get; set; }
        public string director { get; set; }
        public int? film_type { get; set; }
        public int? episode { get; set; }
        public int? episode_current { get; set; }
        public string contract_copyright { get; set; }
        public DateTime? contract_exprired { get; set; }
        public string contract_appendix { get; set; }
        public string copyright_appendix { get; set; }
        public DateTime? copyright_expired { get; set; }
        public int? catalog_id { get; set; }
        public string upload_file { get; set; }
        public string thumb_file { get; set; }
        public int? exclusive { get; set; }
        public float? price { get; set; }
        public int? status { get; set; }
        public int? cpid { get; set; }
        public int episode_id { get; set; }
        public int showhome { get; set; }
    }
    [Alias("film_video_view")]
    public partial class film_video_view : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public int? videoid { get; set; }
        public int? custom_duration { get; set; }
        public int? userid { get; set; }
        public int? view { get; set; }
        public DateTime? datecreated { get; set; }
    }
    [Alias("vw_film_video_view")]
    public partial class vw_film_video_view
    {
        public int Id { get; set; }
        public int? videoid { get; set; }
        public int? custom_duration { get; set; }
        public string film_title { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }

    }
    [Alias("tradding")]
    public partial class tradding : IHasId<int>

    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string name { get; set; }
        public int? revenue { get; set; }
        public int? discount { get; set; }

    }
    [Alias("vw_tradding")]
    public partial class vw_tradding
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int? revenue { get; set; }
        public int? discount { get; set; }

    }
    [Alias("ads")]
    public partial class ads : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string name { get; set; }
        public int? duration { get; set; }
        public int? time_start { get; set; }
        public int? type { get; set; }
        public string url { get; set; }

    }
    [Alias("vw_ads")]
    public partial class vw_ads
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int? duration { get; set; }
        public int? time_start { get; set; }
        public int? type { get; set; }
        public string url { get; set; }

    }
    [Alias("vw_film_banner")]
    public partial class vw_film_banner
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string title { get; set; }
        public int? position { get; set; }
        public int? prior { get; set; }
        public string file { get; set; }
        public string html { get; set; }
        public string desc { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
    }
    [Alias("vw_film_catalog")]
    public partial class vw_film_catalog
    {
        [Required]
        public int id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
        public int? catalogid { get; set; }
        public string catalogid_title { get; set; }
    }
    [Alias("vw_film_catalog_film")]
    public partial class vw_film_catalog_film
    {
        [Required]
        public int Id { get; set; }
        public int? catalogid { get; set; }
        public int? filmid { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
        public string catalogid_title { get; set; }
        public string filmid_title { get; set; }
    }
    [Alias("vw_film_channel")]
    public partial class vw_film_channel
    {
        [Required]
        public int Id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
    }
    [Alias("vw_film_comment")]
    public partial class vw_film_comment
    {
        [Required]
        public int film_id { get; set; }
        public string film_title { get; set; }
        public string thumb_file { get; set; }
        [Required]
        public int comment_id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string content { get; set; }
        [Required]
        public int user_id { get; set; }
        public string user_fullname { get; set; }
    }
    [Alias("vw_film_country")]
    public partial class vw_film_country
    {
        [Required]
        public int Id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
    }
    [Alias("vw_film_cp")]
    public partial class vw_film_cp
    {
        [Required]
        public int Id { get; set; }
        public string title { get; set; }
        public string shortname { get; set; }
        public string address { get; set; }
        public string rep { get; set; }
        public string email { get; set; }
        public string contractname { get; set; }
        public string contractphone { get; set; }
        public string contractemail { get; set; }
        public string desc { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
        public int? status { get; set; }
        public string scan { get; set; }
        public string thumb_file { get; set; }
        public DateTime? birthday { get; set; }
        public int? gender { get; set; }
        public int? country_id { get; set; }
        public string country_title { get; set; }
        public int? catalog_id { get; set; }
        public string catalog_title { get; set; }
        public int? owner_id { get; set; }
        public int stt{ get; set; }
        public string owner_name { get; set; }
    }
    [Alias("vw_film_role")]
    public partial class vw_film_role
    {
        public string title { get; set; }
        public string code { get; set; }
        public DateTime? datecreated { get; set; }
        public string desc { get; set; }
        public int? userid { get; set; }
        [Required]
        public int Id { get; set; }
    }
    [Alias("vw_film_user")]
    public partial class vw_film_user
    {
        [Required]
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string fullname { get; set; }
        public string desc { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
        public int? cpid { get; set; }
        public int? roleid { get; set; }
        public string cpid_title { get; set; }
        public string roleid_title { get; set; }
        public string token { get; set; }
        public string note { get; set; }
        public int ssoid { get; set; }
        public string ssoid_role { get; set; }
    }
    [Alias("vw_film_video")]
    public partial class vw_film_video
    {
        [Required]
        public int Id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string lyric { get; set; }
        public DateTime? datecreated { get; set; }
        public int? userid { get; set; }
        public int? seriid { get; set; }
        public string imdb { get; set; }
        public int? format { get; set; }
        public int? sub_type { get; set; }
        public int? publish_year { get; set; }
        public int? publish_countryid { get; set; }
        public string duration { get; set; }
        public string actor { get; set; }
        public string director { get; set; }
        public int? film_type { get; set; }
        public int? episode { get; set; }
        public int? episode_current { get; set; }
        public string contract_copyright { get; set; }
        public DateTime? contract_exprired { get; set; }
        public string contract_appendix { get; set; }
        public string copyright_appendix { get; set; }
        public DateTime? copyright_expired { get; set; }
        public int? catalog_id { get; set; }
        public string upload_file { get; set; }
        public string thumb_file { get; set; }
        public int? exclusive { get; set; }
        public float? price { get; set; }
        public int? status { get; set; }
        public int showhome { get; set; }
        public int? cpid { get; set; }
        public string publish_countryid_title { get; set; }
        public string catalog_id_title { get; set; }
        public string cpid_title { get; set; }
        public int episode_id { get; set; }
    }
    [Alias("film_like_dislike")]
    public partial class film_like_dislike : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public int? film_id { get; set; }
        public int? user_id { get; set; }
        public int? like { get; set; }
        public int? dislike { get; set; }
        public DateTime? datecreated { get; set; }
        public DateTime? dateupdated { get; set; }
    }
    [Alias("vw_film_like_dislike")]
    public partial class vw_film_like_dislike
    {
        [Required]
        public int Id { get; set; }
        public int? film_id { get; set; }
        public int? user_id { get; set; }
        public int? like { get; set; }
        public int? dislike { get; set; }
        public DateTime? datecreated { get; set; }
        public DateTime? dateupdated { get; set; }
    }
    [Alias("film_user_package")]
    public partial class film_user_package : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public int? userid { get; set; }
        public int? packageid { get; set; }
        public string phone { get; set; }
        public DateTime? datecreated { get; set; }
    }
    [Alias("vw_film_user_package")]
    public partial class vw_film_user_package
    {
        [Required]
        public int Id { get; set; }
        public int? userid { get; set; }
        public int? packageid { get; set; }
        public string phone { get; set; }
        public DateTime? datecreated { get; set; }
        public string package_title { get; set; }
    }
    [Alias("film_landing_page_pr")]
    public partial class film_landing_page_pr : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public int? guidid { get; set; }
        public string source { get; set; }
        public string ip { get; set; }
        public DateTime? datecreated { get; set; }
    }
    [Alias("vw_film_landing_page_pr")]
    public partial class vw_film_landing_page_pr
    {
        [Required]
        public int Id { get; set; }
        public int? guidid { get; set; }
        public string source { get; set; }
        public string ip { get; set; }
        public DateTime? datecreated { get; set; }
    }
    // extend
    [Alias("film_drm")]
    public partial class film_drm
    {
        [Required]
        public int Id { get; set; }
        public int userId { get; set; }
        public string key { get; set; }
        public string metaData { get; set; }
        public DateTime? datecreate { get; set; }
        public DateTime? datesubcribe { get; set; }
        public string note { get; set; }
    }
    [Alias("vw_film_drm")]
    public partial class vw_film_drm
    {
        [Required]
        public int Id { get; set; }
        public int userId { get; set; }
        public string key { get; set; }
        public string metaData { get; set; }
        public DateTime? datecreate { get; set; }
        public DateTime? datesubcribe { get; set; }
        public string note { get; set; }
    }
    [Alias("film_video_image")]
    public partial class film_video_image : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string url { get; set; }
        public int? filmid { get; set; }
        public int? userid { get; set; }
        public DateTime? datecreated { get; set; }
        public int? bb_thoa_thuan { get; set; }
    }
    [Alias("vw_film_video_image")]
    public partial class vw_film_video_image
    {
        public int Id { get; set; }
        public string url { get; set; }
        public int? filmid { get; set; }
        public int? userid { get; set; }
        public DateTime? datecreated { get; set; }
        public int? bb_thoa_thuan { get; set; }
    }
    [Alias("report")]
    public partial class report : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public int filmid { get; set; }
        public string title { get; set; }
        public string channel { get; set; }
        public string link { get; set; }
        public int status { get; set; }
        public int cost { get; set; }
        public string reason { get; set; }
        public string partner { get; set; }
        public string email { get; set; }
        public string note { get; set; }
        public int cpid { get; set; }
        public int userid { get; set; }
        public DateTime? datecreated { get; set; }
        public string source { get; set; }
        public DateTime? date_thaoco { get; set; }
        public DateTime? date_camco { get; set; }
    }
    [Alias("vw_report")]
    public partial class vw_report
    {
        public int Id { get; set; }
        public int filmid { get; set; }
        public string title { get; set; }
        public string channel { get; set; }
        public string link { get; set; }
        public int status { get; set; }
        public int cost { get; set; }
        public string reason { get; set; }
        public string partner { get; set; }
        public string email { get; set; }
        public string note { get; set; }
        public int cpid { get; set; }
        public int userid { get; set; }
        public string cpid_title { get; set; }
        public string userid_title { get; set; }
        public DateTime? datecreated { get; set; }
        public string source { get; set; }
        public string channel_title { get; set; }
        public string channel_link { get; set; }
        public DateTime? date_thaoco { get; set; }
        public DateTime? date_camco { get; set; }
    }

    [Alias("static_content")]
    public partial class static_content : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string key { get; set; }
    }
    [Alias("vw_static_content")]
    public partial class vw_static_content
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string key { get; set; }
    }

    [Alias("film_contract")]
    public partial class film_contract : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string title { get; set; }
        public string upload_file { get; set; }
        public string thumb_file { get; set; }
        public DateTime? date_created { get; set; }
        public DateTime? date_updated { get; set; }
        public int status { get; set; }
        public int userid { get; set; }
    }
    [Alias("vw_film_contract")]
    public partial class vw_film_contract
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string upload_file { get; set; }
        public string thumb_file { get; set; }
        public DateTime? date_created { get; set; }
        public DateTime? date_updated { get; set; }
        public int status { get; set; }
        public int userid { get; set; }
    }

    [Alias("bb_thoa_thuan")]
    public partial class bb_thoa_thuan : IHasId<int>
    {
        [Alias("id")]
        [AutoIncrement]
        public int Id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public int? user_id { get; set; }
        public DateTime? date_created { get; set; }
        public DateTime? date_updated { get; set; }
        public int status { get; set; }
        public string note { get; set; }
    }
    [Alias("vw_bb_thoa_thuan")]
    public partial class vw_bb_thoa_thuan
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public int? user_id { get; set; }
        public DateTime? date_created { get; set; }
        public DateTime? date_updated { get; set; }
        public int status { get; set; }
        public string note { get; set; }
        public string user_name { get; set; }
    }
    [Alias("film_register")]
    public partial class film_register
    {
        public int id { get; set; }
        public int videoid { get; set; }
        public string title { get; set; }
        public string link_video { get; set; }
        public string channel { get; set; }
        public string link_channel { get; set; }
        public DateTime date { get; set; }
        public float time { get; set; }
        public string email { get; set; }
        public string source { get; set; }
        public string cptitle { get; set; }
        public int userid { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public DateTime date_start { get; set; }
        public DateTime date_end { get; set; }
        public DateTime? date_camco{ get; set; }
        public string status { get; set; }
        public string note { get; set; }
        public string payment { get; set; }
        public string phone { get; set; }
        public float price { get; set; }
        public string name { get; set; }
        public string cmt { get; set; }
        public string sohd { get; set; }
        public string filehd { get; set; }
        public string state { get; set; }
        public string address { get; set; }

    }
    [Alias("film_register_detail")]
    public partial class film_register_detail
    {
        public int id { get; set; }
        public int regid { get; set; }
        public string videotitle { get; set; }
        public string link { get; set; }
        public string channel { get; set; }
        public string linkchannel { get; set; }
        public DateTime date { get; set; }
        public float time { get; set; }
        public string release { get; set; }
        public string platform { get; set; }
    }

}

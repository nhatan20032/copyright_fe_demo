using copyrights_fe.Helpers;
using copyrights_fe.Model;
using copyrights_fe.Services;
using copyrights_fe.Services.Connection;
using lamlt.webservice.Services;
using ServiceStack.OrmLite;
using Spire.Doc;
using System.Globalization;
using System.Reflection;

namespace Acs_Lala.Services
{
    public class film_resgiterService : AppConnection
    {
        #region ===================================== DELETE =====================================
        public int Delete(int id)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                return db.Delete(db.From<film_register>().Where(x => x.id == id));
            }
        }
        #endregion
        #region ===================================== GetAll =====================================
        public List<film_register> GetAll()
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                return db.Select(db.From<film_register>().OrderByDescending(x => x.id)).ToList();
            }
        }
        #endregion

        #region ===================================== GetInfo =====================================
        public List<film_register> GetInfo(int id, int userid, int videoid, string title, string email, string channel, string link_channel, string link_video, string status)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<film_register>();
                if (id > 0) { query.Where(e => e.id == id); }
                if (userid > 0) { query.Where(e => e.userid == userid); }
                if (videoid > 0) { query.Where(e => e.videoid == videoid); }
                if (!string.IsNullOrEmpty(title)) { query.Where(e => e.title == title); }
                if (!string.IsNullOrEmpty(email)) { query.Where(e => e.email == email); }
                if (!string.IsNullOrEmpty(channel)) { query.Where(e => e.channel == channel); }
                if (!string.IsNullOrEmpty(link_channel)) { query.Where(e => e.link_channel == link_channel); }
                if (!string.IsNullOrEmpty(link_video)) { query.Where(e => e.link_video == link_video); }
                if (!string.IsNullOrEmpty(status)) { query.Where(e => e.status == status); }
                query.OrderByDescending(x => x.id);
                return db.Select(query).ToList();
            }
        }

        internal int update_status(int id, string status)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<film_register>().Where(e => e.id == id);
                var objUpdate = db.Select(query).SingleOrDefault();
                if (objUpdate != null)
                {
                    objUpdate.status = status;
                    if (db.Update(objUpdate) > 0)
                    {
                        return objUpdate.id;
                    }
                }
            }
            return -1;
        }
        #endregion
        #region ===================================== Cập nhật link video, channel =====================================
        public int UpdateLink(film_register obj)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                if (obj.id > 0)
                {
                    var query = db.From<film_register>().Where(e => e.id == obj.id || (e.userid == obj.userid && e.videoid == obj.videoid));
                    var objUpdate = db.Select(query).SingleOrDefault();
                    if (objUpdate != null)
                    {
                        // Giữ nguyên nội dung cũ
                        objUpdate.id = objUpdate.id;
                        objUpdate.videoid = objUpdate.videoid;
                        objUpdate.title = objUpdate.title;
                        objUpdate.date = objUpdate.date;
                        objUpdate.time = objUpdate.time;
                        objUpdate.email = objUpdate.email;
                        objUpdate.source = objUpdate.source;
                        objUpdate.userid = objUpdate.userid;
                        objUpdate.updated = DateTime.Now;
                        objUpdate.status = objUpdate.status;
                        objUpdate.note = objUpdate.note;
                        objUpdate.payment = objUpdate.payment;
                        objUpdate.phone = objUpdate.phone;
                        objUpdate.date_start = objUpdate.date_start;
                        objUpdate.date_end = objUpdate.date_end;
                        objUpdate.date_camco = objUpdate.date_camco;
                        objUpdate.state = objUpdate.state;
                        // Người dùng cập nhật thông tin 
                        objUpdate.link_video = !string.IsNullOrEmpty(obj.link_video) ? obj.link_video : objUpdate.link_video;
                        objUpdate.channel = !string.IsNullOrEmpty(obj.channel) ? obj.channel : objUpdate.channel;
                        objUpdate.link_channel = !string.IsNullOrEmpty(obj.link_channel) ? obj.link_channel : objUpdate.link_channel;

                        int rs = db.Update(objUpdate) > 0 ? objUpdate.id : 0;
                        return rs;
                    }
                }
            }
            return 0;
        }
        #endregion
        public int getVAT()
        {
            int rs = 10;
            try
            {
                web_configService cf = new web_configService();
                string vat = cf.GetBykey("vat");
                if (!string.IsNullOrEmpty(vat))
                {
                    if (vat.Contains("%")) { vat = vat.Replace("%", ""); }
                    rs = int.Parse(vat);
                }
            }
            catch (Exception) { }
            return rs;
        }
        #region ===================================== UpdateOrInsert =====================================
        public int UpdateOrInsert(film_register obj)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                if (obj.id > 0)
                {
                    var query = db.From<film_register>().Where(e => e.id == obj.id || (e.userid == obj.userid && e.videoid == obj.videoid));
                    var objUpdate = db.Select(query).SingleOrDefault();
                    if (objUpdate != null)
                    {
                        objUpdate.id = obj.id;
                        objUpdate.videoid = obj.videoid;
                        objUpdate.cptitle = obj.cptitle;
                        objUpdate.title = obj.title;
                        objUpdate.link_video = obj.link_video;
                        objUpdate.channel = obj.channel;
                        objUpdate.link_channel = obj.link_channel;
                        objUpdate.date = obj.date;
                        objUpdate.time = obj.time;
                        objUpdate.email = obj.email;
                        objUpdate.address = obj.address;
                        objUpdate.source = obj.source;
                        objUpdate.userid = obj.userid;
                        objUpdate.updated = DateTime.Now;
                        objUpdate.date_start = objUpdate.date_start;
                        objUpdate.date_end = objUpdate.date_end;
                        objUpdate.date_camco = objUpdate.date_camco;
                        objUpdate.price = objUpdate.price;
                        objUpdate.status = obj.status;
                        objUpdate.note = obj.note;
                        objUpdate.payment = obj.payment;
                        objUpdate.phone = obj.phone;
                        objUpdate.name = obj.name;
                        objUpdate.cmt = obj.cmt;
                        int rs = db.Update(objUpdate) > 0 ? objUpdate.id : 0;
                        return rs;
                    }
                    return -1;
                }
                else
                {
                    var objUpdate = new film_register
                    {
                        title = obj.title,
                        videoid = obj.videoid,
                        link_video = obj.link_video,
                        channel = obj.channel,
                        link_channel = obj.link_channel,
                        date = obj.date,
                        time = obj.time,
                        email = obj.email,
                        address = obj.address,
                        source = obj.source,
                        userid = obj.userid,
                        cptitle = obj.cptitle,
                        created = DateTime.Now,
                        updated = DateTime.Now,
                        status = obj.status,
                        phone = obj.phone,
                        note = obj.note,
                        payment = obj.payment,
                        date_start = obj.date_start,
                        date_end = obj.date_end,
                        date_camco = obj.date_camco,
                        name = obj.name,
                        cmt = obj.cmt,
                        state = obj.state,
                    };
                    int rs = (int)db.Insert(objUpdate, selectIdentity: true);
                    film_video obj_video;
                    if (rs > 0)
                    {
                        // Gửi Email cho khách hàng đăng ký
                        string regHtml = "";
                        float total_price = 0;
                        string br = "<br />";
                        regHtml += "Chào <strong>" + obj.email + "</strong> ";

                        // Lấy thông tin video
                        try
                        {
                            obj_video = db.Select(db.From<film_video>().Where(e => e.Id == obj.videoid)).LastOrDefault();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        if (obj.price > 0)
                        {
                            // Đổi bằng giá đặc biệt(giá tương lai)
                            total_price = obj.price * obj.time;
                        }
                        else
                        {
                            // Tổng giá = giá của video * thời gian)
                            total_price = (int)(obj_video.price * obj.time);
                        }
                        if (!string.IsNullOrEmpty(objUpdate.note) && objUpdate.note.Contains("Xóa video"))
                        {
                            total_price = 0;
                        }
                        int config_vat = getVAT();
                        float tongtien = ((total_price * config_vat) / 100) + total_price;
                        float? phidasudung = 0;
                        float? phidasudung_vat = 0;
                        // Kiểm tra hình thức đăng ký
                        switch (obj.payment)
                        {
                            case "Đóng phí":
                                regHtml += "Chúng tôi đã nhận được thông tin bạn đăng ký trên hệ thống" + br + br;
                                regHtml += "Chi phí thanh toán của bạn là:" + br;
                                if (!string.IsNullOrEmpty(obj.note))
                                {
                                    DateTime oldDate;
                                    DateTime.TryParse(obj.date.ToShortDateString(), out oldDate);
                                    DateTime date_start = obj.date_start;
                                    if (objUpdate.note.Contains("Xóa video"))
                                    {
                                        date_start = DateTime.Now;
                                        if (obj.date_camco != null)
                                        {
                                            date_start = obj.date_camco.Value;
                                        }
                                    }
                                    TimeSpan difference = date_start.Subtract(oldDate);
                                    DateTime DateTimeDifferene = DateTime.MinValue + difference;
                                    int InYears = DateTimeDifferene.Year - 1;
                                    int InMonths = DateTimeDifferene.Month - 1;
                                    int InDays = DateTimeDifferene.Day - 1;
                                    if (InYears > 0)
                                    {
                                        phidasudung = (obj_video.price * InYears);
                                    }
                                    // Nếu số tháng lớn hơn 7 thì sẽ tính tròn lên 1 năm
                                    // Nếu số tháng lớn hơn hoặc bằng 7 và số ngày lớn hơn hoặc bằng 1
                                    if ((InMonths > 7) || (InMonths == 7 && InDays >= 1))
                                    {
                                        phidasudung += (obj_video.price * 1);
                                        goto lblVat;
                                    }
                                    if (
                                        (InMonths >= 1 && InMonths <= 7) || // Nếu số tháng lớn hơn 1 và nhỏ hơn 7 tháng thì vẫn tính tròn xuống 6 tháng
                                        (InMonths == 7 && InDays == 0) ||   // Nếu số tháng bằng 7 và số ngày bằng 0
                                        (InYears <= 0 && InDays > 0)        // Nếu dưới 6 tháng nhưng số ngày là 1 cũng tính 6 tháng
                                    )
                                    {
                                        phidasudung += (obj_video.price * 0.5f);
                                        goto lblVat;
                                    }

                                lblVat:
                                    phidasudung_vat = ((phidasudung * config_vat) / 100);
                                    phidasudung += phidasudung_vat;
                                    regHtml += $" -   Phí đã sử dụng + VAT: {string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", phidasudung)} VND" + br;
                                }
                                regHtml += $" - Chi phí đăng ký tiếp tục sử dụng: {string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", total_price)} VND" + br;
                                regHtml += $"   + VAT phí tiếp tục sử dụng: {string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", ((total_price * config_vat) / 100))} VND" + br;
                                regHtml += $" - Tổng tiền: {string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tongtien + phidasudung)} VND" + br;
                                regHtml += $" - Tên bài hát: {obj.title}" + br;
                                regHtml += $" - Tên tác giả: {obj.cptitle}" + br;
                                regHtml += $" - Kênh: <a target='_blank' href='{obj.link_channel}'>{obj.channel}</a>" + br;
                                regHtml += $" - Link: <a target='_blank' href='{obj.link_video}'>{obj.link_video}</a>" + br;
                                regHtml += "Nếu bạn đồng ý với mức phí trên, xin vui lòng xác nhận vào mail này." + br;
                                regHtml += "Chi tiết thủ tục chúng tôi sẽ gửi lại sau khi nhận được phản hồi từ bạn." + br;
                                //regHtml += $" - Gửi ảnh chụp màn hình giao dịch thanh toán thành công vào email này." + br;
                                //regHtml += "<br /><b>* Thông tin chuyển khoản</b>" + br;
                                //regHtml += "Chủ tài khoản: CÔNG TY CỔ PHẦN TRUYỀN THÔNG VÀ GIẢI TRÍ HỒNG ÂN" + br;
                                //regHtml += "Số tài khoản: 140 2334 3933 013" + br;
                                //regHtml += "Ngân hàng: TechComBank - Chi nhánh Hà Nội" + br + br;
                                //regHtml += "<b>2. </b>Điền thông tin vào hợp đồng đăng ký sử dụng bản quyền tác giả đính kèm dưới đây, in 03 bản, ký và gửi lại cho chúng tôi tại địa chỉ: <b>Bộ phận hành chính Hồng Ân - SĐT 024 6684 2838  - Tòa nhà Golden West, số 2 Lê Văn Thiêm, Nhân Chính, Thanh Xuân, Hà Nội</b>. Đây là cơ sở để chúng tôi làm việc với bên tác giả. Chúng tôi sẽ gửi lại bạn 01 bản," + br + br;
                                //regHtml += "<b>3. </b>Gửi bản scan 02 mặt của CMND/CCCD qua email này." + br + br;
                                //regHtml += $"<i><b>*</b> Trường hợp đơn vị sử dụng là cá nhân thì gửi kèm CMND/CCCD theo email này." + br;
                                //regHtml += $"<b>*</b> Trường hợp đơn vị sử dụng là công ty / tổ chức thì không cần gửi.</i>" + br + br;
                                //regHtml += "Sau khi thanh toán xong, bạn vui lòng vào lại hệ thống xác nhận việc thanh toán thành công và thực hiện gửi biên bản theo địa chỉ trên cho chúng tôi." + br;
                                break;
                            case "Chia sẻ doanh thu":
                                regHtml += "Chúng tôi đã nhận được thông tin đăng ký của bạn trên hệ thống." + br;
                                regHtml += "Với hình thức đăng ký chia sẻ doanh thu, chúng tôi thông tin đến bạn như sau." + br;
                                regHtml += "<b>1. </b>Tỷ lệ doanh thu là: " + br + br;
                                regHtml += "<b>2. </b>Nội dung mô tả trên video phải có thông tin xác nhận bản quyền của Hồng Ân, cụ thể:" + br;
                                regHtml += "© Copyright by Hong An Entertaiment" + br;
                                regHtml += "Bản quyền các ca khúc thuộc về Hồng Ân Entertaiment" + br + br;
                                regHtml += "<b>3. </b>Điền thông tin vào hợp đồng đăng ký sử dụng bản quyền tác giả đính kèm dưới đây, in 03 bản, ký và gửi lại cho chúng tôi tại địa chỉ: <b>Bộ phận hành chính Hồng Ân - SĐT 024 6684 2838 - Tòa nhà Golden West, số 2 Lê Văn Thiêm, Nhân Chính, Thanh Xuân, Hà Nội</b>. Đây là cơ sở để chúng tôi làm việc với bên tác giả. Chúng tôi sẽ gửi lại bạn 01 bản," + br + br;
                                regHtml += "<b>4. </b>Gửi bản scan 02 mặt của CMND/CCCD qua email này." + br;
                                regHtml += $"<i><b>*</b> Trường hợp đơn vị sử dụng là cá nhân thì gửi kèm CMND/CCCD theo email này." + br;
                                regHtml += $"<b>*</b> Trường hợp đơn vị sử dụng là công ty / tổ chức thì không cần gửi.</i>" + br + br;
                                break;
                            default:
                                regHtml += "Chúng tôi đã nhận được thông tin đăng ký của bạn trên hệ thống. Chúng tôi sẽ liên hệ với bạn trong thời gian sớm nhất" + br;
                                break;
                        }
                        regHtml += "Trân trọng cảm ơn!" + br;
                        #region ===================================== Thay thế nội dung trong file tác quyền =====================================
                        string newfile_hopdong = "";
                        string home_dir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                        string path = $"{home_dir}/wwwroot/file-mau/";
                        string path_temp = $"{path}/temp/";
                        string dem_sohd = $"{path}/so_hop_hong_hien_tai.txt";
                        string fileTacQuyen = $"{path}/mau-hop-dong-tac-quyen.docx";
                        string file_bbxoa = $"{path}/bb_xoa_v1.docx";

                        // Lấy số hợp đồng của ID cũ nhất
                        var queryOld = db.From<film_register>();
                        List<film_register> lstReg = db.Select(queryOld);
                        int sohd_cu = 0;
                        int thang_truoc = 0;
                        if (lstReg.Count > 0)
                        {
                            if (lstReg.Count >= 2)
                            {
                                if (!string.IsNullOrEmpty(lstReg[lstReg.Count - 2].sohd))
                                {
                                    sohd_cu = int.Parse(lstReg[lstReg.Count - 2].sohd.Split("-")[0]);
                                    thang_truoc = int.Parse(lstReg[lstReg.Count - 2].sohd.Split("-")[1]);
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(lstReg[lstReg.Count - 1].sohd))
                                {
                                    sohd_cu = int.Parse(lstReg[lstReg.Count - 1].sohd.Split("-")[0]);
                                    thang_truoc = int.Parse(lstReg[lstReg.Count - 1].sohd.Split("-")[1]);
                                }
                            }
                            if (sohd_cu < 0)
                            {
                                sohd_cu = 0;
                            }
                        }
                        DateTime today = DateTime.Now;
                        try
                        {
                            string sohopdong = "";
                            int sohd_moi = 0;
                            int sohd_cu_download = 0;
                            // Kiểm tra xem tạo lúc này tháng là tháng mấy
                            if (thang_truoc == today.Month)
                            {
                                sohd_moi = sohd_cu + 1;
                            }
                            else
                            {
                                sohd_moi = 1;
                            }
                            if (!string.IsNullOrEmpty(obj.filehd))
                            {
                                sohd_moi -= 1;
                            }
                            if (!string.IsNullOrEmpty(objUpdate.note) && objUpdate.note.Contains("Xóa video"))
                            {
                                fileTacQuyen = file_bbxoa;
                                Console.WriteLine($"==============> fileTacQuyen : {fileTacQuyen}");
                            }
                            if (File.Exists(fileTacQuyen))
                            {
                                fileTacQuyen = fileTacQuyen.Replace("//", "//");
                                Console.WriteLine("Tìm thấy file mẫu hợp đồng và chuẩn bị thay đổi thông tin");

                                Document doc = new Document(fileTacQuyen);
                                doc.EmbedFontsInFile = true;
                                string thanghientai = "";

                                thanghientai = today.Month < 10 ? "0" + today.Month.ToString() : today.Month.ToString();
                                sohd_cu_download = sohd_moi - 1;
                                //sohopdong = (sohd_cu < 10) ? "0" + sohd_cu_download + thanghientai : sohd_cu_download + thanghientai;

                                sohopdong = (sohd_moi < 10) ? "0" + sohd_moi + thanghientai : sohd_moi + thanghientai;
                                //if (sohopdong.Contains("00")) { sohopdong = sohopdong.Replace("00", "0"); }
                                doc.Replace("[sohopdong]", sohopdong, false, true);
                                doc.Replace("[ngay]", today.Day.ToString(), false, true);
                                doc.Replace("[thang]", today.Month.ToString(), false, true);
                                doc.Replace("[nam]", today.Year.ToString(), false, true);
                                doc.Replace("[baihat]", obj.title, false, true);
                                doc.Replace("[kenh]", !string.IsNullOrEmpty(obj.channel) ? obj.channel : "", false, true);
                                doc.Replace("[link]", !string.IsNullOrEmpty(obj.link_video) ? obj.link_video : "", false, true);
                                doc.Replace("[phichuavat]", string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", total_price), false, true);
                                var tonggggggg = tongtien + phidasudung;
                                var tonggggggg_str = "";
                                if (tonggggggg > 0)
                                {
                                    tonggggggg_str = HelpUtil.NumberToText((float)tonggggggg);
                                }
                                doc.Replace("[tongtien]", string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tonggggggg), false, true);
                                doc.Replace("[bangchu]", tonggggggg_str, false, true);
                                doc.Replace("[nhacsi]", obj.cptitle, false, true);
                                doc.Replace("[nentang]", obj.source.ToUpper(), false, true);
                                Console.WriteLine(obj.source.ToUpper());
                                var thoigiandk = obj.time;
                                var ngayph = obj.date;// 20/5/2020
                                var ngayhethan = ngayph.AddMonths((int)thoigiandk * 12);
                                var demngay = (ngayhethan - ngayph).Days;

                                //doc.Replace("[ngayhethan]", ngayph.AddDays(demngay).ToString("dd-MM-yyyy"), false, true);
                                // Ngày tháng bắt đầu và kết thúc sử dụng sẽ do đội bản quyền set
                                //doc.Replace("[ngayphathanh]", "Từ ........../........../..........     ", false, true);
                                //doc.Replace("[ngayhethan]", "     đến    ........../........../..........", false, true);

                                if (!string.IsNullOrEmpty(obj.note))
                                {
                                    if (objUpdate.note.Contains("Xóa video"))
                                    {
                                        if (obj.date_camco != null)
                                        {
                                            today = obj.date_camco.Value;
                                        }
                                        doc.Replace("[ngayquakhu]", $"Từ {obj.date.ToString("dd-MM-yyyy")}", false, true);
                                        doc.Replace("[ngayhethan]", $"     đến    {today.ToString("dd-MM-yyyy")}", false, true);
                                        doc.Replace("[vat]", $"{string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", ((phidasudung - phidasudung_vat) * config_vat) / 100)}", false, true);
                                        doc.Replace("[phidasudung]", $"{string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", phidasudung - phidasudung_vat)}", false, true);
                                        doc.Replace("[link_channel]", !string.IsNullOrEmpty(obj.link_channel) ? obj.link_channel : "", false, true);
                                    }
                                    else
                                    {
                                        doc.Replace("[ngayquakhu]", $"Thời gian đã sử dụng: {obj.date.ToString("dd-MM-yyyy")} đến {today.ToString("dd-MM-yyyy")}", false, true);
                                        doc.Replace("[phidasudung]", $"Phí đã sử dụng + VAT: {string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", phidasudung)}" + " VND", false, true);
                                    }
                                }
                                else
                                {
                                    doc.Replace("[vat]", string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", ((total_price * config_vat) / 100)), false, true);
                                    doc.Replace("[ngayquakhu]", "", false, true);
                                    doc.Replace("[phidasudung]", "", false, true);
                                }

                                doc.Replace("[vat]", string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", ((total_price * config_vat) / 100)), false, true);
                                doc.Replace("[ngayphathanh]", $"Từ {obj.date_start.ToString("dd-MM-yyyy")}", false, true);
                                doc.Replace("[ngayhethan]", $"     đến    {obj.date_end.ToString("dd-MM-yyyy")}", false, true);
                                //Lưu lại file hợp đồng mới
                                if (!string.IsNullOrEmpty(objUpdate.note) && objUpdate.note.Contains("Xóa video"))
                                {
                                    string docx_name = $"bien_ban_thoa_thuan_chi_phi_BQTG_{sohopdong}_{DateTime.Now.Year}.docx";
                                    newfile_hopdong = $"{path_temp}/{docx_name}";
                                    doc.SaveToFile(newfile_hopdong, FileFormat.Docx);
                                    // Lưu cho admin
                                    doc.SaveToFile($"/home/amnhacsaigon/dotnet/acs-be-web/wwwroot/file-mau/temp/{docx_name}", FileFormat.Docx);
                                    update_sohd(rs, sohopdong.Substring(0, 2) + "-" + sohopdong.Substring(2), docx_name);
                                    Console.WriteLine($"==============> fileTacQuyen : {fileTacQuyen}");
                                    Console.WriteLine($"Đã thay đổi thông tin thành công ra file mới {newfile_hopdong}");
                                }
                                else
                                {
                                    newfile_hopdong = $"{path_temp}/hop_dong_tac_quyen_{sohopdong}_{DateTime.Now.Year}.docx";
                                    doc.SaveToFile(newfile_hopdong, FileFormat.Docx);
                                    // Lưu cho admin
                                    doc.SaveToFile($"/home/amnhacsaigon/dotnet/acs-be-web/wwwroot/file-mau/temp/hop_dong_tac_quyen_{sohopdong}_{DateTime.Now.Year}.docx", FileFormat.Docx);
                                    update_sohd(rs, sohopdong.Substring(0, 2) + "-" + sohopdong.Substring(2), $"hop_dong_tac_quyen_{sohopdong}_{DateTime.Now.Year}.docx");
                                    Console.WriteLine($"Đã thay đổi thông tin thành công ra file mới {newfile_hopdong}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Không tìm thấy file mẫu hợp đồng");
                                Console.WriteLine(fileTacQuyen);
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        #endregion
                        try
                        {
                            HelpUtils.SendEmail(obj.email, "Xác nhận thông tin thanh toán chi phí bản quyền", regHtml, null);
                        }
                        catch (Exception)
                        {
                            Delete(rs);
                            return -1;
                        }
                        Console.WriteLine(rs);
                        return rs;
                    }
                    return rs;
                }
            }
        }
        internal string update_sohd(int id, string sohd, string filehd)
        {
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var query = db.From<film_register>().Where(e => e.id == id);
                var rs = db.Select(query).LastOrDefault();
                if (rs != null)
                {
                    rs.sohd = sohd;
                    if (!string.IsNullOrEmpty(filehd))
                    {
                        var lstFile = filehd.Split("/");
                        rs.filehd = lstFile[lstFile.Length - 1];
                    }
                    return "Đổi trạng thái " + (db.Update(rs) > 0 ? "thành công" : "thất bại");
                }
            }
            return "";
        }
        #endregion



        #region ===================================== Tạo giao dịch =====================================
        public int CreateTransaction(film_register objReg)
        {
            int result;
            string time_reg = "0";
            switch (objReg.time)
            {
                case 6: time_reg = "0.5"; break;
                case 12: time_reg = "1"; break;
                case 18: time_reg = "1.5"; break;
                case 24: time_reg = "2"; break;
                case 36: time_reg = "3"; break;
            }
            lamlt.data.film_transactions obj = new lamlt.data.film_transactions()
            {
                film_id = objReg.videoid,
                fee1 = 0,// Tổng giá = film_video.price + month
                report_id = 0,
                payer_email = "", // Lấy từ Register
                artist_id = 0, // Lấy theo bài bài hát 
                payment = objReg.status,
                months2 = time_reg,
                vat = 10,
            };
            using (var db = _connectionFilmLala.OpenDbConnection())
            {
                var objUpdate = new lamlt.data.film_transactions
                {
                    artist_id = obj.artist_id,
                    film_id = obj.film_id,
                    report_id = obj.report_id,
                    source_id = obj.source_id,
                    fee1 = obj.fee1,
                    months1 = obj.months1,
                    fee2 = obj.fee2,
                    months2 = obj.months2,
                    datecreated = DateTime.Now,
                    dateupdated = DateTime.Now,
                    payment = obj.payment,
                    user_id = objReg.userid,
                    payer = obj.payer,
                    status = obj.status,
                    date_chuyentien = obj.date_chuyentien,
                    date_nhanbienban = obj.date_nhanbienban,
                    date_thaoco = obj.date_thaoco,
                    note = obj.note,
                    payer_email = obj.payer_email,
                    payer_phone = obj.payer_phone,
                    payer_address = obj.payer_address,
                    date_gui_bien_ban = obj.date_gui_bien_ban,
                    vat = obj.vat,
                    giam_tru_chi_phi = obj.giam_tru_chi_phi,
                    date_ky_bien_ban = obj.date_ky_bien_ban
                };
                result = (int)db.Insert(objUpdate, selectIdentity: true);
                return result > 0 ? result : -1;
            }
        }
        #endregion
        public static string CalculateDays(DateTime StartDate, DateTime EndDate)
        {
            DateTime oldDate;
            DateTime.TryParse(StartDate.ToShortDateString(), out oldDate);
            TimeSpan difference = EndDate.Subtract(oldDate);
            DateTime DateTimeDifferene = DateTime.MinValue + difference;
            int InYears = DateTimeDifferene.Year - 1;
            int InMonths = DateTimeDifferene.Month - 1;
            int InDays = DateTimeDifferene.Day - 1;
            return InYears.ToString() + " Years " + InMonths.ToString() + " Months " + InDays.ToString() + " Days";
        }
    }
}

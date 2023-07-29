using System;
using System.Text;

namespace copyrights_fe.Services.Utilities
{
    public class CommService
    {
        //IConnectionFactory _connectionData;


        public CommService()
        {

        }

        public static string fitTo6(int p)
        {
            string result = p.ToString();
            if (result.Length == 6) return result;
            if (result.Length == 5) return "0" + result;
            if (result.Length == 4) return "00" + result;
            if (result.Length == 3) return "000" + result;
            if (result.Length == 2) return "0000" + result;
            if (result.Length == 1) return "00000" + result;
            return result;
        }
        public static string fitTo6(long p)
        {
            string result = p.ToString();
            if (result.Length == 6) return result;
            if (result.Length == 5) return "0" + result;
            if (result.Length == 4) return "00" + result;
            if (result.Length == 3) return "000" + result;
            if (result.Length == 2) return "0000" + result;
            if (result.Length == 1) return "00000" + result;
            return result;
        }
        public static string fitTo3(int p)
        {
            string result = p.ToString();
            if (result.Length == 2) return "0" + result;
            if (result.Length == 1) return "00" + result;
            return result;
        }


        public static DateTime ConvertStringToDate(string p)
        {
            try
            {
                if (string.IsNullOrEmpty(p))
                    return new DateTime(1900, 01, 01);
                if (p.IndexOf(":") != -1) return DateTime.ParseExact(p, "dd/MM/yyyy HH:mm", null);
                return DateTime.ParseExact(p, "dd/MM/yyyy", null);
            }
            catch (Exception) { return new DateTime(1900, 01, 01); }
        }
        public static DateTime ConvertStringToDate(string p, string f)
        {
            try
            {
                if (string.IsNullOrEmpty(p))
                    return new DateTime(1900, 01, 01);
                return DateTime.ParseExact(p, f, null);
            }
            catch (Exception) { return new DateTime(1900, 01, 01); }
        }
        public static DateTime? ConvertStringToDateNull(string p)
        {
            try
            {
                if (string.IsNullOrEmpty(p))
                    return new DateTime(1900, 01, 01);

                var d = new DateTime(1900, 01, 01);
                if (p.IndexOf(":") != -1)
                    d = DateTime.ParseExact(p, "dd/MM/yyyy HH:mm", null);
                d = DateTime.ParseExact(p, "dd/MM/yyyy", null);
                if (d.Year <= 1900)
                    return null;
                else
                    return d;
            }
            catch (Exception) { return new DateTime(1900, 01, 01); }
        }

        public static string DecimalToString(double p)
        {
            return So_chu(p);
        }
        public static string DecimalToString(string p)
        {
            return So_chu(double.Parse(p));
        }
        public static string So_chu(double gNum)
        {

            if (gNum == 0)
                return "Không đồng";
            if (gNum < 0) gNum = -1 * gNum;

            string lso_chu = "";
            string tach_mod = "";
            string tach_conlai = "";
            double Num = Math.Round(gNum, 0);
            string gN = Convert.ToString(Num);
            int m = Convert.ToInt32(gN.Length / 3);
            int mod = gN.Length - m * 3;
            string dau = "[+]";

            // Dau [+ , - ]
            if (gNum < 0)
                dau = "[-]";
            dau = "";

            // Tach hang lon nhat
            if (mod.Equals(1))
                tach_mod = "00" + Convert.ToString(Num.ToString().Trim().Substring(0, 1)).Trim();
            if (mod.Equals(2))
                tach_mod = "0" + Convert.ToString(Num.ToString().Trim().Substring(0, 2)).Trim();
            if (mod.Equals(0))
                tach_mod = "000";
            // Tach hang con lai sau mod :
            if (Num.ToString().Length > 2)
                tach_conlai = Convert.ToString(Num.ToString().Trim().Substring(mod, Num.ToString().Length - mod)).Trim();

            ///don vi hang mod
            int im = m + 1;
            if (mod > 0)
                lso_chu = Tach(tach_mod).ToString().Trim() + " " + Donvi(im.ToString().Trim());
            /// Tach 3 trong tach_conlai

            int i = m;
            int _m = m;
            int j = 1;
            string tach3 = "";
            string tach3_ = "";

            while (i > 0)
            {
                tach3 = tach_conlai.Trim().Substring(0, 3).Trim();
                tach3_ = tach3;
                lso_chu = lso_chu.Trim() + " " + Tach(tach3.Trim()).Trim();
                m = _m + 1 - j;
                if (!tach3_.Equals("000"))
                    lso_chu = lso_chu.Trim() + " " + Donvi(m.ToString().Trim()).Trim();
                tach_conlai = tach_conlai.Trim().Substring(3, tach_conlai.Trim().Length - 3);

                i = i - 1;
                j = j + 1;
            }
            if (lso_chu.Trim().Substring(0, 1).Equals("k"))
                lso_chu = lso_chu.Trim().Substring(10, lso_chu.Trim().Length - 10).Trim();
            if (lso_chu.Trim().Substring(0, 1).Equals("l"))
                lso_chu = lso_chu.Trim().Substring(2, lso_chu.Trim().Length - 2).Trim();
            if (lso_chu.Trim().Length > 0)
                lso_chu = dau.Trim() + " " + lso_chu.Trim().Substring(0, 1).Trim().ToUpper() + lso_chu.Trim().Substring(1, lso_chu.Trim().Length - 1).Trim() + " đồng chẵn.";

            return lso_chu.ToString().Trim();

        }
        private static string Tach(string tach3)
        {
            string Ktach = "";
            if (tach3.Equals("000"))
                return "";
            if (tach3.Length == 3)
            {
                string tr = tach3.Trim().Substring(0, 1).ToString().Trim();
                string ch = tach3.Trim().Substring(1, 1).ToString().Trim();
                string dv = tach3.Trim().Substring(2, 1).ToString().Trim();
                if (tr.Equals("0") && ch.Equals("0"))
                    Ktach = " không trăm lẻ " + Chu(dv.ToString().Trim()) + " ";
                if (!tr.Equals("0") && ch.Equals("0") && dv.Equals("0"))
                    Ktach = Chu(tr.ToString().Trim()).Trim() + " trăm ";
                if (!tr.Equals("0") && ch.Equals("0") && !dv.Equals("0"))
                    Ktach = Chu(tr.ToString().Trim()).Trim() + " trăm lẻ " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && dv.Equals("0"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && dv.Equals("5"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                if (tr.Equals("0") && ch.Equals("1") && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = " không trăm mười " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && ch.Equals("1") && dv.Equals("0"))
                    Ktach = " không trăm mười ";
                if (tr.Equals("0") && ch.Equals("1") && dv.Equals("5"))
                    Ktach = " không trăm mười lăm ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim() + " ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười " + Chu(dv.Trim()).Trim() + " ";

                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười lăm ";

            }


            return Ktach;

        }
        private static string Donvi(string so)
        {
            string Kdonvi = "";

            if (so.Equals("1"))
                Kdonvi = "";
            if (so.Equals("2"))
                Kdonvi = "nghìn";
            if (so.Equals("3"))
                Kdonvi = "triệu";
            if (so.Equals("4"))
                Kdonvi = "tỷ";
            if (so.Equals("5"))
                Kdonvi = "nghìn tỷ";
            if (so.Equals("6"))
                Kdonvi = "triệu tỷ";
            if (so.Equals("7"))
                Kdonvi = "tỷ tỷ";

            return Kdonvi;
        }
        private static string Chu(string gNumber)
        {
            string result = "";
            switch (gNumber)
            {
                case "0":
                    result = "không";
                    break;
                case "1":
                    result = "một";
                    break;
                case "2":
                    result = "hai";
                    break;
                case "3":
                    result = "ba";
                    break;
                case "4":
                    result = "bốn";
                    break;
                case "5":
                    result = "năm";
                    break;
                case "6":
                    result = "sáu";
                    break;
                case "7":
                    result = "bảy";
                    break;
                case "8":
                    result = "tám";
                    break;
                case "9":
                    result = "chín";
                    break;
            }
            return result;
        }

        public static string ExtractName(string p)
        {
            string[] arrSplit = p.Split(' ');
            return arrSplit[arrSplit.Length - 1];
        }

        public static string ExtractFamilyName(string p)
        {
            string[] arrSplit = p.Split(' ');
            if (arrSplit.Length <= 2) return arrSplit[0];
            string s = "";
            for (int jx = 0; jx <= arrSplit.Length - 2; jx++)
            {
                if (string.IsNullOrEmpty(s)) s = arrSplit[jx];
                else s = s + " " + arrSplit[jx];
            }
            return s;
        }



        internal static string displayDate(DateTime dateTime)
        {
            if (dateTime.Year <= 1900) return "";
            return dateTime.ToString("dd/MM/yyyy");
        }

        internal static string displayDate(DateTime? dateTime)
        {
            if (dateTime == null) return "";
            if (dateTime.GetValueOrDefault().Year <= 1900) return "";
            return dateTime.GetValueOrDefault().ToString("dd/MM/yyyy");
        }

        internal static string displayDateTime(DateTime? dateTime)
        {
            if (dateTime == null) return "";
            if (dateTime.GetValueOrDefault().Year <= 1900) return "";
            return dateTime.GetValueOrDefault().ToString("dd/MM/yyyy HH:mm");
        }

        public string GetProductCodeGeneral(string productname, string unitcode, string suppliercode)
        {
            return TrimVietnameseMark(productname, false, true) + "_" + TrimVietnameseMark(unitcode, false, true);// + "_" + TrimVietnameseMark(suppliercode, false, true);// +"_" + TrimVietnameseMark(suppliercode);
        }


        public string GetHotelCodeGeneral(string hotelname)
        {
            return TrimVietnameseMark(hotelname, false, true);
        }

        public static string TrimVietnameseMark(string str, bool isLower = true, bool isRemoveSpace = true)
        {
            str = str.Replace(".", " ");
            str = str.Replace(",", " ");
            str = str.Replace("_", "_");
            str = str.Replace("-", "-");
            str = str.Replace(";", " ");
            str = str.Replace("?", " ");
            str = str.Replace("(", "");
            str = str.Replace(")", "");
            str = str.Replace("[", "");
            str = str.Replace("]", "");
            str = str.Replace("{", "");
            str = str.Replace("}", "");
            str = str.Replace("<", "");
            str = str.Replace(">", "");
            str = str.Replace("'", "");

            while (str.IndexOf("  ") > 0)
            {
                str = str.Replace("  ", " ");
            }
            if (isRemoveSpace) str = str.Replace(" ", "");
            str = str.Replace("ấ", "a");
            str = str.Replace("ầ", "a");
            str = str.Replace("ẩ", "a");
            str = str.Replace("ẫ", "a");
            str = str.Replace("ậ", "a");

            str = str.Replace("Ấ", "A");
            str = str.Replace("Ầ", "A");
            str = str.Replace("Ẩ", "A");
            str = str.Replace("Ẫ", "A");
            str = str.Replace("Ậ", "A");

            str = str.Replace("ắ", "a");
            str = str.Replace("ằ", "a");
            str = str.Replace("ẳ", "a");
            str = str.Replace("ẵ", "a");
            str = str.Replace("ặ", "a");

            str = str.Replace("Ắ", "A");
            str = str.Replace("Ằ", "A");
            str = str.Replace("Ẳ", "A");
            str = str.Replace("Ẵ", "A");
            str = str.Replace("Ặ", "A");

            str = str.Replace("á", "a");
            str = str.Replace("à", "a");
            str = str.Replace("ả", "a");
            str = str.Replace("ã", "a");
            str = str.Replace("ạ", "a");
            str = str.Replace("â", "a");
            str = str.Replace("ă", "a");

            str = str.Replace("Á", "A");
            str = str.Replace("À", "A");
            str = str.Replace("Ả", "A");
            str = str.Replace("Ã", "A");
            str = str.Replace("Ạ", "A");
            str = str.Replace("Â", "A");
            str = str.Replace("Ă", "A");

            str = str.Replace("ế", "e");
            str = str.Replace("ề", "e");
            str = str.Replace("ể", "e");
            str = str.Replace("ễ", "e");
            str = str.Replace("ệ", "e");

            str = str.Replace("Ế", "E");
            str = str.Replace("Ề", "E");
            str = str.Replace("Ể", "E");
            str = str.Replace("Ễ", "E");
            str = str.Replace("Ệ", "E");

            str = str.Replace("é", "e");
            str = str.Replace("è", "e");
            str = str.Replace("ẻ", "e");
            str = str.Replace("ẽ", "e");
            str = str.Replace("ẹ", "e");
            str = str.Replace("ê", "e");

            str = str.Replace("É", "E");
            str = str.Replace("È", "E");
            str = str.Replace("Ẻ", "E");
            str = str.Replace("Ẽ", "E");
            str = str.Replace("Ẹ", "E");
            str = str.Replace("Ê", "E");

            str = str.Replace("í", "i");
            str = str.Replace("ì", "i");
            str = str.Replace("ỉ", "i");
            str = str.Replace("ĩ", "i");
            str = str.Replace("ị", "i");

            str = str.Replace("Í", "I");
            str = str.Replace("Ì", "I");
            str = str.Replace("Ỉ", "I");
            str = str.Replace("Ĩ", "I");
            str = str.Replace("Ị", "I");

            str = str.Replace("ố", "o");
            str = str.Replace("ồ", "o");
            str = str.Replace("ổ", "o");
            str = str.Replace("ỗ", "o");
            str = str.Replace("ộ", "o");

            str = str.Replace("Ố", "O");
            str = str.Replace("Ồ", "O");
            str = str.Replace("Ổ", "O");
            str = str.Replace("Ô", "O");
            str = str.Replace("Ộ", "O");

            str = str.Replace("ớ", "o");
            str = str.Replace("ờ", "o");
            str = str.Replace("ở", "o");
            str = str.Replace("ỡ", "o");
            str = str.Replace("ợ", "o");

            str = str.Replace("Ớ", "O");
            str = str.Replace("Ờ", "O");
            str = str.Replace("Ở", "O");
            str = str.Replace("Ỡ", "O");
            str = str.Replace("Ợ", "O");

            str = str.Replace("ứ", "u");
            str = str.Replace("ừ", "u");
            str = str.Replace("ử", "u");
            str = str.Replace("ữ", "u");
            str = str.Replace("ự", "u");

            str = str.Replace("Ứ", "U");
            str = str.Replace("Ừ", "U");
            str = str.Replace("Ử", "U");
            str = str.Replace("Ữ", "U");
            str = str.Replace("Ự", "U");

            str = str.Replace("ý", "y");
            str = str.Replace("ỳ", "y");
            str = str.Replace("ỷ", "y");
            str = str.Replace("ỹ", "y");
            str = str.Replace("ỵ", "y");

            str = str.Replace("Ý", "Y");
            str = str.Replace("Ỳ", "Y");
            str = str.Replace("Ỷ", "Y");
            str = str.Replace("Ỹ", "Y");
            str = str.Replace("Ỵ", "Y");

            str = str.Replace("Đ", "D");
            str = str.Replace("Đ", "D");
            str = str.Replace("đ", "d");

            str = str.Replace("ó", "o");
            str = str.Replace("ò", "o");
            str = str.Replace("ỏ", "o");
            str = str.Replace("õ", "o");
            str = str.Replace("ọ", "o");
            str = str.Replace("ô", "o");
            str = str.Replace("ơ", "o");

            str = str.Replace("Ó", "O");
            str = str.Replace("Ò", "O");
            str = str.Replace("Ỏ", "O");
            str = str.Replace("Õ", "O");
            str = str.Replace("Ọ", "O");
            str = str.Replace("Ô", "O");
            str = str.Replace("Ơ", "O");

            str = str.Replace("ú", "u");
            str = str.Replace("ù", "u");
            str = str.Replace("ủ", "u");
            str = str.Replace("ũ", "u");
            str = str.Replace("ụ", "u");
            str = str.Replace("ư", "u");

            str = str.Replace("Ú", "U");
            str = str.Replace("Ù", "U");
            str = str.Replace("Ủ", "U");
            str = str.Replace("Ũ", "U");
            str = str.Replace("Ụ", "U");
            str = str.Replace("Ư", "U");

            if (isLower) str = str.ToLower();

            return str;
        }

        public static string shortMark(string str)
        {
            var arr = str.Split(' ');
            var rs = "";
            if (arr.Length <= 1)
            {
                rs = arr[0].Trim().Substring(0, 1);
            }
            else
            {
                foreach (var item in arr)
                {
                    var it = string.IsNullOrEmpty(item) ? item.Trim() : "";
                    rs += item.Trim().Substring(0, 1);
                }
            }
            return rs.ToUpper();
        }


        public static string displayNumber(int? i)
        {
            if (i == null) return "";
            return i.GetValueOrDefault().ToString("N0").Replace(',', '.');
        }

        public static string displayNumber(decimal? i)
        {
            if (i == null) return "";
            return i.GetValueOrDefault().ToString("N0").Replace(',', '.');
        }

        public static string displayNumber(double? i)
        {
            if (i == null) return "";
            return i.GetValueOrDefault().ToString("N0").Replace(',', '.');
        }

        internal static DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
                diff += 7;
            return dt.AddDays(-diff).Date;
        }

        internal static DateTime EndOfWeek(DateTime dt, DayOfWeek endOfWeek)
        {
            int diff = dt.DayOfWeek - endOfWeek;
            if (diff < 0)
                diff += 7;
            return dt.AddDays(7 - diff).Date;
        }

        public static string ConvertTextToSlug(string s)
        {
            StringBuilder sb = new StringBuilder();
            bool wasHyphen = true;
            var nc = TrimVietnameseMark(s, true, false);
            foreach (char c in nc)
            {
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(c);
                    wasHyphen = false;
                }
                else if (char.IsWhiteSpace(c) && !wasHyphen)
                {
                    sb.Append('-');
                    wasHyphen = true;
                }
                else if (!wasHyphen)
                {
                    sb.Append('-');
                    wasHyphen = true;
                }
            }
            // Avoid trailing hyphens
            if (wasHyphen && sb.Length > 0)
                sb.Length--;
            return sb.ToString();
        }

        public static string filmUrl(string title, int id)
        {

            return "/phim/" + ConvertTextToSlug(title) + "-p" + id;
        }

        public static string catalogUrl(string title)
        {
            return "/danh-muc/" + ConvertTextToSlug(title);
        }
        public static string artistInfoUrl(string username)
        {
            return "/artist/" + ConvertTextToSlug(username);
        }
        public static string songInfoUrl(string title, int id)
        {
            return "/song/" + ConvertTextToSlug(title) + "-p" + id;
        }

        public static string film_contractInfoUrl(string title, int id)
        {
            return "/film_contract/" + ConvertTextToSlug(title) + "-p" + id;
        }
    }
}




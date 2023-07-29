namespace copyrights_fe.App
{
    public class JsonResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public int int_status
        {
            get
            {
                var t = 0;
                int.TryParse(status, out t);
                return t;
            }
        }
        public object data { get; set; }

        public static JsonResponse create()
        {
            return new JsonResponse() { status = "0", message = "Có lỗi trong quá trình xử lý!" };
        }
        public static JsonResponse create(int status)
        {
            return new JsonResponse() { status = status.ToString(), message = "" };
        }
        public static JsonResponse create(int status, string message)
        {
            return new JsonResponse() { status = status.ToString(), message = message };
        }
        public static JsonResponse create(int status, string message, object data)
        {
            return new JsonResponse() { status = status.ToString(), message = message, data = data };
        }
        public static JsonResponse create(int status, object data)
        {
            return new JsonResponse() { status = status.ToString(), message = "", data = data };
        }



        public static JsonResponse success()
        {
            return new JsonResponse() { status = "1", message = "success" };
        }
        public static JsonResponse success(string message)
        {
            return new JsonResponse() { status = "1", message = message };
        }
        public static JsonResponse success(object data)
        {
            return new JsonResponse() { status = "1", message = "success", data = data };
        }
        public static JsonResponse success(string message, object data)
        {
            return new JsonResponse() { status = "1", message = message, data = data };
        }


        public static JsonResponse error()
        {
            return new JsonResponse() { status = "0", message = "error" };
        }
        public static JsonResponse error(string message)
        {
            return new JsonResponse() { status = "0", message = message };
        }
        public static JsonResponse error(object data)
        {
            return new JsonResponse() { status = "0", message = "error", data = data };
        }
        public static JsonResponse error(string message, object data)
        {
            return new JsonResponse() { status = "0", message = message, data = data };
        }

    }
}
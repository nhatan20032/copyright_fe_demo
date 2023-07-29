using copyrights_fe.Services.Utilities;
using Microsoft.AspNetCore.Mvc;
using copyrights_fe.App;
using copyrights_fe.Model;
using copyrights_fe.Services;

namespace copyrights_fe.Controllers
{
    public class PublisherController : Controller
    {
        [Route("artist")]
        public IActionResult Index(int length, int start, string search, string catalog_id)
        {
            if (length == 0 || length == null)
            {
                length = 10;
            }
            var _film_cpSv = new Services.film_cpServices();
            if (catalog_id == null)
            {
                var list_vwfilm_cp = _film_cpSv.GetAllViewItem(new PagingModel() { offset = start, limit = length, search = search });
                if (list_vwfilm_cp == null) { return NotFound(); }
                return View(list_vwfilm_cp);
            }
            else
            {
                var list_vwfilm_cp = _film_cpSv.GetAllViewItem(new PagingModel()
                {
                    offset = start, /*limit = length,*/
                    search = search,
                    catalog_id = int.Parse(catalog_id)
                });
                if (list_vwfilm_cp == null) { return NotFound(); }
                return View(list_vwfilm_cp);
            }
        }
        [HttpGet]
        [Route("artist/List_catalog")]
        public PartialViewResult list_catalog(int length, int start, string search, string catalog_id)
        {
            if (length == 0 || length == null)
            {
                length = 10;
            }
            var _film_cpSv = new Services.film_cpServices();
            if (catalog_id == "-1")
            {
                var data1 = _film_cpSv.GetAllViewItem(new PagingModel() { offset = start, limit = length, search = search });
                if (data1 == null) { }
                return PartialView("list_catalog", data1);
            }
            var data = _film_cpSv.GetViewAllByCatalogId(new PagingModel() { offset = start, limit = length, search = search, catalog_id = int.Parse(catalog_id) });
            if (data == null) { }
            return PartialView("list_catalog", data);
        }
        [HttpGet]
        [Route("artist/{shortname}")]
        public IActionResult film_cp_info(string shortname)
        {
            film_cp_extend film_cp_ex = new film_cp_extend();
            var _film_cpSv = new Services.film_cpServices();
            var _filmSv = new Services.FilmService();
            var film_cp1 = _film_cpSv.searchByUrl(shortname);
            var film_cp_info = _film_cpSv.GetViewInfoById(film_cp1.Id.ToString());
            var listFilm = _filmSv.GetViewAllItemByFilmCpId(new PagingModel() { limit = 10000000 }, film_cp1.Id);
            film_cp_ex.vw_film_cp = film_cp_info;
            film_cp_ex.list_film_video = listFilm;
            return View(film_cp_ex);
        }
        [HttpGet]
        [Route("film_cp/get")]
        public JsonResult get(int id, string title)
        {
            return Json(new film_cpServices().get(id, title));
        }
    }
}

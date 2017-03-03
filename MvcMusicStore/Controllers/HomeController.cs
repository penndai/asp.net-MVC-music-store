using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcMusicStore.Models;
using System;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        MusicStoreEntities storeDB = new MusicStoreEntities();

        public ActionResult Index()
        {
            // Get most popular albums
            var albums = GetTopSellingAlbums(5);
			ViewData["Option"] = 1;

			return View(albums);
        }

		public ActionResult ToggleViewed()
		{
			var albums = GetTopViewedAlbums(5);
			ViewData["Option"] = 0;
			return View("Index", albums);
		}

		[HttpPost]
		public ActionResult Promotion()
		{
			return Content(storeDB.Albums.OrderBy(x=>Guid.NewGuid()).Take(1).Single().AlbumArtUrl);
		}

		private List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count
            return storeDB.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }

		private List<Album> GetTopViewedAlbums(int count)
		{
			return storeDB.Albums
				.OrderByDescending(a => a.Cart.Count())
				.Take(count)
				.ToList();
		}
	}
}
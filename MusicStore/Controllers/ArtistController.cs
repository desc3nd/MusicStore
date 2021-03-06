using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicStore.IServices;
using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistService _artistService;
        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }
        // GET: ArtistController
        public ActionResult Index()
        {
            var artists = _artistService.GetArtists();
            return View(artists);
        }

        // GET: ArtistController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           var artist =  _artistService.GetArtist(id.Value);
            return View(artist);
        }

        // GET: ArtistController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtistController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Artist artist)
        {
            try
            {
                _artistService.Create(artist);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArtistController/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null || !Account.isLoggedIn)
            {
                return NotFound();
            }
            var artist =_artistService.GetArtist(id);
            if(artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: ArtistController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Artist artist)
        {
            if(!Account.isLoggedIn)
            {
                return NotFound();
            }
            try
            {
                _artistService.Edit(artist);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArtistController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || !Account.isLoggedIn)
            {
                return NotFound();
            }

            var artist = _artistService.GetArtist(id);

            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: ArtistController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Artist artist)
        {
            if (!Account.isLoggedIn)
            {
                return NotFound();
            }
            try
            {
                _artistService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

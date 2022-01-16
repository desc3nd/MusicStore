using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.IServices;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        // GET: GenreController
        public ActionResult Index()
        {
            var genres = _genreService.GetGenres();
            return View(genres);
        }

        // GET: GenreController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GenreController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genre genre)
        {
            try
            {
                _genreService.Create(genre);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GenreController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || !Account.isLoggedIn)
            {
                return NotFound();
            }
            var genre = _genreService.GetGenre(id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // POST: GenreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Genre genre)
        {
            if (!Account.isLoggedIn)
            {
                return NotFound();
            }
            try
            {
                _genreService.Edit(genre);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GenreController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || !Account.isLoggedIn)
            {
                return NotFound();
            }

            var genre = _genreService.GetGenre(id);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // POST: GenreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Genre genre)
        {
            if (!Account.isLoggedIn)
            {
                return NotFound();
            }
            try
            {
                _genreService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

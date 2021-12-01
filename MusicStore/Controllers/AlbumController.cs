using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicStore.IServices;
using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IArtistService _artistService;
        private readonly IGenreService _genreService;
        public AlbumController(IAlbumService albumService, IArtistService artistService, IGenreService genreService)
        {
            _albumService = albumService;
            _artistService = artistService;
            _genreService = genreService;
        }
        // GET: AlbumController
        public ActionResult Index()
        {
            var albums = _albumService.GetAlbums();
            return View(albums);
        }

        // GET: AlbumController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AlbumController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AlbumController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //edit ten odpowiada za widok, tzn jeżeli klikniemy edit to otworzy nam się zakładka z widokiem editu i za to odpowiedzialna jest ta metoda
        // GET: AlbumController/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            //chwilowo robie to w ten sposób. Poprawnie byłoby wykorzystać tutaj metodę zwracającą pojedyńczy album, zatem: var album = _albumService.GetAlbum(id);
            var album = _albumService.GetAlbums().FirstOrDefault(x => x.Id == id);
            ViewData["Artists"] = new SelectList(_artistService.GetArtists().OrderBy(x => x.Name), "Id", "Name", album.ArtistId);
            ViewData["Genres"] = new SelectList(_genreService.GetGenres().OrderBy(x => x.Name), "Id", "Name", album.GenreId);
            return View(album);
        }
        //ta metoda natomiast odpowiedzialna jest za zapis edytowanych danych do bazy
        // POST: AlbumController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Album album)
        {
            if (id != album.Id)
            {
                return NotFound();
            }
            try
            {
                _albumService.Edit(album);

            }               

            catch
            {
                return View(album);
            }            
            return RedirectToAction(nameof(Index));

        }

        // GET: AlbumController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AlbumController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

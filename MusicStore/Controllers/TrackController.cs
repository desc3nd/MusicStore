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
    public class TrackController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly ITrackService _trackService;

        public TrackController(IAlbumService albumService, ITrackService trackService)
        {
            _albumService = albumService;
            _trackService = trackService;
        }

        // GET: TrackController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TrackController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || !Account.isLoggedIn)
            {
                return NotFound();
            }
            var track = _trackService.GetTrack(id);
            if (track == null)
            {
                return NotFound();
            }
            return View(track);
        }

        // GET: TrackController/Create
        [HttpGet]
        [Route("Track/Create/{albumName}")]
        public ActionResult Create(string albumName)
        {
            ViewData["AlbumName"] = albumName;
            ViewData["Artist"] = _albumService.GetAlbums().FirstOrDefault(x => x.Title == albumName).Artist.Id;
            ViewData["Genre"] = _albumService.GetAlbums().FirstOrDefault(x => x.Title == albumName).Genre.Id;
            ViewData["Album"] = _albumService.GetAlbums().FirstOrDefault(x => x.Title == albumName).Id;
            return View();
        }

        // POST: TrackController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Track track)
        {
            if (!Account.isLoggedIn)
            {
                return NotFound();
            }
            try
            {
                _trackService.Create(track);
            }
            catch
            {
                return View();
            }
            return RedirectToAction(nameof(Index), "Album");
        }

        // GET: TrackController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || !Account.isLoggedIn) 
            {
                return NotFound();
            }
            var track = _trackService.GetTrack(id);
            if(track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // POST: TrackController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Track track)
        {
            if (!Account.isLoggedIn)
            {
                return NotFound();
            }
            try
            {
                _trackService.Edit(track);
            }
            catch
            {
                return View();
            }
            return RedirectToAction(nameof(Index), "Album");
        }

        // GET: TrackController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || !Account.isLoggedIn)
            {
                return NotFound();
            }
            var track = _trackService.GetTrack(id);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // POST: TrackController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Track track)
        {
            if (!Account.isLoggedIn)
            {
                return NotFound();
            }
            try
            {
                _trackService.Delete(track.Id);
            }
            catch
            {
                return View();
            }
            return RedirectToAction(nameof(Index), "Album");
        }
    }
}

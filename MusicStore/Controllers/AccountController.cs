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
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService userService)
        {
            _accountService = userService;
        }

        // GET: UserController
        public ActionResult Index()
        {
            ViewData["IsAccountInDB"] = _accountService.IsAccountInDB();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Account account)
        {
            ViewData["IsAccountInDB"] = _accountService.IsAccountInDB();
            if (_accountService.LogIn(account))
            {
                return RedirectToAction(nameof(Index),"Album");
            }
            ViewData["InvalidLogin"] = "Invalid login or password";

            return View();
        }

        public ActionResult LogOut()
        {
            Account.isLoggedIn = false;
            return RedirectToAction(nameof(Index), "Album");
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            if (_accountService.IsAccountInDB()) 
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Account account)
        {
                _accountService.AddAccuount(account);
                return RedirectToAction(nameof(Index), "Album");
           
        }

        // GET: UserController/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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

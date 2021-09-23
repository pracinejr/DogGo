using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogGo.Models;
using DogGo.Repositories;
using DogGo.Models.ViewModels;

namespace DogGo.Controllers
{

    public class WalksController : Controller
    {
        private readonly IWalksRepository _walksRepo;
        private readonly IOwnerRepository _ownerRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalkerRepository _walkerRepo;

        public WalksController(IWalksRepository walksRepository, IOwnerRepository ownerRepository,
    IDogRepository dogRepository,
    IWalkerRepository walkerRepository)
        {
            _walksRepo = walksRepository;
            _ownerRepo = ownerRepository;
            _walkerRepo = walkerRepository;
            _dogRepo = dogRepository;
        }

        // GET: WalksControler
        public ActionResult Index()
        {
            List<Walks> walks = _walksRepo.GetAllWalks();
            WalksDetailViewModel vm = new WalksDetailViewModel()
            {
                Owner = new Owner(),
                Dog = new Dog(),
                Walker = new Walker(),
            };
            return View(vm);
        }

        // GET: WalksControler/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WalksControler/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalksControler/Create
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

        // GET: WalksControler/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalksControler/Edit/5
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

        // GET: WalksControler/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalksControler/Delete/5
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
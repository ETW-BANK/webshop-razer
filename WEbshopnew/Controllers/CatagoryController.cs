using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WEbshopnew.DataAccess.Data;
using WEbshopnew.DataAccess.Repository.IRepository;
using WEbshopnew.Models;

namespace webshoping.Controllers
{
    public class CatagoryController : Controller
    {
        private readonly ICatagoryRepository _catagoryRepository;

        public CatagoryController(ICatagoryRepository catagoryRepository)
        {
            _catagoryRepository = catagoryRepository;
        }
        [Authorize]
        public IActionResult Index()
        {
           List<Catagory> ctagorylist = _catagoryRepository.GetAll().ToList();
            return View(ctagorylist);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Catagory obj)
        {
            if (obj.CatagoryName == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Displayordere cannot exactly match he name");
            }
            if (ModelState.IsValid)
            {
                _catagoryRepository.Add(obj);
                _catagoryRepository.Save();
                TempData["Success"] = "Catagory Created Sucessfully";
                return RedirectToAction("Index");   
            }
            return View();
        }





        public IActionResult Edit(int id)
        {
            Catagory? catagoryfromdb = _catagoryRepository.Get(u => u.CatagoryId == id);
            if (id == null || id == 0)
            {
                return NotFound();
            }

           
            if (catagoryfromdb == null)
            {
                return NotFound();
            }
            return View(catagoryfromdb);
        }

        [HttpPost]

        public IActionResult Edit(Catagory? catagory)
        {
            if (ModelState.IsValid)
            {
                _catagoryRepository.Update(catagory);
                _catagoryRepository.Save();
                TempData["Success"] = "Catagory Updated Sucessfully";
                return RedirectToAction("Index");

            }

            return View();
        }
    



        [HttpPost]

        public IActionResult Delete(int? id)
        {
            Catagory? catagoryfromdb = _catagoryRepository.Get(u => u.CatagoryId == id);

            if(catagoryfromdb == null)
            {
                return NotFound();
            }
            _catagoryRepository.Remove(catagoryfromdb);
            _catagoryRepository.Save();
            TempData["Success"] = "Catagory deleted Sucessfully";
            return RedirectToAction("Index");
        }
       
    }

    
}

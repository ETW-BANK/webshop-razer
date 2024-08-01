using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEbshopnew.DataAccess.Data;
using WEbshopnew.DataAccess.Repository.IRepository;
using WEbshopnew.Models;
using WEbshopnew.Utilities;

namespace WEbshopnew.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Administrator)]
    public class CatagoryController : Controller
    {
       
        private readonly IUnitOfWork _unitofwork;

        public CatagoryController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            List<Catagory> ctagorylist = _unitofwork.Catagory.GetAll().ToList();
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
                _unitofwork.Catagory.Add(obj);
                 _unitofwork.Save();
                TempData["Success"] = "Catagory Created Sucessfully";
                return RedirectToAction("Index");
            }
            return View();
        }





        public IActionResult Edit(int id)
        {
            Catagory? catagoryfromdb = _unitofwork.Catagory.Get(u => u.CatagoryId == id);
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
               _unitofwork.Catagory.Update(catagory);
                _unitofwork.Save();
                TempData["Success"] = "Catagory Updated Sucessfully";
                return RedirectToAction("Index");

            }

            return View();
        }




        [HttpPost]

        public IActionResult Delete(int? id)
        {
            Catagory? catagoryfromdb = _unitofwork.Catagory.Get(u => u.CatagoryId == id);

            if (catagoryfromdb == null)
            {
                return NotFound();
            }
            _unitofwork.Catagory.Remove(catagoryfromdb);
            _unitofwork.Save();
            TempData["Success"] = "Catagory deleted Sucessfully";
            return RedirectToAction("Index");
        }

    }


}

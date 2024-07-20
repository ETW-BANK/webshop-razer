using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEbshopnew.DataAccess.Repository;
using WEbshopnew.DataAccess.Repository.IRepository;
using WEbshopnew.Models;

namespace WEbshopnew.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

    private readonly IUnitOfWork _unitOfWork;   
    
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
      
        }
        public IActionResult Index()
        {
            List<Products> productslist = _unitOfWork.Product.GetAll().ToList();
         
            return View(productslist);
        }
    
            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
       
        public IActionResult Create(Products product)
        {
            if (ModelState.IsValid)
            {
               

               _unitOfWork.Product.Add(product);
                _unitOfWork.Save();
                TempData["Success"] = "Product Added Successfully";
                return RedirectToAction("Index");
            }

            return View(product);
        }
    

    public IActionResult Edit(int id)
        {
            Products? productfromdb = _unitOfWork.Product.Get(u => u.ProductId == id);
            if (id == null || id == 0)
            {
                return NotFound();
            }


            if (productfromdb == null)
            {
                return NotFound();
            }
            return View(productfromdb);
        }


        [HttpPost]

        public IActionResult Edit(Products? product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();
                TempData["Success"] = "Product Updated Sucessfully";
                return RedirectToAction("Index");

            }

            return View();
        }

        [HttpPost]

        public IActionResult Delete(int? id) 
        {
            Products product =_unitOfWork.Product.Get(u=>u.ProductId==id);

            if(id == null || id == 0)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["Success"] = "Product deleted Sucessfully";
            return RedirectToAction("Index");   
        }
    }
}

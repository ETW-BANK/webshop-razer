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

        private readonly IProductsRepository _productsRepository;
        private readonly ICatagoryRepository _catagoryRepository;
        public ProductController(IProductsRepository productsRepository,ICatagoryRepository catagoryRepository)
        {
            _productsRepository = productsRepository;
            _catagoryRepository = catagoryRepository;
        }
        public IActionResult Index()
        {
            List<Products> productslist = _productsRepository.GetAll().ToList();
         
            return View(productslist);
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> catagorylist = _catagoryRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.CatagoryName,
                Value = u.CatagoryId.ToString()
            });

            ViewBag.catagorylist=catagorylist;
            return View();
        }

        [HttpPost]

        public IActionResult Create(Products product)
        {
            
           
            if (ModelState.IsValid)
            {
                _productsRepository.Add(product);
                _productsRepository.Save();
                TempData["Success"] = "Product Added Sucessfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            Products? productfromdb = _productsRepository.Get(u => u.ProductId == id);
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
                _productsRepository.Update(product);
                _productsRepository.Save();
                TempData["Success"] = "Product Updated Sucessfully";
                return RedirectToAction("Index");

            }

            return View();
        }

        [HttpPost]

        public IActionResult Delete(int? id) 
        {
            Products product = _productsRepository.Get(u=>u.ProductId==id);

            if(id == null || id == 0)
            {
                return NotFound();
            }

            _productsRepository.Remove(product);
            _productsRepository.Save();
            TempData["Success"] = "Product deleted Sucessfully";
            return RedirectToAction("Index");   
        }
    }
}

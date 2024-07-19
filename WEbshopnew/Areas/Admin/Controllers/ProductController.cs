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
        private readonly IWebHostEnvironment _webHostEnvironment;   
        public ProductController(IProductsRepository productsRepository,ICatagoryRepository catagoryRepository,IWebHostEnvironment webHostEnvironment)
        {
            _productsRepository = productsRepository;
            _catagoryRepository = catagoryRepository;
            _webHostEnvironment = webHostEnvironment;
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(Products product, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "images/products");
                    var filePath = Path.Combine(uploads, file.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    product.ImageUrl = "/images/products/" + file.FileName;
                }

                _productsRepository.Add(product);
                _productsRepository.Save();
                TempData["Success"] = "Product Added Successfully";
                return RedirectToAction("Index");
            }

            ViewBag.catagorylist = new SelectList(_catagoryRepository.GetAll(), "CatagoryId", "CatagoryName", product.CatagoryId);
            return View(product);
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEbshopnew.DataAccess.Repository;
using WEbshopnew.DataAccess.Repository.IRepository;
using WEbshopnew.Models;
using WEbshopnew.Models.ViewModels;
using WEbshopnew.Utilities;

namespace WEbshopnew.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Administrator)]
    public class ProductController : Controller
    {

    private readonly IUnitOfWork _unitOfWork;   
        private readonly IWebHostEnvironment _webHostEnvironment;
    
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            
            _webHostEnvironment=webHostEnvironment;
      
        }
        public IActionResult Index()
        {
            List<Products> productslist = _unitOfWork.Product.GetAll(includeproperties: "Category").ToList();
         
            return View(productslist);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CatagoryList = _unitOfWork.Catagory.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CatagoryName,
                    Value = u.CatagoryId.ToString()
                }),
                Product = new Products()
            };
            if(id == null || id == 0)
            {
                return View(productVM);
            }

            else
            {
                productVM.Product = _unitOfWork.Product.Get(u => u.ProductId == id);
                return View(productVM);
            }
            
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(obj.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if (obj.Product.ProductId == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                    TempData["Success"] = "Product Added Successfully";
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                    TempData["Success"] = "Product Updated Successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                obj.CatagoryList = _unitOfWork.Catagory.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CatagoryName,
                    Value = u.CatagoryId.ToString()
                });
            }

            return View(obj);
        }









      //  [HttpPost]

       // public IActionResult Delete(int? id) 
     //   //{
           // Products product =_unitOfWork.Product.Get(u=>u.ProductId==id);

          //  if(id == null || id == 0)
           // {
              //  return NotFound();
          //  }

           // _unitOfWork.Product.Remove(product);
          //  _unitOfWork.Save();
         //   TempData["Success"] = "Product deleted Sucessfully";
         //   return RedirectToAction("Index");   
      //  }

        #region API CALLS

        [HttpGet]

        public IActionResult GetAll()
        {
            List<Products> productslist = _unitOfWork.Product.GetAll(includeproperties: "Category").ToList();

            return Json(new {data=productslist});   
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var producttobedeleted = _unitOfWork.Product.Get(u => u.ProductId == id);

                if (producttobedeleted == null)
            {
                return Json(new {success=false,message="Error While Deleteing"});
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, producttobedeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(producttobedeleted);
            _unitOfWork.Save();

            List<Products> productslist = _unitOfWork.Product.GetAll(includeproperties: "Category").ToList();

            return Json(new { success = true, message="Deleted Successully" });
        }

        #endregion
    }
}

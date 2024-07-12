using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


using webshoping.Models;
using WEbshopnew.Data;

namespace webshoping.Controllers
{
    public class CatagoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatagoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
           List<Catagory> ctagorylist = _context.Catagories.ToList();
            return View(ctagorylist);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
  

        public async Task<IActionResult> GetCatagories()
        {
            var result = await _context.Catagories.Select(catagory => new Catagory
            {
                CatagoryId = catagory.CatagoryId,
                CatagoryName = catagory.CatagoryName,
                CatagoryDescription = catagory.CatagoryDescription
            }).ToListAsync();

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Catagory category)
        {
            var result=  _context.Catagories.FirstOrDefault(x=>x.CatagoryName == category.CatagoryName);

            if (result !=null) 
            
            {
                ModelState.AddModelError("", "Catagory name already exists.");
                return View(result);

            }
            else if (ModelState.IsValid)
            {
               
                _context.Catagories.Add(category);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Catagory Created Sucessfully";
              
                //return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpPost]

        public async Task<IActionResult> Delete( int id)
        {
            Catagory? catagory= await _context.Catagories.FindAsync(id);

        
            
            if (catagory == null)
            {
                return NotFound();
            }

             _context.Catagories.Remove(catagory);
            var result= await _context.SaveChangesAsync();  

            if (result>0)
            {
                TempData["Success"] = "Catagory Deleted Sucessfully";
                return RedirectToAction("Index");
            }


            return View(catagory);
        }

        public IActionResult Edit(int id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }

            Catagory? catagoryfromdb = _context.Catagories.Find(id);
            if(catagoryfromdb == null)
            {
                return NotFound();
            }
            return View(catagoryfromdb);
        }

        [HttpPost]

        public async Task<IActionResult> Edit( Catagory? catagory)
        {
            if(ModelState.IsValid)
            {
                 _context.Update(catagory);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Catagory Updated Sucessfully";
                return RedirectToAction("Index");   

            }

            return View();
        }
    }

    
}

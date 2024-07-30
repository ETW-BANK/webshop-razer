using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEbshopnew.DataAccess.Repository.IRepository;
using WEbshopnew.Models;


namespace WEbshopnew.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

       
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<Products> productList=_unitOfWork.Product.GetAll(includeproperties: "Category");


            return View(productList);
        }

        public IActionResult Details(int id)
        {
         
            Products product = _unitOfWork.Product.Get(u => u.ProductId == id,includeproperties: "Category");
           

            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

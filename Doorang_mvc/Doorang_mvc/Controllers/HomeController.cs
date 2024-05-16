using Doorang_mvc.DAL;
using Doorang_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Doorang_mvc.Controllers
{
    public class HomeController : Controller
    {

        AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Explore> products = _context.Explories.ToList();
            return View(products);
        }
    }
}

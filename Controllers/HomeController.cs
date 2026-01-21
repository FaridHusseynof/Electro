using System.Diagnostics;
using Electro.Data;
using Electro.Models;
using Microsoft.AspNetCore.Mvc;

namespace Electro.Controllers
{
    public class HomeController : Controller
    {
        private ElectroDbContext _context { get; }
        public HomeController(ElectroDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.products.Where(c=>!c.IsDeleted));
        }


    }
}


using ExamCode.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamCode.Controllers
{
    public class HomeController : Controller
    {
       private readonly IExploreService _exploreService;

        public HomeController(IExploreService exploreService)
        {
            _exploreService = exploreService;
        }
        public IActionResult Index()
        {
            var explores= _exploreService.GetAllExplores();
            return View(explores);
        }

       
    }
}

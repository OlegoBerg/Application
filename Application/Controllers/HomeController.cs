using Microsoft.AspNetCore.Mvc;
using Application.Models;


namespace Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContractRepository _contractRepository;

        public HomeController(IContractRepository contractRepository)
        {

            _contractRepository = contractRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        
    }
}
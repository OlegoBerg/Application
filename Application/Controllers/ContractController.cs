using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class ContractController : Controller
    {
        private readonly IContractRepository _contractRepository;

        public ContractController(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var contract = new Contract
            {
                Items = new List<ContractItem>() // ициализаця списка элементов
            };
            return View(contract);
        }

        [HttpPost]
        public IActionResult Create(Contract contract)
        {
            if (_contractRepository.GetContractById(contract.Id) != null)
            {
                
                return RedirectToAction("ContractExists");
            }

            if (contract.Items != null)
            {
                foreach (var item in contract.Items)
                {
                    Console.WriteLine($"Item ID: {item.Id}, Name: {item.Name}, Price: {item.Price}");
                }
            }

            bool isSaved = _contractRepository.SaveContract(contract);
            if (isSaved)
            {
                return RedirectToAction("Index");
            }
            else
            {

                ModelState.AddModelError("Id", "A contract with the same ID already exists.");

                var contractViewModel = new ContractViewModel
                {
                    Contract = contract,
                    TotalPrice = 0 // Здесь может быть любое значение, зависит от вашей логики
                };
                return View(contractViewModel);
            }
        }

        public IActionResult Index()
        {
            var contracts = _contractRepository.GetContracts2();
            return View(contracts);
        }

        public IActionResult ContractExists() => View();

        [HttpGet]
        public IActionResult CheckContractIdExists(int id)
        {
            var contractExists = _contractRepository.GetContractById(id) != null;
            return Json(new { exists = contractExists });
        }
       
    }
}

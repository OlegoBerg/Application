using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Models;

namespace Application.Controllers
{
    public class ViewResultController : Controller
    {

        private readonly IContractRepository _contractRepository;
        private readonly XmlConventer _xmlConventer;

        public ViewResultController(IContractRepository contractRepository, XmlConventer xmlConventer)
        {
            _contractRepository = contractRepository;
            _xmlConventer = xmlConventer;
        }

        public IActionResult ShowAllContracts()
        {
            var contracts = _contractRepository.GetContracts();
            var contractViewModels = contracts.Select(c => new ContractViewModel
            {
                Contract = c,
                TotalPrice = _xmlConventer.GetTotalPrice(c)
            }).ToList();

            return View(contractViewModels);
        }

     
    }
}

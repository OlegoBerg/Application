using Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;

namespace Application.Controllers
{
    public class ContractDetailsController : Controller
    {
        private readonly ContractsDbContext _context;

        public ContractDetailsController(ContractsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
         
            var contractXmlData = _context.Contracts
                .FirstOrDefault(c => c.Id == id);

            if (contractXmlData == null)
            {
                return NotFound();
            }

            
            Contract contract;
            var xmlSerializer = new XmlSerializer(typeof(Contract));
            using (var stringReader = new StringReader(contractXmlData.XmlData))
            {
                contract = (Contract)xmlSerializer.Deserialize(stringReader);
            }

            return View(contract);
        }
    }
}

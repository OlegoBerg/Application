using System.IO;
using System.Xml.Serialization;

namespace Application.Models
{
    public class ContractRepos : IContractRepository
    {
        private readonly ContractsDbContext _context;
        public ContractRepos(ContractsDbContext context)
        {
            _context = context;
        }
        public bool SaveContract(Contract contract)
        {
            if (_context.Contracts.Any(c => c.Id == contract.Id))
            {
                return false;
            }

            var xmlSerializer = new XmlSerializer(typeof(Contract));
            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, contract);
                var xmlData = stringWriter.ToString();

                var contractXmlData = new ContractXmlData
                {
                    Id = contract.Id,
                    XmlData = xmlData
                };

                _context.Contracts.Add(contractXmlData);
                _context.SaveChanges();
            }

            return true;
        }

        public List<Contract> GetContracts()
        {
            var contractXmlDataList = _context.Contracts.ToList();
            var contracts = new List<Contract>();
            var xmlSerializer = new XmlSerializer(typeof(Contract));

            foreach (var contractXmlData in contractXmlDataList)
            {
                using (var stringReader = new StringReader(contractXmlData.XmlData))
                {
                    var contract = (Contract)xmlSerializer.Deserialize(stringReader);
                    contracts.Add(contract);
                }
            }

            return contracts;
        }

        public List<ContractViewModel> GetContracts2() 
        {
            var xmlConventer = new XmlConventer();  
            var contracts = this.GetContracts();
            
            var viewModels = contracts.Select(contract => new ContractViewModel
            {
                Contract = contract,
                TotalPrice = xmlConventer.GetTotalPrice(contract)
            }).ToList();

            return viewModels;
        }
        

        public Contract GetContractById(int id)
        {
            var contractXmlData = _context.Contracts.FirstOrDefault(c => c.Id == id);
            if (contractXmlData == null)
            {
                return null;
            }

            var xmlSerializer = new XmlSerializer(typeof(Contract));
            using (var stringReader = new StringReader(contractXmlData.XmlData))
            {
                return (Contract)xmlSerializer.Deserialize(stringReader);
            }
        }
    }
    }


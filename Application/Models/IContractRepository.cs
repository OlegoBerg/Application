using System.Linq;

namespace Application.Models
{
    public interface IContractRepository
    {

        bool SaveContract(Contract contract);
        List<Contract> GetContracts();
        List<ContractViewModel> GetContracts2();
        Contract GetContractById(int id);
    }
}
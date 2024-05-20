using Microsoft.Extensions.Primitives;

namespace Application.Models
{
    [Serializable]
    public class ContractItem
    {
        public int Id { get; set; }
        public string Name {  get; set; }
       public decimal Price { get; set; }
        public int ContractId { get; set; }

    }
}

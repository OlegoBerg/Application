using Microsoft.EntityFrameworkCore;
using Application.Models;

namespace Application.Models
{
    public class ContractsDbContext : DbContext
    {
        public ContractsDbContext(DbContextOptions<ContractsDbContext> options) : base(options) { }

        public DbSet<ContractXmlData> Contracts => Set<ContractXmlData>();

      
    }
    public class ContractXmlData
    {
        public int Id { get; set; }
        public string XmlData { get; set; }
    }
}

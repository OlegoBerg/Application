using Application.Models;
using System;
using System.Collections.Generic;

namespace Application.Models
{
    [Serializable]
    public class Contract
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        /// <summary>
        private DateTime _utcDate;
        public DateTime UtcDate
        {
            get => _utcDate;
            set => _utcDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
        /// </summary>



        public List<ContractItem>? Items { get; 
            set; }
    }
}
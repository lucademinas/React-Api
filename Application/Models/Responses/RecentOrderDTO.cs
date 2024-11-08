using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses
{
    public class RecentOrderDto
    {
        public int OrderId { get; set; }
        public string ClientName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
    }
}
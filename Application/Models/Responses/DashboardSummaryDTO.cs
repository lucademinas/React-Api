using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses
{
    public class DashboardSummaryDto
    {
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public int TotalClients { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<RecentOrderDto> RecentOrders { get; set; }
    }
}
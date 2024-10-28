using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Today;
        public decimal Total {  get; set; }
    }
}

using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderDetailRepository : IBaseRepository<OrderDetail>
    {
        List<OrderDetail> GetAllBySaleOrder(int saleOrderId);
        List<OrderDetail> GetAllByProduct(int productId);
        OrderDetail? Get(int id);
    }
}

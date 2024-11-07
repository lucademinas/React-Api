using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class OrderDetailRepository : BaseRepository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationContext _context;
        public OrderDetailRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public List<OrderDetail> GetAllBySaleOrder(int saleOrderId)
        {
            return _context.OrderDetails
                .Include(p => p.Product)
                .Include(s => s.Order)
                .ThenInclude(c => c.Client)
                .Where(x => x.Id == saleOrderId)
                .ToList();
        }

        public List<OrderDetail> GetAllByProduct(int productId)
        {
            return _context.OrderDetails
                .Include(p => p.Product)
                .Include(s => s.Order)
                .ThenInclude(c => c.Client)
                .Where(x => x.Product.Id == productId)
                .ToList();
        }

        public OrderDetail? Get(int id)
        {
            return _context.OrderDetails
                .Include(p => p.Product)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}

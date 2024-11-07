using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly ApplicationContext _context;
        public OrderRepository(ApplicationContext context) : base(context) 
        {
            _context = context;
        }

        public List<Order> GetAllByClient(int clientId)
        {
            return _context.Orders
                .Include(cl => cl.Client)
                .Include(od => od.OrderDetails)
                .ThenInclude(p => p.Product)
                .Where(x => x.ClientId == clientId)
                .ToList();
        }

        public Order? Get(int id)
        {
            return _context.Orders
                .Include(cl => cl.Client)
                .Include(s => s.OrderDetails)
                .ThenInclude(p => p.Product)
                .FirstOrDefault(x => x.Id == id);

        }
    }
}

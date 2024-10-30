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
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationContext _context;
        public ProductRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public List<Product> GetAllByAdmin(int adminId)
        {
            return _context.Products
                .Include(x => x.Admin)
                .Where(x => x.AdminId == adminId)
                .ToList();
        }

        public List<Product> GetAll()
        {
            return _context.Products
                .Include(x => x.Admin)
                .ToList();
        }
    }
}

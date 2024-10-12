using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        List<Product> Get();
        Product? Get(int id);
        void Add(ProductDto dto);
        void Delete(int id);
        void Update(int id, ProductDto dto);

    }
}

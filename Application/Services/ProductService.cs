using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public List<Product> Get()
        {
            return _repository.GetAll();
        }

        public Product? Get(int id)
        {
            return _repository.Get(id);
        }

        public void Add(ProductDto dto)
        {
            var product = new Product
            {
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                Size = dto.Size,
            };
        }

        public void Delete(int id)
        {
            var productDelete = _repository.Get(id);
            if (productDelete != null)
            {
                _repository.Delete(productDelete);
            }

        }

        public void Update (int id, ProductDto dto)
        {
            var product = _repository.Get(id);
            if(product != null)
            {
                product.Description = dto.Description;
                product.Price = dto.Price;
                product.ImageUrl = dto.ImageUrl;
                product.Size = dto.Size;

                _repository.Update(product);
            }
        }
    }
}

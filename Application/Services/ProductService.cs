using Application.Interfaces;
using Application.Models;
using Application.Models.Responses;
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
        private readonly IProductRepository _productRepository;
        private readonly IAdminRepository _adminRepository;

        public ProductService(IProductRepository repository, IAdminRepository adminRepository)
        {
            _productRepository = repository;
            _adminRepository = adminRepository;
        }

        public List<ProductResponseDTO> Get()
        {
            var products = _productRepository.GetAll();
            return products.Select(s => new ProductResponseDTO
            {
                Id = s.Id,
                Description = s.Description,
                Price = s.Price,
                ImageUrl = s.ImageUrl,
                Admin = new AdminResponseDTO
                {
                    Id = s.Admin.Id,
                    Name = s.Admin.Name,
                    Email = s.Admin.Email,
                }
            }).ToList();
        }

        public Product? Get(int id)
        {
            return _productRepository.Get(id);
        }

        public List<ProductResponseDTO> GetAllByAdmin(int adminId)
        {
            var products = _productRepository.GetAllByAdmin(adminId);

            return products.Select(s => new ProductResponseDTO
            {
                Id = s.Id,
                Description = s.Description,
                Price = s.Price,
                ImageUrl = s.ImageUrl,
                Admin = new AdminResponseDTO
                {
                    Id = s.Admin.Id,
                    Name = s.Admin.Name,
                    Email = s.Admin.Email,
                }               
            }).ToList();
        }

        public void Add(int adminId, ProductDto dto)
        {
            var product = new Product
            {
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                Size = dto.Size,
                AdminId = adminId,
            };

            _productRepository.Add(product);
        }

        public void Delete(int id)
        {
            var productDelete = _productRepository.Get(id);
            if (productDelete != null)
            {
                _productRepository.Delete(productDelete);
            }

        }

        public void Update (int id, ProductDto dto)
        {
            var product = _productRepository.Get(id);
            if(product != null)
            {
                product.Description = dto.Description;
                product.Price = dto.Price;
                product.ImageUrl = dto.ImageUrl;
                product.Size = dto.Size;

                _productRepository.Update(product);
            }
        }
    }
}

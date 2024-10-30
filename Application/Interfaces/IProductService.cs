using Application.Models;
using Application.Models.Responses;
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
        List<ProductResponseDTO> Get();
        Product? Get(int id);
        void Add(int adminId, ProductDto dto);
        void Delete(int id);
        void Update(int id, ProductDto dto);
        List<ProductResponseDTO> GetAllByAdmin(int adminId);

    }
}

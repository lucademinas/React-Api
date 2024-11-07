using Application.Models;
using Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderDetailService
    {
        List<OrderDetailResponseDTO> GetAllBySaleOrder(int saleOrderId);
        List<OrderDetailResponseDTO> GetAllByProducts(int productId);
        OrderDetailResponseDTO? Get(int id);
        void Add(OrderDetailCreateDto dto);
        void Delete(int id);

    }
}

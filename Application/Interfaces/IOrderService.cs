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
    public interface IOrderService
    {
        List<OrderResponseDTO> GetAllByClient(int clientId);
        OrderResponseDTO? Get(int id);
        void Add(OrderDto createSaleOrder);
        void Delete(int id);
    }
}

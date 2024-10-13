using Application.Models;
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
        List<Order> Get();
        Order? Get(int id);
        void Add(OrderDto dto);
        void Delete(int id);
    }
}

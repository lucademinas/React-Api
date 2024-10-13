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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public List<Order> Get()
        {
            return _orderRepository.GetAll();
        }

        public Order? Get(int id)
        {
            return _orderRepository.Get(id);
        }

        public void Add(OrderDto dto)
        {
            var user = _userRepository.Get(dto.UserId);
            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            var order = new Order
            {
                User = user,
                OrderDate = dto.OrderDate,
                Total = dto.Total
            };

            _orderRepository.Add(order);
        }

        public void Delete(int id)
        {
            var orderDelete = _orderRepository.Get(id);
            if(orderDelete != null)
            {
                _orderRepository.Delete(orderDelete);
            }
        }
    }
}

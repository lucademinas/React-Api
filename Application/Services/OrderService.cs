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
        private readonly IClientRepository _clientRepository;

        public OrderService(IOrderRepository orderRepository, IClientRepository clientRepository)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
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
            var client = _clientRepository.Get(dto.ClientId);
            if (client == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            var order = new Order()
            {
                ClientId = dto.ClientId,
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

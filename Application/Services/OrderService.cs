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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public List<OrderResponseDTO> GetAllByClient(int clientId)
        {
            var saleOrders = _repository.GetAllByClient(clientId);

            return saleOrders.Select(s => new OrderResponseDTO
            {
                Id = s.Id,
                Total = s.Total,
                Client = new ClientResponseDTO
                {
                    Id = s.Client.Id,
                    Name = s.Client.Name,
                },
                OrderDetails = s.OrderDetails.Select(d => new OrderDetailResponseDTO
                {
                    Id = d.Id,
                    Amount = d.Quantity,
                    Product = new ProductResponseDTO
                    {
                        Id = d.Product.Id,
                        Description = d.Product.Description,
                        Price = d.Product.Price,
                    }
                }).ToList()

            }).ToList();
        }

        public OrderResponseDTO? Get(int id)
        {
            var order = _repository.Get(id);
            if (order is null)
            {
                return null;
            }
            return new OrderResponseDTO
            {
                Id = order.Id,
                Total = order.Total,
                Client = new ClientResponseDTO
                {
                    Id = order.Client.Id,
                    Name = order.Client.Name,

                },
                OrderDetails = order.OrderDetails.Select(s => new OrderDetailResponseDTO
                {
                    Id = s.Id,
                    Amount = s.Quantity,
                    Product = new ProductResponseDTO
                    {
                        Id = s.Product.Id,
                        Description = s.Product.Description,
                        Price = s.Product.Price
                    }

                }).ToList()
            };
        }

        public void Add(OrderDto createSaleOrder)
        {
            Order order = new Order()
            {
                ClientId = createSaleOrder.ClientId,

            };

            _repository.Add(order);

        }

        public void Delete(int id)
        {
            var saleOrder = _repository.Get(id);
            if (saleOrder is not null)
            {
                _repository.Delete(saleOrder);
            }
        }

        public List<AdminOrderSummaryDTO> GetOrdersByAdmin(int adminId)
        {
            // Obtiene todos los pedidos que incluyen productos de este administrador
            var orders = _repository.GetOrdersByAdmin(adminId);

            // Mapea las órdenes a AdminOrderSummaryDTO
            var result = orders.Select(order => new AdminOrderSummaryDTO
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                Total = order.Total,
                Client = new ClientResponseDTO
                {
                    Id = order.Client.Id,
                    Name = order.Client.Name,
                    Email = order.Client.Email
                },
                // Filtra y mapea los productos asociados a este administrador
                Products = order.OrderDetails
                    .Where(d => d.Product.AdminId == adminId)  // Filtra solo los productos del administrador
                    .Select(d => new ProductOrderSummaryDTO
                    {
                        ProductId = d.Product.Id,
                        Description = d.Product.Description,
                        Quantity = d.Quantity,
                        TotalPrice = d.Quantity * d.Product.Price
                    })
                    .ToList()
            }).ToList();

            return result;
        }




    }
}

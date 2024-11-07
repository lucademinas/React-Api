using Application.Interfaces;
using Application.Models.Responses;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;

        public DashboardService(
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IClientRepository clientRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
        }

        public DashboardSummaryDto GetDashboardSummary(int adminId)
        {
            // Total de productos del admin
            var totalProducts = _productRepository.GetAllByAdmin(adminId).Count;

            // Total de órdenes (puedes ajustar según tu lógica de negocio)
            var totalOrders = _orderRepository.GetAll().Count;

            // Total de clientes
            var totalClients = _clientRepository.GetAll().Count;

            // Calcular ingresos totales
            var totalRevenue = _orderRepository.GetAll().Sum(o => o.Total);

           

            // Órdenes recientes
            var recentOrders = _orderRepository.GetAll()
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .Select(o => new RecentOrderDto
                {
                    OrderId = o.Id,
                    ClientName = o.Client.Name,
                    OrderDate = o.OrderDate,
                    Total = o.Total
                })
                .ToList();

            return new DashboardSummaryDto
            {
                TotalProducts = totalProducts,
                TotalOrders = totalOrders,
                TotalClients = totalClients,
                TotalRevenue = totalRevenue,
                RecentOrders = recentOrders
            };
        }
    }
}

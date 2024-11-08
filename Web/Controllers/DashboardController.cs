using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domain.Interfaces;
using System.Linq;
using System;
using System.Collections.Generic;
using Application.Models.Responses;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IClientRepository _clientRepository;

        public DashboardController(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IClientRepository clientRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _clientRepository = clientRepository;
        }

        [HttpGet("summary")]
        public IActionResult GetDashboardSummary()
        {
            try
            {
                // Obtener el ID del admin desde el token
                var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                // Total de productos del admin
                var totalProducts = _productRepository.GetAllByAdmin(adminId).Count;

                // Total de órdenes
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

                var dashboardSummary = new DashboardSummaryDto
                {
                    TotalProducts = totalProducts,
                    TotalOrders = totalOrders,
                    TotalClients = totalClients,
                    TotalRevenue = totalRevenue,
                    RecentOrders = recentOrders
                };

                return Ok(dashboardSummary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("sales")]
        public IActionResult GetSalesData(string period)
        {
            try
            {
                List<SalesDataDto> salesData = period.ToUpper() switch
                {
                    "SEMANAL" => GetWeeklySales(),
                    "MENSUAL" => GetMonthlySales(),
                    "ANUAL" => GetAnnualSales(),
                    _ => GetMonthlySales()
                };

                return Ok(salesData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener datos de ventas: {ex.Message}");
            }
        }

        private List<SalesDataDto> GetMonthlySales()
        {
            return _orderRepository.GetAll()
                .GroupBy(o => o.OrderDate.ToString("MMM"))
                .Select(g => new SalesDataDto
                {
                    Month = g.Key,
                    Sales = g.Sum(o => o.Total)
                })
                .OrderBy(x => x.Month)
                .ToList();
        }

        private List<SalesDataDto> GetWeeklySales()
        {
            var now = DateTime.Now;
            return _orderRepository.GetAll()
                .Where(o => o.OrderDate >= now.AddDays(-30)) // Últimos 30 días
                .GroupBy(o => GetWeekNumber(o.OrderDate))
                .Select(g => new SalesDataDto
                {
                    Month = $"Semana {g.Key}",
                    Sales = g.Sum(o => o.Total)
                })
                .OrderBy(x => x.Month)
                .ToList();
        }

        private List<SalesDataDto> GetAnnualSales()
        {
            return _orderRepository.GetAll()
                .GroupBy(o => o.OrderDate.Year)
                .Select(g => new SalesDataDto
                {
                    Month = g.Key.ToString(),
                    Sales = g.Sum(o => o.Total)
                })
                .OrderBy(x => x.Month)
                .ToList();
        }

        private int GetWeekNumber(DateTime date)
        {
            return (date.DayOfYear - 1) / 7 + 1;
        }
    }

}
using Application.Models.Responses;
using Domain.Interfaces;
using Application.Models;
using Domain.Entities;
using Application.Interfaces;


namespace Application.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderDetailService(IOrderDetailRepository saleOrderDetailRepository, IProductRepository productRepository, IOrderRepository saleOrderRepository)
        {
            _orderDetailRepository = saleOrderDetailRepository;
            _productRepository = productRepository;
            _orderRepository = saleOrderRepository;
        }

        public List<OrderDetailResponseDTO> GetAllBySaleOrder(int saleOrderId)
        {
            var saleOrder = _orderRepository.Get(saleOrderId);
            return saleOrder.OrderDetails.Select(detail => new OrderDetailResponseDTO
            {
                Id = detail.Id,
                Amount = detail.Quantity,
                Product = new ProductResponseDTO
                {
                    Id = detail.Product.Id,
                    Description = detail.Product.Description,
                    Price = detail.Product.Price
                }
            }).ToList();

        }

        public List<OrderDetailResponseDTO> GetAllByProducts(int productId)
        {
            var product = _productRepository.Get(productId);

            var saleOrderDetails = _orderDetailRepository.GetAllByProduct(productId);
            return saleOrderDetails.Select(s => new OrderDetailResponseDTO
            {
                Id = s.Id,
                Amount = s.Quantity,
                Product = new ProductResponseDTO
                {
                    Id = s.Product.Id,
                    Description = s.Product.Description,
                    Price = s.Product.Price
                }
            }).ToList();
        }

        public OrderDetailResponseDTO? Get(int id)
        {
            var saleOrderDetail = _orderDetailRepository.Get(id);
            if (saleOrderDetail is null)
            {
                return null;
            }
            return new OrderDetailResponseDTO
            {
                Id = saleOrderDetail.Id,
                Amount = saleOrderDetail.Quantity,
                Product = new ProductResponseDTO
                {
                    Id = saleOrderDetail.Product.Id,
                    Description = saleOrderDetail.Product.Description,
                    Price = saleOrderDetail.Product.Price
                }
            };
        }

        public void Add(OrderDetailCreateDto dto)
        {
            var product = _productRepository.Get(dto.ProductId);


            var orderDetail = new OrderDetail()
            {
                ProductId = dto.ProductId,
                OrderId = dto.SaleOrderId,
                Quantity = dto.Amount,
            };

            _orderDetailRepository.Add(orderDetail);

            var order = _orderRepository.Get(dto.SaleOrderId);
            if (order is not null)
            {
                order.Total += orderDetail.Quantity * product.Price;
                _orderRepository.Update(order);
            }
        }

        public void Delete(int id)
        {
            var saleOrderDelete = _orderDetailRepository.Get(id);
            if (saleOrderDelete is not null)
            {
                var saleOrder = _orderRepository.Get(saleOrderDelete.OrderId);
                if (saleOrder is not null)
                {
                    saleOrder.Total -= saleOrderDelete.Quantity * saleOrderDelete.Product.Price;
                    _orderRepository.Update(saleOrder);
                }

                _orderDetailRepository.Delete(saleOrderDelete);
            }
        }
    }
}

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace KwanProperty.Services.Order.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<Models.Order> _logger;

        public OrderService(ILogger<Models.Order> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Models.Order> GetOrders()
        {
            _logger.LogInformation("OrderService|GetOrders|Starting to get orders");

            return new List<Models.Order>
            {
                new Models.Order
                {
                    OrderId = Guid.NewGuid(),
                    UserName = "QuanTN",
                    TotalPrice = 1500000,
                    FirstName = "Quan",
                    LastName = "Tran",
                    Email = "tranngocquan95vn@gmail.com",
                    Address = "Kham Thien",
                    PaymentMethod = 1,
                    CreatedBy = "QuanTN",
                    CreatedDate = DateTime.Today
                },
                new Models.Order
                {
                    OrderId = Guid.NewGuid(),
                    UserName = "MaiVN",
                    TotalPrice = 100000,
                    FirstName = "Mai",
                    LastName = "Tran",
                    Email = "maivn@gmail.com",
                    Address = "Ha Dong",
                    PaymentMethod = 2,
                    CreatedBy = "MaiVN",
                    CreatedDate = DateTime.Today
                },
            };
        }
    }
}

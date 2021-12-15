using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KwanProperty.Services.Order.Models;

namespace KwanProperty.Services.Order.Services
{
    public interface IOrderService
    {
        IEnumerable<Models.Order> GetOrders();
    }
}

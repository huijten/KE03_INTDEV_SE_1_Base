using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class OrderHistoryModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        public OrderHistoryModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IEnumerable<Order> CustomerOrders { get; set; } = new List<Order>();

        public void OnGet()
        {
            int customerId = 1;
            CustomerOrders = _orderRepository
                .GetAllOrders()
                .Where(o => o.CustomerId == customerId)
                .ToList();
        }
    }
}
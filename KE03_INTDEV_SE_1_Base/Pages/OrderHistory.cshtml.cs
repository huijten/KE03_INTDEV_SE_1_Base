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
        #region Dependency Injection
        private readonly IOrderRepository _orderRepository;

        public OrderHistoryModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        #endregion
        
        #region Properties
        public IEnumerable<Order> CustomerOrders { get; set; } = new List<Order>();
        #endregion
        
        #region GET request method
        public void OnGet()
        {
            int customerId = 1;
            CustomerOrders = _orderRepository
                .GetAllOrders()
                .Where(o => o.CustomerId == customerId)
                .ToList();
        }
        #endregion
    }
}
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class OrderModel : PageModel
    {
        #region Dependency Injection
        private readonly IPartRepository _partRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        public OrderModel(IPartRepository partRepository, IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _partRepository = partRepository;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }
        #endregion
        
        #region Properties
        public List<(Part Part, int Quantity)> GroupedCartItems { get; set; } = new();
        public double TotalPrice => GroupedCartItems.Sum(item => item.Part.Price * item.Quantity);
        #endregion
        
        #region GET request method
        public void OnGet()
        {
            LoadCart();
        }
        #endregion

        #region POST request method
        public IActionResult OnPostPlaceOrder()
        {
            LoadCart();

            if (!GroupedCartItems.Any())
            {
                return RedirectToPage("/Index");
            }
            
            Order order = new Order
            {
                CustomerId = 1,
                Customer = _customerRepository.GetCustomerById(1),
                OrderDate = DateTime.Now
            };

            foreach (var item in GroupedCartItems)
            {
                order.Parts.Add(item.Part); 
            }

            _orderRepository.AddOrder(order);
            Console.Write(order);

            return RedirectToPage("OrderConfirmation");
        }
        #endregion
        
        #region Load shopping cart method
        private void LoadCart()
        {
            var partIdsJson = TempData["CartPartIds"] as string;
            TempData.Keep("CartPartIds");

            var partIds = string.IsNullOrEmpty(partIdsJson)
                ? new List<int>()
                : JsonSerializer.Deserialize<List<int>>(partIdsJson);

            if (partIds != null)
            {
                var grouped = partIds
                    .GroupBy(id => id)
                    .Select(g =>
                    {
                        var part = _partRepository.GetPartById(g.Key);
                        return part != null ? (part, g.Count()) : default;
                    })
                    .Where(item => item.part != null)
                    .ToList();

                GroupedCartItems = grouped;
            }
        }
        #endregion
    }
}

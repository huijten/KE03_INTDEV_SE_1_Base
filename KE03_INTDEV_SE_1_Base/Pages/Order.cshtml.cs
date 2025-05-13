using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class OrderModel : PageModel
    {
        private readonly IPartRepository _partRepository;
        private readonly ILogger<OrderModel> _logger;

        public OrderModel(IPartRepository partRepository, ILogger<OrderModel> logger)
        {
            _partRepository = partRepository;
            _logger = logger;
        }

        public List<(Part Part, int Quantity)> GroupedCartItems { get; set; } = new();
        public double TotalPrice => GroupedCartItems.Sum(item => item.Part.Price * item.Quantity);

        public void OnGet()
        {
            LoadCart();
        }

        public IActionResult OnPostPlaceOrder()
        {
            LoadCart();

            if (!GroupedCartItems.Any())
            {
                return RedirectToPage("/Index");
            }

            // Here you would typically save the order to the database, send a confirmation email, etc.
            _logger.LogInformation("Order placed with {Count} items and total €{Total}", GroupedCartItems.Count, TotalPrice);

            TempData.Remove("CartPartIds");

            return RedirectToPage("OrderConfirmation");
        }

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
    }
}

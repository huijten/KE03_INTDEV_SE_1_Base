using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Interfaces; // <-- Add this using directive for the interface
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace KE03_INTDEV_SE_1_Base.Pages;

public class OrderConfirmation : PageModel
{
    #region Dependency Injection
    private readonly IPartRepository _partRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;

    public OrderConfirmation(IPartRepository partRepository, ICustomerRepository customerRepository, IOrderRepository orderRepository)
    {
        _partRepository = partRepository;
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
    }
    #endregion

    #region GET request method
    public void OnGet()
    {
        List<int> partIds = TempData["CartPartIds"] as List<int>;
    }
    #endregion
    
    #region POST request method
    public IActionResult OnPostReturnHome()
    {
        TempData.Remove("CartPartIds");
        return RedirectToPage("/Index");
    }
    #endregion
}
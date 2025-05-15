using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Repositories;


namespace KE03_INTDEV_SE_1_Base.Pages;

public class OrderConfirmation : PageModel
{
    private readonly PartRepository _partRepository;

    public OrderConfirmation(PartRepository partRepository)
    {
        _partRepository = partRepository;
    }

    public void OnGet()
    {

    }

    public IActionResult OnPostReturnHome()
    {
        List<int> partIds = TempData["CartPartIds"] as List<int>;

        if (partIds != null)
        {
            List<Part> partsInCart = new List<Part>();

            foreach (var k in partIds)
            {
                Part part = _partRepository.GetPartById(k);
                if (part != null)
                {
                    partsInCart.Add(part);
                }
            }
        }
        else
        {
            TempData["ErrorMessage"] = "Cart data not found or invalid.";
            return RedirectToPage("/Cart");
        }

        TempData.Remove("CartPartIds");
        return RedirectToPage("/Index");
    }
}
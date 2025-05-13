using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace KE03_INTDEV_SE_1_Base.Pages;

public class OrderConfirmation : PageModel
{
    public void OnGet()
    {
        
    }
    
    public IActionResult OnPostReturnHome()
    {
        TempData.Remove("CartPartIds");
        return RedirectToPage("/Index");
    }
}
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPartRepository _partsRepository;

        public IList<Part> Parts { get; set; } = new List<Part>();
        
        public static List<(int PartId, int Quantity)> Cart = new();

        [BindProperty(SupportsGet = true)] public string? SearchTerm { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IPartRepository partsRepository)
        {
            _logger = logger;
            _partsRepository = partsRepository;
        }

        public void OnGet()
        {
            var allParts = _partsRepository.GetAllParts();

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                Parts = allParts
                    .Where(p => p.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                _logger.LogInformation($"Filtered parts count: {Parts.Count} using searchTerm: '{SearchTerm}'");
            }
            else
            {
                Parts = allParts.ToList();
                _logger.LogInformation($"Getting all {Parts.Count} parts.");
            }
        }

        public async Task<IActionResult> OnPostAddToCart(int partId, int quantity)
        {
            var partList = TempData.ContainsKey("CartPartIds")
                ? JsonSerializer.Deserialize<List<int>>(TempData["CartPartIds"] as string)
                : new List<int>();

            partList.Add(partId);

            TempData["CartPartIds"] = JsonSerializer.Serialize(partList);
            TempData.Keep("CartPartIds");
            return RedirectToPage("Index");
        }
    }
}
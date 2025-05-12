using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPartRepository _partsRepository;

        public IList<Part> Parts { get; set; } = new List<Part>();

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
    }
}
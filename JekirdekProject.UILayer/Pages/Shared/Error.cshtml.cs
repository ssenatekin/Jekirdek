using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JekirdekProject.UILayer.Pages.Shared
{
    public class ErrorModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // Burada hata mesajý veya daha fazla iþlem yapýlabilir
            _logger.LogError("A global error has occurred.");
        }
    }
}

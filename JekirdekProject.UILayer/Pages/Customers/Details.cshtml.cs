using JekirdekProject.BusinessLayer.Services;
using JekirdekProject.DataAccessLayer.EntityFramework;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JekirdekProject.UILayer.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public DetailsModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Customer = await _customerService.GetCustomerById(id);

            if (Customer == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}

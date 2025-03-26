using JekirdekProject.BusinessLayer.Services;
using JekirdekProject.DataAccessLayer.EntityFramework;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JekirdekProject.UILayer.Pages.Customers
{
    public class UpdateModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public UpdateModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Customer.RegistrationDate = DateTime.SpecifyKind(Customer.RegistrationDate, DateTimeKind.Utc);

            await _customerService.UpdateCustomer(Customer);
            return RedirectToPage("Index");
        }
    }
}

using JekirdekProject.BusinessLayer.Services;
using JekirdekProject.DataAccessLayer.EntityFramework;
using JekirdekProject.UILayer.Pages.Login;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JekirdekProject.UILayer.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ICustomerService customerService, ILogger<CreateModel> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public void OnGet()
        {
            Customer = new Customer();
            Customer.RegistrationDate = DateTime.UtcNow;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {              
                Customer.RegistrationDate = DateTime.SpecifyKind(Customer.RegistrationDate, DateTimeKind.Utc);
                await _customerService.AddCustomer(Customer);
                _logger.LogInformation("Customer {CustomerName} created successfully at {Time}", Customer.FirstName + " " + Customer.LastName, DateTime.UtcNow);
                // return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating customer {CustomerName} at {Time}", Customer.FirstName + " " + Customer.LastName, DateTime.UtcNow);
            }
            return RedirectToPage("Index");
        }
    }
}

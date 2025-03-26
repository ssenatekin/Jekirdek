using JekirdekProject.BusinessLayer.Services;
using JekirdekProject.DataAccessLayer.EntityFramework;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JekirdekProject.UILayer.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<DeleteModel> _logger;


        public DeleteModel(ICustomerService customerService, ILogger<DeleteModel> logger)
        {
            _customerService = customerService;
            _logger = logger;
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            try
            {           
                if (customer != null)
                {
                    await _customerService.DeleteCustomer(id);
                    _logger.LogInformation("Customer {CustomerName} with ID {CustomerId} deleted successfully at {Time}", customer.FirstName + " " + customer.LastName, customer.Id, DateTime.UtcNow);
                    return RedirectToPage("./Index"); 
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error occurred while deleting customer with ID {CustomerId} at {Time}", customer.Id, DateTime.UtcNow);
            }

            return NotFound();
        }
    }
}

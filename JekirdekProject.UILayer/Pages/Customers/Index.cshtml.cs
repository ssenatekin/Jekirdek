using JekirdekProject.BusinessLayer.Services;
using JekirdekProject.DataAccessLayer.EntityFramework;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JekirdekProject.UILayer.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public IndexModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public List<Customer> Customers { get; set; }

        //public async Task OnGetAsync()
        //{
        //    Customers = (await _customerService.GetAllCustomers()).ToList();
        //}
     
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var customers = (await _customerService.GetAllCustomers()).ToList();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                customers = customers.Where(c => c.FirstName.Contains(SearchTerm) || c.LastName.Contains(SearchTerm)).ToList();
            }

            Customers = customers;
            return Page();
        }
    }
}

using JekirdekProject.DataAccessLayer.EntityFramework;
using JekirdekProject.DataAccessLayer.Repository;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekProject.BusinessLayer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository repository, ILogger<CustomerService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers() => await _repository.GetAllAsync();
        public async Task<Customer> GetCustomerById(int id) => await _repository.GetByIdAsync(id);
        public async Task AddCustomer(Customer customer) /*{ await _repository.AddAsync(customer); await _repository.SaveAsync(); }*/
        {
            try
            {
                await _repository.AddAsync(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateCustomerAsync: {ex.Message}");
                throw new Exception("An error occurred while creating the customer.");
            }
        }
        public async Task UpdateCustomer(Customer customer) { await _repository.UpdateAsync(customer); await _repository.SaveAsync(); }
        public async Task DeleteCustomer(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer != null)
            {
                //
                await _repository.DeleteAsync(customer.Id);
                await _repository.SaveAsync();
            }
        }
        public async Task<IEnumerable<Customer>> GetFilteredCustomers(string? name, string? email, DateTime? registrationDate, string? region)
        {
            var customers = await _repository.GetAllAsync();

            if (!string.IsNullOrEmpty(name))
                customers = customers.Where(c => c.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(email))
                customers = customers.Where(c => c.Email.Contains(email, StringComparison.OrdinalIgnoreCase));

            if (registrationDate.HasValue)
                customers = customers.Where(c => c.RegistrationDate.Date == registrationDate.Value.Date);

            if (!string.IsNullOrEmpty(region))
                customers = customers.Where(c => c.Region.Contains(region, StringComparison.OrdinalIgnoreCase));

            return customers;
        }
    }
}

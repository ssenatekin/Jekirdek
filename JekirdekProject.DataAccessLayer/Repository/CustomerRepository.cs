using JekirdekProject.DataAccessLayer.Concrete;
using JekirdekProject.DataAccessLayer.EntityFramework;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekProject.DataAccessLayer.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly Context _context;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(Context context, ILogger<CustomerRepository> logger) : base(context,logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<IEnumerable<Customer>> GetCustomersByRegion(string region)
        {
            return await _dbSet.Where(c => c.Region == region).ToListAsync();
        }
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }
    }
}

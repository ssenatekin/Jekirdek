using JekirdekProject.DataAccessLayer.EntityFramework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekProject.DataAccessLayer.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetCustomersByRegion(string region);
        Task<IEnumerable<Customer>> GetAllAsync();
    }
}

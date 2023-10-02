using Paging.API.Models;

namespace Paging.API.Contracts
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        PagedList<Customer> GetCustomers(PagedParameters custParameters);
        Customer GetCustomerById(Guid custId);
    }
}

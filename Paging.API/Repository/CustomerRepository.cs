using Paging.API.Contracts;
using Paging.API.Data;
using Paging.API.Models;

namespace Paging.API.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public PagedList<Customer> GetCustomers(PagedParameters custParameters)
        {
            return PagedList<Customer>.ToPagedList(FindAll(),
                custParameters.PageNumber,
                custParameters.PageSize);
        }

        public Customer GetCustomerById(Guid custId)
        {
            return FindByCondition(cust => cust.CustomerID.Equals(custId))
                .DefaultIfEmpty(new Customer())
                .FirstOrDefault();
        }

    }
}

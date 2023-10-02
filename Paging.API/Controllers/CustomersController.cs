using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using Paging.API.Contracts;
using Paging.API.Data;
using Paging.API.Models;

namespace Paging.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ICustomerRepository _repository;

        public CustomersController(DataContext context, ICustomerRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET: api/Customers
        [HttpGet]
        [Route("getall-data")]
        public async Task<ActionResult<IEnumerable<Customer>>> Getcustomers()
        {
            if (_context.customers == null)
            {
                return NotFound();
            }
            return await _context.customers.ToListAsync();
        }

        [HttpGet]
        [Route("getpaging-by-param")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetcustomersByFilter(PagedParameters ownerParameters)
        {
            if (_context.customers == null)
            {
                return NotFound();
            }
            return await _context.customers.OrderBy(on => on.CustomerID)
          .Skip((ownerParameters.PageNumber - 1) * ownerParameters.PageSize)
          .Take(ownerParameters.PageSize).ToListAsync();
        }

        [HttpGet]
        [Route("paging-filter")]
        public IActionResult GetCustomerPagingData([FromQuery] PagedParameters custParameters)
        {
            var customer = _repository.GetCustomers(custParameters);

            var metadata = new
            {
                customer.TotalCount,
                customer.PageSize,
                customer.CurrentPage,
                customer.TotalPages,
                customer.HasNext,
                customer.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(customer);
        }
    }
}

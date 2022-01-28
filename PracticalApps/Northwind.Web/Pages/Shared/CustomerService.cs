using Packt.Shared;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;

namespace Northwind.Web.Pages.Shared
{
    public interface ICustomerService
    {
        Customer? GetCustomer();
    }

    public class CustomerService : ICustomerService
    {
        private readonly NorthwindContext _context;
        private readonly HttpContext? _httpContext;

        public CustomerService(NorthwindContext context,
             IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContext = httpContextAccessor.HttpContext;
        }
        public Customer? GetCustomer()
        {
            string? customerId = _httpContext!.Request.Query["customer"];
            
            if (customerId is not null)
            {
                return _context.Find<Customer>(customerId);
            }
            else
            {
                return null;
            };
        }
    }
}

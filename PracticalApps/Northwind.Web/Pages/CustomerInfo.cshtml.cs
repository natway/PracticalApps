using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared;
using Northwind.Web.Pages.Shared;

namespace Northwind.Web.Pages;

public class CustomerInfoModel : PageModel
{
    [BindProperty]
    public Customer? customer { get; set; }
    private NorthwindContext db;
    private ICustomerService _customerService;
    public CustomerInfoModel(NorthwindContext injectedContext, ICustomerService customerService)
    {
        db = injectedContext;
        _customerService = customerService;
        // this function works if called here or in OnGet() -- I don't know which is better practice
        customer = _customerService.GetCustomer();
    }
    public void OnGet(string customerId)
    {
        
        ViewData["Title"] = "Northwind B2B - Customer Info";
     //   customer = _customerService.GetCustomer();
    }
}

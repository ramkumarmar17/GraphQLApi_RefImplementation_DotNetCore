using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CustomerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private List<Customer> _customers;

        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger)
        {
            _logger = logger;
            _customers = GetCustomers();
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Customer> GetAllCustomers()
        {
            Console.WriteLine($"GetAllCustomers: Time: {DateTime.Now.ToString("o")}");
            Thread.Sleep(1000);
            return _customers;
        }

        [HttpGet]
        [Route("{id}")]
        public Customer GetCustomer(string id)
        {
            Console.WriteLine($"GetCustomer: Time: {DateTime.Now.ToString("o")}");
            Thread.Sleep(1000);
            var customer = _customers.FirstOrDefault(c => c.Id.Equals(id));
            return customer ?? new Customer();
        }

        private List<Customer> GetCustomers()
        {
            return new List<Customer> {
                new Customer {
                    Id = "110011",
                    FullName = "Raj",
                    Address = new Address { Building = "111", Area = "aaa", Street = "10th street", City = "Bangalore", State = "Karnataka", ZipCode = "560016", Country = "India" },
                    Gender = "Male",
                    Occupation = "IT",
                    SocialSecurityId = Guid.NewGuid().ToString(),
                    DateOfBirth = new DateTime(2000, 3, 17)
                },
                new Customer {
                    Id = "220022",
                    FullName = "Vijay",
                    Address = new Address { Building = "222", Area = "bbb", Street = "14th street", City = "Chennai", State = "TamilNadu", ZipCode = "600010", Country = "India" },
                    Gender = "Male",
                    Occupation = "IT",
                    SocialSecurityId = Guid.NewGuid().ToString(),
                    DateOfBirth = new DateTime(1990, 1, 15)
                },
                new Customer {
                    Id = "330033",
                    FullName = "Charan",
                    Address = new Address { Building = "333", Area = "ccc", Street = "5th street", City = "Hyderabad", State = "Telangana", ZipCode = "500045", Country = "India" },
                    Gender = "Male",
                    Occupation = "IT",
                    SocialSecurityId = Guid.NewGuid().ToString(),
                    DateOfBirth = new DateTime(2010, 5, 10)
                }
            };
        }

       
    }
}

using CustomersAPI.Contracts;
using CustomersAPI.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ILogger<CustomerController> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            this._customerRepository = customerRepository;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var res = await _customerRepository.GetById(id);
            return StatusCode(StatusCodes.Status200OK, res);

        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCustomer(CustomerDTO payload)
        {          
            var res = await _customerRepository.AddCustomer(payload);
            return StatusCode(StatusCodes.Status200OK, res);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {

            var res = await _customerRepository.DeleteCustomer(id);
            return StatusCode(StatusCodes.Status200OK, res);

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCustomers()
        {

            var res = await _customerRepository.GetCustomers();
            return StatusCode(StatusCodes.Status200OK, res);

        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCustomer(CustomerDTO payload)
        {
            var res = await _customerRepository.UpdateCustomer(payload);
            return StatusCode(StatusCodes.Status200OK, res);

        }

    }
}

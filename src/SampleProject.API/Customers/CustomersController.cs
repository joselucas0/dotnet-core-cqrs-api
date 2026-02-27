using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.Customers;
using SampleProject.Application.Customers.GetCustomerDetails;
using SampleProject.Application.Customers.RegisterCustomer;

namespace SampleProject.API.Customers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Register a new customer.
        /// </summary>
        /// <param name="request">Customer registration data.</param>
        /// <returns>Newly created customer.</returns>
        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> RegisterCustomer([FromBody]RegisterCustomerRequest request)
        {
           var customer = await _mediator.Send(new RegisterCustomerCommand(request.Email, request.Name));

           return Created(string.Empty, customer);
        }

        /// <summary>
        /// Get customer details by ID.
        /// </summary>
        /// <param name="customerId">Customer ID.</param>
        /// <returns>Customer details.</returns>
        [Route("{customerId}")]
        [HttpGet]
        [ProducesResponseType(typeof(CustomerDetailsDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerDetails([FromRoute]Guid customerId)
        {
            var customerDetails = await _mediator.Send(new GetCustomerDetailsQuery(customerId));

            return Ok(customerDetails);
        }
    }
}

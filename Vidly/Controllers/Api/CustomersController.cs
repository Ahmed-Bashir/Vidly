using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Controllers.Dto;
using Vidly.Models;


namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {

        private MyDbContext _context;

        public CustomersController()
        {
            _context = new MyDbContext();
        }

        // GET /api/customers 

        public IEnumerable<CustomerDto> GetCustomers()
        {

          return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        // GET /api/customers/1

        public IHttpActionResult GetCustomers(int id)
        {

            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)

                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
            
        }

        // POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)

                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);

            customerDto.Id = customer.Id;

            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        // PUT - /api/customers 
        [HttpPut]

        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)

                throw new HttpResponseException(HttpStatusCode.BadRequest);

           

            var customerDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if(customerDb == null)

                throw new HttpResponseException(HttpStatusCode.NotFound);

             Mapper.Map(customerDto,customerDb);

            _context.SaveChanges();
        }

        [HttpDelete]
        public void RemoveCustomer(int id)
        {
            if (!ModelState.IsValid)

                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerDb == null)

                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerDb);

            _context.SaveChanges();
        }


    }

    



}

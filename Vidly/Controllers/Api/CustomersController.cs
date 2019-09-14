using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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

        public IEnumerable<Customer> GetCustomers()
        {

          return _context.Customers.ToList();
        }

        // GET /api/customers/1

        public Customer GetCustomers(int id)
        {

            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if(customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return customer;
        }

        // POST /api/customers
        [HttpPost]
        public Customer CreateCustomer( Customer customer)
        {
            if (!ModelState.IsValid)
            
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            

            _context.Customers.Add(customer);

            _context.SaveChanges();

            return customer;
        }

        // PUT - /api/customers 
        [HttpPut]

        public void UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)

                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if(customerDb == null)

                throw new HttpResponseException(HttpStatusCode.NotFound);

            customerDb.Name = customer.Name;
            customerDb.BirthDate = customer.BirthDate;
            customerDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            customerDb.MembershipTypeId = customer.MembershipTypeId;

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

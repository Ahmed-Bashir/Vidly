using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Min18YearsIfaMember: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if(customer.MembershipTypeId == MembershipType.Unkown || customer.MembershipTypeId == MembershipType.PayAsYouGo )
            {
                return ValidationResult.Success;
            }

           if(customer.BirthDate == null)
            {

                return new ValidationResult("Please enter a birthdate");
            }

            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;

            return (age >= 18) ?

                ValidationResult.Success
                :
                new ValidationResult("Customer needs to be 18 years or over to be on this membership");

            
        }
    }
}
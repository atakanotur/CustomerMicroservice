using FluentValidation;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator: AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c =>  c.Name).NotEmpty().WithMessage("Name is cannot be empty!");
            RuleFor(c => c.Email).NotEmpty().WithMessage("Email is cannot be empty!");
            RuleFor(c => c.Email).EmailAddress().WithMessage("Email must be a valid email address!");
        }
    }
}

using _3dd_Data.Models.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Data.Validation
{
    public class RegistrationValidation : AbstractValidator<RegistrationViewModel>
    {
        public RegistrationValidation()
        {
            RuleFor(r => r.Name).NotEmpty().MinimumLength(3);
            RuleFor(r => r.Surname).NotEmpty().MinimumLength(3);
            RuleFor(r => r.UserName).NotEmpty().MinimumLength(3);
            RuleFor(r => r.Email).NotEmpty().MinimumLength(3).EmailAddress();
            RuleFor(r => r.Password).NotEmpty().MinimumLength(3);
        }
    }
}

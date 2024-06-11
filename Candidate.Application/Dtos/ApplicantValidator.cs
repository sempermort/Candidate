using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Candidate.Application.Dtos
{
    public class ApplicantValidator : AbstractValidator<ApplicantDto>
    {
        public ApplicantValidator()
        {

            RuleFor(n => n.FirstName)
            .NotEmpty().WithMessage("First Name is Required")
            .MaximumLength(50).WithMessage("First name must not Exceed 20 characters");

            RuleFor(n => n.LastName)
            .NotEmpty().WithMessage("Last Name is Required")
            .MaximumLength(50).WithMessage("Last name must not Exceed 20 characters");

            RuleFor(n => n.Email)
            .NotEmpty().WithMessage("Email is Required");

            RuleFor(p => p.PhoneNumber)
            .MinimumLength(7).WithMessage("PhoneNumber can not be less than 7 characters.")
            .MaximumLength(20).WithMessage("PhoneNumber can not exceed 20 characters.");

            RuleFor(n => n.Comment)
            .NotEmpty().WithMessage("Comment  is Required");
        }
    }
}

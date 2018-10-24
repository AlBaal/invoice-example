using FluentValidation;
using Invoice.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.WebApp.Validators
{
    public class InvoiceCreateValidator : AbstractValidator<InvoicesViewModel>
    {
        public InvoiceCreateValidator()
        {
            RuleFor(vm => vm.Description)
                .NotEmpty()
                .WithMessage(ValidationMessages.Required);

            RuleFor(vm => vm.BankAccount)
                .NotEmpty()
                .WithMessage(ValidationMessages.Required);

            RuleFor(vm => vm.DueDate)
                .NotEmpty()
                .WithMessage(ValidationMessages.Required)
                .Must(ValidationConditions.IsValidType)
                .WithMessage(ValidationMessages.InvalidFormat);
        }
    }
}

using FluentValidation;
using Invoice.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.WebApp.Validators
{
    public class InvoiceItemCreateValidator : AbstractValidator<InvoiceItemsViewModel>
    {
        public InvoiceItemCreateValidator()
        {
            RuleFor(vm => vm.Name)
                .NotEmpty()
                .WithMessage(ValidationMessages.Required);

            RuleFor(vm => vm.Quantity)
                .NotEmpty()
                .WithMessage(ValidationMessages.Required)
                .Must(ValidationConditions.IsValidType)
                .WithMessage(ValidationMessages.InvalidFormat)
                .GreaterThan(0)
                .WithMessage(ValidationMessages.NumberGreaterThen);

            RuleFor(vm => vm.Amount)
                .NotEmpty()
                .WithMessage(ValidationMessages.Required)
                .Must(ValidationConditions.IsValidType)
                .WithMessage(ValidationMessages.InvalidFormat)
                .GreaterThan(0)
                .WithMessage(ValidationMessages.NumberGreaterThen);
        }
    }
}

﻿using FluentValidation;
using Nop.Core.Domain.Customers;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.Customer;

namespace Nop.Web.Validators.Customer
{
    public partial class LoginValidator : BaseNopValidator<LoginModel>
    {
        public LoginValidator(ILocalizationService localizationService, CustomerSettings customerSettings)
        {
            if (!customerSettings.UsernamesEnabled)
            {
                //login by email
                RuleFor(x => x.Email).NotEmpty().WithMessage(localizationService.GetResourceAsync("Account.Login.Fields.Email.Required").Result);
                RuleFor(x => x.Email).EmailAddress().WithMessage(localizationService.GetResourceAsync("Common.WrongEmail").Result);
            }
        }
    }
}
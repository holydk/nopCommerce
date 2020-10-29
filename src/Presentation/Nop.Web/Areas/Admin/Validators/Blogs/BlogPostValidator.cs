﻿using FluentValidation;
using Nop.Core.Domain.Blogs;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Services.Seo;
using Nop.Web.Areas.Admin.Models.Blogs;
using Nop.Web.Framework.Validators;

namespace Nop.Web.Areas.Admin.Validators.Blogs
{
    public partial class BlogPostValidator : BaseNopValidator<BlogPostModel>
    {
        public BlogPostValidator(ILocalizationService localizationService, INopDataProvider dataProvider)
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage(localizationService.GetResourceAsync("Admin.ContentManagement.Blog.BlogPosts.Fields.Title.Required").Result);

            RuleFor(x => x.Body)
                .NotEmpty()
                .WithMessage(localizationService.GetResourceAsync("Admin.ContentManagement.Blog.BlogPosts.Fields.Body.Required").Result);

            //blog tags should not contain dots
            //current implementation does not support it because it can be handled as file extension
            RuleFor(x => x.Tags)
                .Must(x => x == null || !x.Contains("."))
                .WithMessage(localizationService.GetResourceAsync("Admin.ContentManagement.Blog.BlogPosts.Fields.Tags.NoDots").Result);

            RuleFor(x => x.SeName).Length(0, NopSeoDefaults.SearchEngineNameLength)
                .WithMessage(string.Format(localizationService.GetResourceAsync("Admin.SEO.SeName.MaxLengthValidation").Result, NopSeoDefaults.SearchEngineNameLength));

            SetDatabaseValidationRules<BlogPost>(dataProvider);
        }
    }
}
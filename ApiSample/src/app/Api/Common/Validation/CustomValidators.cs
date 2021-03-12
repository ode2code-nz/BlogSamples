using FluentValidation;
using ApiSample.Domain.Model.ToDos;

namespace ApiSample.Api.Common.Validation
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, string> MustBeValidEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(x => Email.Create(x).IsSuccess == true)
                .WithMessage("'{PropertyName}' should be valid email"); 
        }
    }
}
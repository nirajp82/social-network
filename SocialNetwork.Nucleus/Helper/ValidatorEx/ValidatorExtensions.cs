using FluentValidation;

namespace SocialNetwork.Nucleus
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotEmpty()
                .MinimumLength(6).WithMessage("Password must be between least 6 - 10 characters")
                .MaximumLength(10).WithMessage("Password must be between least 6 - 10 characters")
                .Matches("[A-Z]").WithMessage("Password must contain 1 uppercase letter")
                .Matches("[a-z]").WithMessage("Password must have at least 1 lowercase character")
                .Matches("[0-9]").WithMessage("Password must contain a number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain non alphanumeric");
        }
    }
}

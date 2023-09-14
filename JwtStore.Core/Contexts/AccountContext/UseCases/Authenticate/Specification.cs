using Flunt.Notifications;
using Flunt.Validations;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request) => new Contract<Notification>()
        .Requires()
        .IsLowerThan(request.Password.Length, 128, "The maximum length of a Password is 128 characters")
        .IsGreaterOrEqualsThan(request.Password.Length, 8, "The minimum length of a Password is 8 characters")
        .IsEmail(request.Email, "Email", "Invalid e-mail!");
}

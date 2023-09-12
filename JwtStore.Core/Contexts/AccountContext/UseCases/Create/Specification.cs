using Flunt.Notifications;
using Flunt.Validations;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request) => new Contract<Notification>()
        .Requires()
        .IsLowerThan(request.Name.Length, 120, "Name", "The maximum length of a Name is 120 characters")
        .IsGreaterOrEqualsThan(request.Name.Length, 3, "Name", "The minimum length of a Name is 3 characters")
        .IsLowerThan(request.Password.Length, 128, "The maximum length of a Password is 128 characters")
        .IsGreaterOrEqualsThan(request.Password.Length, 8, "The minimum length of a Password is 8 characters")
        .IsEmail(request.Email, "Email", "Invalid e-mail!");
}

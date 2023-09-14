using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository) => _repository = repository;

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region Request Validation
        
        try
        {
            var response = Specification.Ensure(request);
            if (!response.IsValid)
                return new Response("Invalid Request", 400, response.Notifications);
        }
        catch
        {
            return new Response("Unable to validate your request", 500);
        }

        #endregion

        #region Get Profile
        User? user;

        try
        {
            user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (user is null)
                return new Response("Profile not found!", 404);
        }
        catch
        {
            return new Response("Failed to retrieve profile", 500);
        }
        #endregion

        #region Check if password is valid
        
        if (!user.Password.VerifyHash(request.Password))
            return new Response("Username or Password Invalid", 400);

        #endregion

        #region Check if account is active
        try
        {
            if (!user.Email.Verification.IsActive)
                return new Response("Inactive Account", 400);
        }
        catch
        {
            return new Response("Failed to check profile", 500);
        }
        #endregion

        #region Return data
        try
        {
            var data = new ResponseData
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                Roles = Array.Empty<string>()
            };

            return new Response(string.Empty, data);
        }
        catch
        {
            return new Response("Failed to check profile", 500);
        }
        #endregion
    }
}
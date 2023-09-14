using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtStore.Core.Contexts.AccountContext.ValueObjects;
using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    private readonly IService _service;

    public Handler(IRepository repository, IService service)
    {
        _repository = repository;
        _service = service;
    }

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

        #region Create Object

        Email email;
        Password password;
        User user;

        try
        {
            email = new(request.Email);
            password = new(request.Password);
            user = new(request.Name, email, password);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        #endregion

        #region Verify User

        try
        {
            var exists = await _repository.AnyAsync(request.Email, cancellationToken);
            if (exists)
                return new Response("E-mail already exists", 400);
        }
        catch
        {
            return new Response("Failed to verify registered e-mail", 500);
        }

        #endregion

        #region Data persistence

        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Failed in data persistence", 500);
        }

        #endregion

        //#region Send activation e-mail

        //try
        //{
        //    await _service.SendVerificationEmailAsync(user, cancellationToken);
        //}
        //catch
        //{
        //    // Already persisted data. Do nothing.
        //}

        //#endregion

        return new Response("Account created successfully", new ResponseData(user.Id, user.Name, user.Email));
    }
}

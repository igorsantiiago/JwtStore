using JwtStore.Core;
using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace JwtStore.Infra.Contexts.AccountContext.UseCases.Create;

public class Service : IService
{
    //public async Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken)
    //{
    //    var client = new SendGridClient(Configuration.SendGrid.ApiKey);
    //    var from = new EmailAddress(Configuration.Email.DefaultFromEmail, Configuration.Email.DefaultFromName);
    //    const string subject = "Verify your account";
    //    var to = new EmailAddress(user.Email, user.Name);
    //    var content = $"Code: {user.Email.Verification.Code}";
    //    var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
    //    await client.SendEmailAsync(msg, cancellationToken);
    //}
}

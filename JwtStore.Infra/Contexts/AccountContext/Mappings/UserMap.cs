using JwtStore.Core.Contexts.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JwtStore.Infra.Contexts.AccountContext.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(user => user.Name).HasColumnName("Name").HasColumnType("NVARCHAR").HasMaxLength(120).IsRequired(true);
        builder.Property(user => user.Image).HasColumnName("Image").HasColumnType("VARCHAR").HasMaxLength(255).IsRequired(true);

        builder.OwnsOne(user => user.Email).Property(email => email.Address).HasColumnName("Email").IsRequired(true);

        builder.OwnsOne(user => user.Email).OwnsOne(email => email.Verification).Property(verification => verification.Code).HasColumnName("EmailVerificationCode").IsRequired(true);
        builder.OwnsOne(user => user.Email).OwnsOne(email => email.Verification).Property(verification => verification.ExpiresAt).HasColumnName("EmailVerificationExpiresAt").IsRequired(false);
        builder.OwnsOne(user => user.Email).OwnsOne(email => email.Verification).Property(verification => verification.VerifiedAt).HasColumnName("EmailVerificationVerifiedAt").IsRequired(false);
        builder.OwnsOne(user => user.Email).OwnsOne(email => email.Verification).Ignore(verification => verification.IsActive);

        builder.OwnsOne(user => user.Password).Property(password => password.Hash).HasColumnName("PasswordHash").IsRequired(true);
        builder.OwnsOne(user => user.Password).Property(password => password.ResetCode).HasColumnName("PasswordResetCode").IsRequired(true);

    }
}

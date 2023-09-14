using JwtStore.Core.Contexts.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JwtStore.Infra.Contexts.AccountContext.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Name).HasColumnName("Name").HasColumnType("NVARCHAR").HasMaxLength(120).IsRequired(true);
        builder.Property(user => user.Image).HasColumnName("Image").HasColumnType("VARCHAR").HasMaxLength(255).IsRequired(true);

        builder.OwnsOne(user => user.Email).Property(email => email.Address).HasColumnName("Email").HasColumnType("NVARCHAR").HasMaxLength(320).IsRequired(true);

        builder.OwnsOne(user => user.Email).OwnsOne(email => email.Verification).Property(verification => verification.Code).HasColumnName("EmailVerificationCode").HasMaxLength(10).IsRequired(true);
        builder.OwnsOne(user => user.Email).OwnsOne(email => email.Verification).Property(verification => verification.ExpiresAt).HasColumnName("EmailVerificationExpiresAt").IsRequired(false);
        builder.OwnsOne(user => user.Email).OwnsOne(email => email.Verification).Property(verification => verification.VerifiedAt).HasColumnName("EmailVerificationVerifiedAt").IsRequired(false);
        builder.OwnsOne(user => user.Email).OwnsOne(email => email.Verification).Ignore(verification => verification.IsActive);

        builder.OwnsOne(user => user.Password).Property(password => password.Hash).HasColumnName("PasswordHash").HasMaxLength(255).IsRequired(true);
        builder.OwnsOne(user => user.Password).Property(password => password.ResetCode).HasColumnName("PasswordResetCode").HasMaxLength(10).IsRequired(true);

        builder.HasMany(user => user.Roles).WithMany(role => role.Users).UsingEntity<Dictionary<string, object>>("UserRole",
            role => role.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.Cascade),
            user => user.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade));
    }
}

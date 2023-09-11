using JwtStore.Core.Contexts.SharedContext.Entities;

namespace JwtStore.Core.Contexts.AccountContext.ValueObjects;

public class Verification : Entity
{
    public Verification() { }

    public string Code { get; } = Guid.NewGuid().ToString("N")[..8].ToUpper();
    public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);
    public DateTime? VerifiedAt { get; private set; } = null;
    public bool IsActive => VerifiedAt != null && ExpiresAt == null;

    public void Verify(string code)
    {
        if (IsActive)
            throw new Exception("Este item já foi ativado!");

        if (ExpiresAt <= DateTime.UtcNow)
            throw new Exception("Este código esta expirado!");

        if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new Exception("Código de verificação inválido!");

        ExpiresAt = null;
        VerifiedAt = DateTime.UtcNow;
    }
}

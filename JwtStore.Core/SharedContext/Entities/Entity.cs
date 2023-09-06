namespace JwtStore.Core.SharedContext.Entities;

public abstract class Entity : IEquatable<Guid>
{
    protected Entity() => Id = Guid.NewGuid();

    public Guid Id { get; }

    public bool Equals(Guid otherId) => Id == otherId;

    public override int GetHashCode() => Id.GetHashCode();
}

namespace NitajTodoApp.Domain.Primitives;

public abstract class Entity : ValueObject, IEquatable<Entity>
{
    protected Entity(int id) => Id = id;
    protected Entity() { }

    public int Id { get; private init; }

    public bool Equals(Entity? other)
    {
        if (other is null || other.GetType() != GetType())
        {
            return false;
        }

        return other.Id == Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType() || obj is not Entity entity)
        {
            return false;
        }

        return entity.Id == Id;
    }

    public override int GetHashCode() => Id.GetHashCode() * 41;
}

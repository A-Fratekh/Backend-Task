namespace EmployeeProfile.Domain;

public abstract class Entity
{
    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
            return false;

        return ReferenceEquals(this, obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

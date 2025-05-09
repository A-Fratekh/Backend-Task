namespace EmployeeProfile.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Entity other || GetType() != other.GetType())
                return false;

            return Id == other.Id;
        }
        public override int GetHashCode() => Id.GetHashCode();
    }
}

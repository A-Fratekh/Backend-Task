namespace EmployeeProfile.Domain.Aggregates.OccupationAggregate;
public class Grade
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid OccupationId { get; private set; }
    public Occupation Occupation { get; private set; }

    private Grade() { } 

    public Grade(string name, Guid occupationId)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        OccupationId = occupationId;
    }

    public void Update(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}
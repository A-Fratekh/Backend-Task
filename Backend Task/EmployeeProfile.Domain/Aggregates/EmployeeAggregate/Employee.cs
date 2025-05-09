﻿using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;

namespace EmployeeProfile.Domain.Aggregates.EmployeeAggregate;

public class Employee : Entity, IAggregateRoot
{
    public string EmployeeNo { get; private set; }
    public string Name { get; private set; }
    public DateTime HireDate { get; private set; }
    public Guid DepartmentId { get; private set; }
    public Department Department { get; private set; }
    public Guid OccupationId { get; private set; }
    public Occupation Occupation { get; private set; }
    public Guid GradeId { get; private set; }
    public Grade Grade { get; private set; }

    private Employee() { }

    public Employee(string employeeNo, string name, DateTime hireDate,
        Guid departmentId, Guid occupationId, Guid gradeId)
    {
        EmployeeNo = employeeNo ?? throw new ArgumentNullException(nameof(employeeNo));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        HireDate = hireDate;
        DepartmentId = departmentId;
        OccupationId = occupationId;
        GradeId = gradeId;
    }

    public void Update(string name, DateTime hireDate,
        Guid departmentId, Guid occupationId, Guid gradeId)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        HireDate = hireDate;
        DepartmentId = departmentId;
        OccupationId = occupationId;
        GradeId = gradeId;
    }
}

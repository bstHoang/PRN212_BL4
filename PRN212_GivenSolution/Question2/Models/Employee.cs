using System;
using System.Collections.Generic;

namespace Question2.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FullName { get; set; } = null!;

    public int PositionId { get; set; }

    public int DepartmentId { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public DateOnly DateOfHire { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();

    public virtual ICollection<EmployeeTraining> EmployeeTrainings { get; set; } = new List<EmployeeTraining>();

    public virtual Position Position { get; set; } = null!;
}

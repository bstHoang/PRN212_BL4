using System;
using System.Collections.Generic;

namespace Question2.Models;

public partial class EmployeeTraining
{
    public int EmployeeId { get; set; }

    public int TrainingId { get; set; }

    public string? CompletionStatus { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual TrainingProgram Training { get; set; } = null!;
}

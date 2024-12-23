using System;
using System.Collections.Generic;

namespace Question2.Models;

public partial class TrainingProgram
{
    public int TrainingId { get; set; }

    public string TrainingName { get; set; } = null!;

    public int Duration { get; set; }

    public string TrainerName { get; set; } = null!;

    public virtual ICollection<EmployeeTraining> EmployeeTrainings { get; set; } = new List<EmployeeTraining>();
}

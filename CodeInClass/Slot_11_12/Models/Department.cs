using System;
using System.Collections.Generic;

namespace Slot_11_12.Models;

public partial class Department
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}

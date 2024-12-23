using System;
using System.Collections.Generic;

namespace WpfApp2.Models;

public partial class Department
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}

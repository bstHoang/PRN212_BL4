using System;
using System.Collections.Generic;

namespace Project_PRN212_FALL24.Models;

public partial class SettingName
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Setting> Settings { get; set; } = new List<Setting>();
}

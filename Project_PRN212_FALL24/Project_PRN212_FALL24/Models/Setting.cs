using System;
using System.Collections.Generic;

namespace Project_PRN212_FALL24.Models;

public partial class Setting
{
    public int Id { get; set; }

    public int Type { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual SettingName TypeNavigation { get; set; } = null!;
}

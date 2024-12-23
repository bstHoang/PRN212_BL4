using System;
using System.Collections.Generic;

namespace Project_PRN212_FALL24.Models;

public partial class Account
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string HiddenPassword => new string('*', Password?.Length ?? 0);
    public int Role { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual Setting RoleNavigation { get; set; } = null!;
}

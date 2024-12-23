using System;
using System.Collections.Generic;

namespace Project_PRN212_FALL24.Models;

public partial class History
{
    public int Id { get; set; }

    public int Idaccount { get; set; }

    public int Idbook { get; set; }

    public DateOnly BookLoanDate { get; set; }

    public DateOnly? BookReturnDate { get; set; }

    public DateOnly Deadline { get; set; }

    public virtual Account IdaccountNavigation { get; set; } = null!;

    public virtual Book IdbookNavigation { get; set; } = null!;
}

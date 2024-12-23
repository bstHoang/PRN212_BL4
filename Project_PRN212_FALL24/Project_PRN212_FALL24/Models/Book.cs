using System;
using System.Collections.Generic;

namespace Project_PRN212_FALL24.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Type { get; set; }

    public string Image { get; set; } = null!;

    public int CreateBy { get; set; }

    public DateOnly DateCreate { get; set; }

    public DateOnly? DateModify { get; set; }

    public int Quantity { get; set; }

    public string? Description { get; set; }

    public string? Author { get; set; } = null;
    public virtual Account CreateByNavigation { get; set; } = null!;

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual Setting TypeNavigation { get; set; } = null!;
}

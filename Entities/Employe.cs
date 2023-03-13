using System;
using System.Collections.Generic;

namespace DIgitalMenu.Entities;

public partial class Employe
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public int Age { get; set; }

    public string Email { get; set; } = null!;

    public bool? Isactive { get; set; }

    public virtual ICollection<Office> Offices { get; } = new List<Office>();
}

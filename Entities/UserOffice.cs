using System;
using System.Collections.Generic;

namespace DIgitalMenu.Entities;

public partial class UserOffice
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? OfficeId { get; set; }

    public bool? IsOwner { get; set; }

    public bool? IsStaf { get; set; }

    public bool? IsActive { get; set; }

    public virtual Office? Office { get; set; }
}

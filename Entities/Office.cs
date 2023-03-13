using System;
using System.Collections.Generic;

namespace DIgitalMenu.Entities;

public partial class Office
{
    public int Id { get; set; }

    public string OfficeName { get; set; } = null!;

    public string? OfficeAddress { get; set; }

    public string? OfficeCountry { get; set; }

    public string? OfficePinCode { get; set; }

    public int? CreatedBy { get; set; }

    public virtual Employe? CreatedByNavigation { get; set; }

    public virtual ICollection<UserOffice> UserOffices { get; } = new List<UserOffice>();
}

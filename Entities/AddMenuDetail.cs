using System;
using System.Collections.Generic;

namespace DIgitalMenu.Entities;

public partial class AddMenuDetail
{
    public int Id { get; set; }

    public string? DishName { get; set; }

    public string? Category { get; set; }

    public string? Type { get; set; }

    public string? Image { get; set; }

    public int? Price { get; set; }

    public string? Quantity { get; set; }
}

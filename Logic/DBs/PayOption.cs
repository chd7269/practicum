using System;
using System.Collections.Generic;

namespace Logic;

public partial class PayOption
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public int UserId { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Moving> Movings { get; set; } = new List<Moving>();

    public virtual User User { get; set; } = null!;
}

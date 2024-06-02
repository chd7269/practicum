using System;
using System.Collections.Generic;

namespace Logic;

public partial class Status
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}

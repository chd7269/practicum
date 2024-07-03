using System;
using System.Collections.Generic;

namespace Logic;

public partial class History
{
    public int HistoryId { get; set; }

    public int? Id { get; set; }

    public DateTime? DateofChange { get; set; }

    public string? OldDomain { get; set; }

    public string? NewDomain { get; set; }

    public int OldAmount { get; set; }

    public int NewAmount { get; set; }

    public virtual User? IdNavigation { get; set; }
}

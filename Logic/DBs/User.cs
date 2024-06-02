using System;
using System.Collections.Generic;

namespace Logic;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int UserTypeId { get; set; }

    public string? Phone { get; set; }

    public bool IsActive { get; set; }

    public int? LenderId { get; set; }

    public DateTime RegisterDate { get; set; }

    public bool IsYearlyPay { get; set; }

    public DateTime? PayDate { get; set; }

    public string LastName { get; set; } = null!;

    public int? ManagerId { get; set; }

    public bool? AreaIndexOn { get; set; }

    public virtual ICollection<Debt> Debts { get; set; } = new List<Debt>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<User> InverseLender { get; set; } = new List<User>();

    public virtual ICollection<User> InverseManager { get; set; } = new List<User>();

    public virtual User? Lender { get; set; }

    public virtual User? Manager { get; set; }

    public virtual ICollection<PayOption> PayOptions { get; set; } = new List<PayOption>();

    public virtual ICollection<Presence> Presences { get; set; } = new List<Presence>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<User2Area> User2Areas { get; set; } = new List<User2Area>();

    public virtual UserType UserType { get; set; } = null!;
}

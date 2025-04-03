using System;
using System.Collections.Generic;

namespace Loyal.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? PanCard { get; set; }

    public long AdhaarCard { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Status { get; set; }

    public string? Reason { get; set; }
}

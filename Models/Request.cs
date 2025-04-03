using System;
using System.Collections.Generic;

namespace Loyal.Models;

public partial class Request
{
    public string? Username { get; set; }

    public string? Offername { get; set; }

    public string? Offerdescription { get; set; }

    public string? Status { get; set; }

    public int UserId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Reason { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Code { get; set; }
}

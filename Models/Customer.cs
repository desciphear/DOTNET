using System;
using System.Collections.Generic;

namespace Loyal.Models;

public partial class Customer
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }
}

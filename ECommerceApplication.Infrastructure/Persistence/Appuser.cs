using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ECommerceApplication.Infrastructure.Persistence;

public partial class Appuser
{
    public string Userid { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Emailaddress { get; set; } = null!;

    [JsonIgnore]
    public string Passwordhash { get; set; } = null!;

    public string? Role { get; set; }

    public string Address { get; set; } = null!;
}

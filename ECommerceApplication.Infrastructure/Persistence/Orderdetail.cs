using System;
using System.Collections.Generic;

namespace ECommerceApplication.Infrastructure.Persistence;

public partial class Orderdetail
{
    public int Id { get; set; }

    public string Userid { get; set; } = null!;

    public string Paymentstatus { get; set; } = null!;

    public DateTime Ordereddate { get; set; }

    public string Shippingaddress { get; set; } = null!;

    public bool Isactive { get; set; }

    public DateTime? Expecteddelivery { get; set; }
}

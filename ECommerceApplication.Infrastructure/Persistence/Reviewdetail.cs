using System;
using System.Collections.Generic;

namespace ECommerceApplication.Infrastructure.Persistence;

public partial class Reviewdetail
{
    public int Id { get; set; }

    public int Productid { get; set; }

    public string Userid { get; set; } = null!;

    public double Ratings { get; set; }

    public DateTime Postedat { get; set; }

    public string? Comment { get; set; }

    public bool Isactive { get; set; }

    public virtual Productdetail Product { get; set; } = null!;
}

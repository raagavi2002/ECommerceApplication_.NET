using System;
using System.Collections.Generic;

namespace ECommerceApplication.Infrastructure.Persistence;

public partial class Productdetail
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public double Price { get; set; }

    public int Quantityavailable { get; set; }

    public int Categoryid { get; set; }

    public double? Averagerating { get; set; }

    public bool Isactive { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Reviewdetail> Reviewdetails { get; set; } = new List<Reviewdetail>();
}

using System;
using System.Collections.Generic;

namespace ECommerceApplication.Infrastructure.Persistence;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Isactive { get; set; }

    public virtual ICollection<Productdetail> Productdetails { get; set; } = new List<Productdetail>();
}

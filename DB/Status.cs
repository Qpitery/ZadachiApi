using System;
using System.Collections.Generic;

namespace ZadachiApi.DB;

public partial class Status
{
    public int Idstatus { get; set; }

    public string? NameStatus { get; set; }

    public virtual ICollection<Zadachi> Zadachis { get; } = new List<Zadachi>();
}

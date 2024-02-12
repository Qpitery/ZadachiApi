using System;
using System.Collections.Generic;

namespace ZadachiApi.DB;

public partial class Zadachi
{
    public int Idzadachi { get; set; }

    public string? NameZadachi { get; set; }

    public string? OpisanieZadachi { get; set; }

    public int Idstatus { get; set; }

    public virtual Status IdstatusNavigation { get; set; } = null!;
}

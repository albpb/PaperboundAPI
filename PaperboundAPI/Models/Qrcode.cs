using System;
using System.Collections.Generic;

namespace PaperboundAPI.Models;

public partial class Qrcode
{
    public string IdQr { get; set; } = null!;

    public int? IdComanda { get; set; }

    public virtual Comanda? IdComandaNavigation { get; set; }
}

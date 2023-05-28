using System;
using System.Collections.Generic;

namespace PaperboundAPI.Models;

public partial class Qr
{
    public int IdQr { get; set; }

    public string? Code { get; set; }

    public int? IdLlibre { get; set; }

    public int? IdPuntRecollida { get; set; }

    public virtual Llibre? IdLlibreNavigation { get; set; }

    public virtual PuntRecollida? IdPuntRecollidaNavigation { get; set; }
}

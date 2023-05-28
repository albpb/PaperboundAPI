using System;
using System.Collections.Generic;

namespace PaperboundAPI.Models;

public partial class PuntRecollida
{
    public int IdPuntRecollida { get; set; }

    public string? Latitud { get; set; }

    public string? Longitud { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Comanda> Comandes { get; } = new List<Comanda>();

    public virtual ICollection<Qr> Qrs { get; } = new List<Qr>();
}

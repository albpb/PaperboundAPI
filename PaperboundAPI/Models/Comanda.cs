using System;
using System.Collections.Generic;

namespace PaperboundAPI.Models;

public partial class Comanda
{
    public int Idcomanda { get; set; }

    public int? Userid { get; set; }

    public int? IdLlibre { get; set; }

    public int? Idrecollida { get; set; }

    public virtual PuntRecollida? IdrecollidaNavigation { get; set; }

    public virtual Qrcode? Qrcode { get; set; }

    public virtual Usuari? User { get; set; }

    public virtual ICollection<Llibre> IdLlibres { get; } = new List<Llibre>();
}

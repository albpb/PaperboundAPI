using System;
using System.Collections.Generic;

namespace PaperboundAPI.Models;

public partial class Usuari
{
    public int IdUser { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Salt { get; set; }

    public virtual ICollection<Comanda> Comandes { get; } = new List<Comanda>();
}

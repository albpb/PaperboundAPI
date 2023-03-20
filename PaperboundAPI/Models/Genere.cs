using System;
using System.Collections.Generic;

namespace PaperboundAPI.Models;

public partial class Genere
{
    public int IdGenere { get; set; }

    public string? NomGenere { get; set; }

    public string? imgGenere { get; set; }

    public virtual ICollection<Llibre> Llibres { get; } = new List<Llibre>();
}

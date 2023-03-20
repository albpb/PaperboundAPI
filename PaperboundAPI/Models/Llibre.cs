using System;
using System.Collections.Generic;

namespace PaperboundAPI.Models;

public partial class Llibre
{
    public int IdLlibre { get; set; }

    public string? Titol { get; set; }

    public int? IdGenere { get; set; }

    public string? NomAutor { get; set; }

    public string? Sinopsi { get; set; }

    public int? Descompte { get; set; }

    public string? UrlImatge { get; set; }

    public decimal? PreuTotal { get; set; }

    public virtual Genere? IdGenereNavigation { get; set; }

    public virtual ICollection<Comanda> IdComandes { get; } = new List<Comanda>();
}

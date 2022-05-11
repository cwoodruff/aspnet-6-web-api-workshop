﻿namespace Chinook.Domain.Entities;

public partial class Track
{
    public Track()
    {
        InvoiceLines = new HashSet<InvoiceLine>();
        Playlists = new HashSet<Playlist>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? AlbumId { get; set; }
    public int MediaTypeId { get; set; }
    public int? GenreId { get; set; }
    public string? Composer { get; set; }
    public int Milliseconds { get; set; }
    public int? Bytes { get; set; }
    public decimal UnitPrice { get; set; }

    public virtual Album? Album { get; set; }
    public virtual Genre? Genre { get; set; }
    public virtual MediaType MediaType { get; set; } = null!;
    public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }

    public virtual ICollection<Playlist> Playlists { get; set; }
}
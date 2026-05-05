using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuoteApi.Models;
[Table("quotes")]
public class Quote
{
    [Column("id")]
    public int Id { get; set; }
    [Column("quote")]
    [MaxLength(2500)]
    public string? Text { get; set; }
    [Column("author")]
    [MaxLength(500)]
    public string? Author { get; set; }
}
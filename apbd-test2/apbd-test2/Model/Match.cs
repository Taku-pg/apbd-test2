using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apbd_test2.Model;

[Table("Match")]
public class Match
{
    [Key]
    public int MatchId { get; set; }
    [ForeignKey(nameof(Tournament))]
    public int TournamentId { get; set; }
    [ForeignKey(nameof(Map))]
    public int MapId { get; set; }
    public DateTime MatchDate { get; set; }
    public int Team1Score { get; set; }
    public int Team2Score { get; set; }
    [Column(TypeName = "numeric")]
    [Precision(4,2)]
    public double BestRating { get; set; }
    public virtual Tournament Tournament { get; set; }
    public virtual Map Map { get; set; }
    
    public ICollection<PlayerMatch> PlayerMatches { get; set; }
}
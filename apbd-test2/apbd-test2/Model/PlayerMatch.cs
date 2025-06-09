using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apbd_test2.Model;

[Table("Player_Match")]
[PrimaryKey(nameof(MatchId),nameof(PlayerId))]
public class PlayerMatch
{
    [ForeignKey(nameof(Match))]
    public int MatchId { get; set; }
    [ForeignKey(nameof(Player))]
    public int PlayerId { get; set; }
    public int MVPs { get; set; }
    [Column(TypeName = "numeric")]
    [Precision(4,2)]
    public double Rating { get; set; }
    
    public virtual Match Match { get; set; }

    public virtual Player Player { get; set; }
}
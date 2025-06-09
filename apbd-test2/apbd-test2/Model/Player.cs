using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd_test2.Model;

[Table("Player")]
public class Player
{
    [Key]
    public int PlayerId { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public ICollection<PlayerMatch> PlayerMatches { get; set; }
}
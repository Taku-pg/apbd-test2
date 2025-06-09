using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace apbd_test2.DTO;

public class NewPlayerDTO
{
    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public List<NewMatchDTO> Matches { get; set; }
}

public class NewMatchDTO
{
    public int MatchId { get; set; }
    public int MVPs { get; set; }
    [Precision(4,2)]
    public double Rating { get; set; }
}
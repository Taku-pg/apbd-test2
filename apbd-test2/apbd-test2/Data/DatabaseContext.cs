using System.Runtime.InteropServices.JavaScript;
using apbd_test2.Model;
using Microsoft.EntityFrameworkCore;

namespace apbd_test2.Data;

public class DatabaseContext:DbContext
{
    public DbSet<Map> Maps { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerMatch> PlayerMatches { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Map>().HasData(new List<Map>
        {
            new Map{MapId = 1,Name="AMap", Type = "Grass"},
            new Map{MapId = 2,Name="BMap", Type = "Sand"}, 
            new Map{MapId = 3,Name="CMap", Type = "Concrete"},
        });
        
        modelBuilder.Entity<Tournament>().HasData(new List<Tournament>
        {
            new Tournament{TournamentId = 1,Name="ATournament", StartDate = new DateTime(2025,6,1),EndDate = new DateTime(2025,6,18)},
            new Tournament{TournamentId = 2,Name="BTournament", StartDate = new DateTime(2025,5,2),EndDate = new DateTime(2025,5,30)},
            new Tournament{TournamentId = 3,Name="CTournament", StartDate = new DateTime(2025,7,4),EndDate = new DateTime(2025,7,28)},
        });

        modelBuilder.Entity<Player>().HasData(new List<Player>
        {
            new Player{PlayerId = 1,FirstName="John", LastName="Doe", BirthDate=new DateTime(1997,4,1)},
            new Player{PlayerId = 2,FirstName="Jane", LastName="Doo", BirthDate=new DateTime(2000,12,4)},
            new Player{PlayerId = 3,FirstName="Jammy", LastName="Smith", BirthDate=new DateTime(1998,5,7)}
        });

        modelBuilder.Entity<Match>().HasData(new List<Match>
        {
            new Match{MatchId = 1,TournamentId = 1,MapId = 1,Team1Score = 2,Team2Score = 3,BestRating = 2.4},
            new Match{MatchId = 2,TournamentId = 1,MapId = 2,Team1Score = 1,Team2Score = 2,BestRating = 1.6},
            new Match{MatchId = 3,TournamentId = 3,MapId = 3,Team1Score = 5,Team2Score = 4,BestRating = 1.2},

        });           
        modelBuilder.Entity<PlayerMatch>().HasData(new List<PlayerMatch>
        {
            new PlayerMatch{MatchId = 1,PlayerId = 1,MVPs = 1,Rating = 1.2},
            new PlayerMatch{MatchId = 2,PlayerId = 2,MVPs = 2,Rating = 1.1},
            new PlayerMatch{MatchId = 3,PlayerId = 3,MVPs = 1,Rating = 1.8},
        });
    }
}
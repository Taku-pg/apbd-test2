using System.ComponentModel.DataAnnotations;
using apbd_test2.CustomException;
using apbd_test2.Data;
using apbd_test2.Model;
using apbd_test2.DTO;
using Microsoft.EntityFrameworkCore;

namespace apbd_test2.Service;

public class PlayerService : IPlayerService
{
    private readonly DatabaseContext _context;

    public PlayerService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<PlayerMatchDTO> GetPlayerById(int id)
    {
        var player = await _context.Players
            .Where(p => p.PlayerId == id)
            .Select(p => new PlayerMatchDTO
            {
                PlayerId = p.PlayerId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                BirthDate = p.BirthDate,
                Matches = p.PlayerMatches.Select(pm => new MatchDTO
                {
                    Tournament = pm.Match.Tournament.Name,
                    Map= pm.Match.Map.Name,
                    Date = pm.Match.MatchDate,
                    MVPs = pm.MVPs,
                    Rating = pm.Rating,
                    Team1Score = pm.Match.Team1Score,
                    Team2Score = pm.Match.Team2Score,
                }).ToList()
            }).FirstOrDefaultAsync();
        if (player == null)
            throw new NotFoundException("Player not found");
        return player;
    }

    public async Task AddPlayer(NewPlayerDTO newPlayer)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            var matches = newPlayer.Matches;
            foreach (var match in matches)
            {
                var hasExist=await _context.Matches.FirstOrDefaultAsync(m => m.MatchId == match.MatchId);
                if (hasExist == null)
                {
                    throw new NotFoundException($"Match not found with id {match.MatchId}");
                }else
                {
                    if (hasExist.BestRating < match.Rating)
                    {
                        hasExist.BestRating = match.Rating;
                    }   
                }
                
            }
            
            var player = new Player
            {
                FirstName = newPlayer.FirstName,
                LastName = newPlayer.LastName,
                BirthDate = newPlayer.BirthDate,
            };
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();

            foreach (var match in matches)
            {
                var pm = new PlayerMatch
                { 
                    MatchId = match.MatchId, 
                    PlayerId = player.PlayerId, 
                    MVPs = match.MVPs, 
                    Rating = match.Rating,
                }; 
                await _context.PlayerMatches.AddAsync(pm);
            }
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
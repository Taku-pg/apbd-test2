using apbd_test2.DTO;

namespace apbd_test2.Service;

public interface IPlayerService
{
    Task<PlayerMatchDTO> GetPlayerById(int playerId);
    Task AddPlayer(NewPlayerDTO newPlayer);
}
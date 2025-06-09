using apbd_test2.CustomException;
using apbd_test2.DTO;
using apbd_test2.Model;
using apbd_test2.Service;
using Microsoft.AspNetCore.Mvc;

namespace apbd_test2.Controller;

[ApiController]
[Route("api/[controller]")]
public class PlayersController:ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayersController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpGet("{id}/matches")]
    public async Task<IActionResult> GetMatches(int id)
    {
        try
        {
            var player = await _playerService.GetPlayerById(id);
            return Ok(player);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPlayer([FromBody] NewPlayerDTO newPlayer)
    {
        try
        {
            await _playerService.AddPlayer(newPlayer);
            return Created("", newPlayer);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}
using AutoLife.Application.DTOs.FavoriteDTOs;
using AutoLife.Infrastructure.Services.FavoriteServices;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoriteController : ControllerBase
{
    private readonly IFavoriteService _favoriteService;

    public FavoriteController(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    [HttpPost]
    public async Task<IActionResult> AddFavorite([FromBody] FavoriteCreateDto favoriteDto)
    {
        try
        {
            await _favoriteService.AddFavoriteAsync(favoriteDto);
            return Ok("Favorite successfully added.");
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFavorites()
    {
        try
        {
            var favorites = await _favoriteService.GetAllFavoritesAsync();
            return Ok(favorites);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetFavoriteById(Guid id)
    {
        try
        {
            var favorite = await _favoriteService.GetFavoriteByIdAsync(id);
            return Ok(favorite);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> RemoveFavorite(Guid id)
    {
        try
        {
            await _favoriteService.RemoveFavoriteAsync(id);
            return Ok("Favorite successfully removed.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

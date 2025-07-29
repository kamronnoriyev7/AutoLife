using AutoLife.Application.DTOs.NewsDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Infrastructure.Services.NewsServices;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] NewsCreateDto dto)
    {
        await _newsService.AddNewsAsync(dto);
        return Ok("News created successfully.");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<News>>> GetAllAsync()
    {
        var newsList = await _newsService.GetAllNewsAsync();
        return Ok(newsList);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<News>> GetByIdAsync(Guid id)
    {
        var news = await _newsService.GetNewsByIdAsync(id);
        return Ok(news);
    }

    [HttpGet("latest")]
    public async Task<ActionResult<IEnumerable<News>>> GetLatestAsync([FromQuery] int count = 5)
    {
        var newsList = await _newsService.GetLatestNewsAsync(count);
        return Ok(newsList);
    }

    [HttpGet("by-category/{categoryId:guid}")]
    public async Task<ActionResult<IEnumerable<News>>> GetByCategoryAsync(Guid categoryId)
    {
        var newsList = await _newsService.GetNewsByCategoryIdAsync(categoryId);
        return Ok(newsList);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] NewsCreateDto dto)
    {
        await _newsService.UpdateNewsAsync(id, dto);
        return Ok("News updated successfully.");
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _newsService.DeleteNewsAsync(id);
        return Ok("News deleted successfully.");
    }
}

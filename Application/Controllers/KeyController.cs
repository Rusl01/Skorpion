using System.Threading.Tasks;
using Application.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks;

namespace Application.Controllers;

/// <summary>
/// Контроллер для управления API для разработчиков
/// </summary>
[Route("api/[controller]")]
public class KeyController : Controller
{
    private readonly ApplicationContext _db;
    
    public KeyController(ApplicationContext context)
    {
        _db = context;
    }
    
    /// <summary>
    /// Возвращает id игры по ключу активации, если ключ валидный
    /// </summary>
    [HttpGet("{key:guid}")] // api/key/{key}
    public async Task<IActionResult> Get(Guid key)
    {
        var keys = _db.Keys;
        
        if (await keys.AnyAsync(x => x.Id == key))
        {
            var game = keys.FirstAsync(x => x.Id == key).Result.GameId;
            return Ok(game);
        }

        return NotFound();
    }

}
using Application.Controllers;
using Application.Data;
using Microsoft.Extensions.Logging;
using Xunit;

namespace ApplicationTests;

public class UnitTest1
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationContext _db;
    
    public UnitTest1(ILogger<HomeController> logger, ApplicationContext context)
    {
        _logger = logger;
        _db = context;
    }
    
    [Fact]
    public void IndexReturnsAViewWithAListOfGames()
    {
        
    }
}
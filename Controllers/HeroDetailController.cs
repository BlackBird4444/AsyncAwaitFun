using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OverwatchAPI.Models;

namespace OverwatchAPI.Controllers;

public class HeroDetailController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public HeroDetailController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task <IActionResult> Index(string key)
    { 
        try 
        {
            Hero? heroDetail = await Hero.GetHeroDetailAsync(key);
          
            if (heroDetail == null )
            {
                _logger.LogError("hero null");
                return Error(); 
            }

            return View(heroDetail);
        }
        catch (Exception ex)
        {
             _logger.LogError(ex, "error listing a hero");
            return Error(); 
        }
    }
    
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
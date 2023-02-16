using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OverwatchAPI.Models;

namespace OverwatchAPI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task <IActionResult> Index()
    {
        try 
        {
            _logger.LogDebug("starting async call");
            var timer = System.Diagnostics.Stopwatch.StartNew();

            _logger.LogDebug("about to call Async");
            IList<Hero>? heroList = await Hero.GetHeroesAsync(); 
            _logger.LogDebug("async started - testing async, should run before ending async logger");
            //TODO: get logs to output
            
            timer.Stop();
            _logger.LogDebug("ending async call - Elapsed time from start: " + timer.ElapsedMilliseconds);
            
            //How to test if function is ASYNC and NOT
            //write scipts that send log to 

            if (heroList == null )
            {
                _logger.LogError("herolist is null");
                return Error(); 
            }

            return View(heroList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error listing heroes");
            return Error(); 
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

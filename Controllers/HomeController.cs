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
            //_logger.LogDebug("starting async call");
            var timer = System.Diagnostics.Stopwatch.StartNew();

            _logger.LogDebug("about to call Async");
            IList<Hero>? heroList = await Hero.GetHeroesAsync(_logger); 
            _logger.LogDebug("log after async call");
            
            timer.Stop();
           // _logger.LogDebug("ending async call - Elapsed time from start: " + timer.ElapsedMilliseconds);
            
            if (heroList == null )
            {
                _logger.LogError("herolist is null");
                return Error(); 
            }

            return View(heroList);
        }
        catch (Exception ex)
        {
            string errorMesage = "error listing heroes";
            _logger.LogError(ex, errorMesage);
            return Error(errorMesage); 
        }
    }

    public IActionResult Privacy()
    {   //https://learn.microsoft.com/en-us/aspnet/core/mvc/views/overview?view=aspnetcore-7.0
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(string errorMessage = "")
    {

        //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        
        //why is supplying the ViewName bypassing app.Environment.IsDevelopment? 
        ErrorViewModel err = new ErrorViewModel { RequestId = Activity.Current?.Id, ErrorMessage = errorMessage ?? HttpContext.TraceIdentifier };
        return View("Error", err);
    }
}

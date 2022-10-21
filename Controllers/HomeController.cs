using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OverwatchAPI.Models;
using System.Text.Json;

namespace OverwatchAPI.Controllers;

//[ApiController]  //is this needed?
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task <IActionResult> Index()
    {
        IList<Hero> heroList = await GetHeroesAsync();   
        return View(heroList);
    }

    //[HttpGet]
    public async Task<IList<Hero>> GetHeroesAsync()
    {
        //try
        //{
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://overwatch-hero-api.herokuapp.com/api/v1/heroes");
    
            if (response.IsSuccessStatusCode)
            {
               
                var heroList = response.Content.ReadFromJsonAsync<IList<Hero>>();
                return heroList.Result;  
                //TODO: return right return type to fix warning
                // read up on extension methods
              
            }
            else
            {
                return null;
            }
        //}
        //catch(HttpRequestException e)
        //{
        //    return null;
        //    Console.WriteLine("Exception: " + e.Message);
        //}
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

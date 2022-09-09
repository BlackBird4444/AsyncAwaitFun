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
        //var test = GetHeroesAsync();


        //homework: google asynchronous programing in .net, understand what async and await operator do
        //what happens to result without await: 
            //it will execute synchronously: see sleep10SecondsAsyncNoAwait()
            //same for async method missint away: see GetHeroesAsync()
        
        //When the await keyword is applied, it suspends the calling method and yields control back to its caller until the awaited task is complete.
        
        //TestSynchronous();
        await TestAsynchronous();
        
        return View();
    }

    public void TestSynchronous()
    {
        sleep10Seconds();
        sleep1Second();
    }

    public void sleep10Seconds()
    {
        Task.Delay(10000);
        Console.WriteLine("10 seconds");
    }

    public void sleep1Second()
    {
        Task.Delay(1000);
        Console.WriteLine("1 second");
    }

    public async Task TestAsynchronous()  //Task<IActionResult> ??
    {
        // sleep10SecondsAsync();
        // sleep1SecondAsync();
        // Console.WriteLine("done");

        await Task.WhenAll(sleep10SecondsAsync(), sleep1SecondAsync());
        Console.WriteLine("when all done");

    }

    public async Task sleep10SecondsAsync()
    {
        Console.WriteLine("starting to wait 10 seconds");
        await Task.Delay(10000);
        Console.WriteLine("end wait 10 seconds");
    }

    public async Task sleep1SecondAsync()
    {
        Console.WriteLine("starting to wait 1 second");
        await Task.Delay(1000);
        Console.WriteLine("end wait 1 second");
    }


    public async Task sleep10SecondsAsyncNoAwait()
    {
        Task.Delay(10000);
        Console.WriteLine("10 seconds");
    }

    public async Task<string?> GetHeroesAsync()
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync("https://overwatch-hero-api.herokuapp.com/api/v1/heroes");
        //TODO: set response content to return jso
        var temp = response.Content.ReadAsStringAsync(); 
        //TODO: check for status code before getting content 
        var results = temp.Result;
        return results;
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

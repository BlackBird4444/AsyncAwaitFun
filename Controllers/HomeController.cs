using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OverwatchAPI.Models;
using System.Net;

namespace OverwatchAPI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    //TODO: user httpClient and make all actions async
    //private readonly HttpClient _httpClient; 

    private readonly HttpWebRequest _httpWebRequest;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        //TODO: should be in program.cs and injected in controllers so they all use same instance
        // service injection
        //_httpClient = new HttpClient();  
        
          }

    public IActionResult Index()  //TODO: make async later
    {
        
        try{
            //HttpResponseMessage response = await _httpClient.GetAsync("https://overwatch-hero-api.herokuapp.com/api/v1/heroes");

            HttpWebRequest _httpWebRequest = (HttpWebRequest)WebRequest.Create("https://overwatch-hero-api.herokuapp.com/api/v1/heroes"); //static method                                          
            _httpWebRequest.Accept = "/";
            //_httpWebRequest. AcceptEncoding
            
            WebResponse response = _httpWebRequest.GetResponse();
            string text;
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }

            _logger.LogInformation("Response = " + text);
        }
        catch(HttpRequestException ex)
        {
            _logger.LogError("Error Message: " + ex.Message);
        }
        
        return View();
    }

    // public Add(int number, T item) //passing in any data Type
    // {
    //     List<string> test = new List<string>();

    //     test.Add("test 2");
        
    // } 

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

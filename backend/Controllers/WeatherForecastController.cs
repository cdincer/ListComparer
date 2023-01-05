using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        //This profile belongs to  SteamDB's creator. It's his public wishlist.
        //It's ok to share it in a public repo like this and It's ok to draw attention like this because he deserves it 
        //and he literally accepts friends for wishlist donations. SteamDB is a great source, I wish I can do more for it, this is it.  
        string url = "https://store.steampowered.com/wishlist/id/xPaw/#sort=order";
        //g_rgWishlistData variable to look for.This is our wishlist variable.
        HttpClient client = new HttpClient();
        string response = client.GetStringAsync(url).Result;
        int begin = response.IndexOf("var g_rgWishlistData");
        int end = response.IndexOf("var g_rgAppInfo") - 4;//End with some to spare that's why we subtract the 4.

        StringBuilder BasicWishListBuilder = new StringBuilder();
        for (int i = begin; i < end; i++)
        {
            BasicWishListBuilder.Append(response[i]);
        }

        string BasicWishList = BasicWishListBuilder.ToString();
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(response);




        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

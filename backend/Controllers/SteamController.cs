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
public class SteamController : ControllerBase
{
    private readonly ILogger<SteamController> _logger;

    public SteamController(ILogger<SteamController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "SteamWishlist")]
    public string Get()
    {
        //My test user
        string url = "https://store.steampowered.com/wishlist/profiles/76561199468516180/#sort=order";

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

        return BasicWishList;
    }
}

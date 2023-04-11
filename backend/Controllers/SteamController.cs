using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json;
using HtmlAgilityPack;
using backend.Helper;
using backend.Controllers.Entities;

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

    [HttpPost(Name = "SteamWishlist")]
    public string Get([FromBody] object options)
    {
        //My test user
        //string url = "https://store.steampowered.com/wishlist/profiles/76561199468516180/#sort=order";
        SteamWishlistOptions Options = Newtonsoft.Json.JsonConvert.DeserializeObject<SteamWishlistOptions>(options.ToString());
        //g_rgWishlistData variable to look for.This is our wishlist variable.
        SteamHelper steamHelper = new SteamHelper();
        HttpClient client = new HttpClient();
        string url = Options.profileAddress;

        string response = client.GetStringAsync(url).Result;
        client.Dispose();
        int begin = response.IndexOf("g_rgWishlistData") + 19;
        int end = response.IndexOf("var g_rgAppInfo") - 4;//End with some to spare that's why we subtract the 4.

        StringBuilder BasicWishListBuilder = new StringBuilder();
        for (int i = begin; i < end; i++)
        {
            BasicWishListBuilder.Append(response[i]);
        }


        List<SteamWishList> STBR = steamHelper.TitleHarvester(BasicWishListBuilder.ToString());
        STBR = steamHelper.NamePriceHarvester(STBR);
        var JsonSTBR = JsonConvert.SerializeObject(STBR);
        BasicWishListBuilder.Clear();
        STBR.Clear();
        return JsonSTBR;
    }

}
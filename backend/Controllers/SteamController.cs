using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json;
using HtmlAgilityPack;

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
        int begin = response.IndexOf("g_rgWishlistData") + 19;
        int end = response.IndexOf("var g_rgAppInfo") - 4;//End with some to spare that's why we subtract the 4.

        StringBuilder BasicWishListBuilder = new StringBuilder();
        for (int i = begin; i < end; i++)
        {
            BasicWishListBuilder.Append(response[i]);
        }


        List<SteamWishList> STBR = TitleHarvester(BasicWishListBuilder.ToString());
        STBR = NamePriceHarvester(STBR);
        var JsonSTBR = JsonConvert.SerializeObject(STBR);

        return JsonSTBR;
    }


    public List<SteamWishList> TitleHarvester(string response)
    {
        int begin = response.IndexOf("[");
        int end = response.IndexOf("]");//End with some to spare that's why we subtract the 4.

        StringBuilder BasicWishListBuilder = new StringBuilder();
        BasicWishListBuilder.Append(response);
        List<string> ItemsToAdd = new List<string>();
        while (BasicWishListBuilder.ToString().Contains("\"appid\":"))
        {
            int startIndex = BasicWishListBuilder.ToString().IndexOf("\"appid\":");
            int gameIndex = startIndex + 8;
            StringBuilder GameName = new StringBuilder();
            while (BasicWishListBuilder[gameIndex] != ',')
            {
                GameName.Append(BasicWishListBuilder[gameIndex]);
                gameIndex++;
            }
            ItemsToAdd.Add(GameName.ToString());
            BasicWishListBuilder.Remove(startIndex, 8);
            GameName.Clear();
        }

        List<SteamWishList> STBR = new List<SteamWishList>();
        foreach (string Item in ItemsToAdd)
        {
            STBR.Add(new SteamWishList { appid = Item, added = "PlaceHolder" });
        }
        return STBR;
    }
    //https://store.steampowered.com/app/217200 Worms Armageddon page
    //https://store.steampowered.com/app/374320 DARK SOULS™ III page
    public List<SteamWishList> NamePriceHarvester(List<SteamWishList> WishList)
    {
        string url = "https://store.steampowered.com/app/";
        List<SteamWishList> STBR = new List<SteamWishList>();

        foreach (SteamWishList item in WishList)
        {
            HttpClient client = new HttpClient();
            string response = client.GetStringAsync(url + item.appid).Result;
            string ItemPrice = "";
            string Title = "";
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(response);
            HtmlNode title = document.GetElementbyId("appHubAppName");
            HtmlNodeCollection price = document.DocumentNode.SelectNodes("//div[contains(@class, 'game_purchase_price')]");
            if (price != null)
            {
                ItemPrice = price[0].InnerHtml;
            }
            else
            {
                ItemPrice = "0";
            }
            int emptyCheck = 0;
            if (title != null)
            {
                Title = title.InnerHtml;
            }
            else
            {
                Title = emptyCheck.ToString();
                emptyCheck++;
            }

            STBR.Add(new SteamWishList { appid = item.appid, title = Title, added = "PlaceHolder", price = ItemPrice });
        }



        return STBR;

    }

}

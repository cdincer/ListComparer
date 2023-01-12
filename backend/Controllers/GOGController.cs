using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class GOGController : ControllerBase
{
    private readonly ILogger<GOGController> _logger;

    public GOGController(ILogger<GOGController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GOGWishlist")]
    public string Get()
    {
        //My test user
        string url = "https://www.gog.com/en/u/listcomparer/wishlist";

        //g_rgWishlistData variable to look for.This is our wishlist variable.
        HttpClient client = new HttpClient();
        string response = client.GetStringAsync(url).Result;
        int begin = response.IndexOf("var gogData");
        int end = response.IndexOf("var translationData") - 4;//End with some to spare that's why we subtract the 4.

        StringBuilder BasicWishListBuilder = new StringBuilder();
        for (int i = begin; i < end; i++)
        {
            BasicWishListBuilder.Append(response[i]);
        }
        BasicWishListBuilder.Replace("var gogData = ", "");

        List<string> ItemsToAdd = new List<string>();
        while (BasicWishListBuilder.ToString().Contains("\"title\":"))
        {
            int startIndex = BasicWishListBuilder.ToString().IndexOf("\"title\":");
            int gameIndex = startIndex + 9;
            StringBuilder GameName = new StringBuilder();
            while (BasicWishListBuilder[gameIndex] != '"')
            {
                GameName.Append(BasicWishListBuilder[gameIndex]);
                gameIndex++;
            }
            ItemsToAdd.Add(GameName.ToString());
            BasicWishListBuilder.Remove(startIndex, 8);
            GameName.Clear();
        }




        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(response);

        return ";;;";
    }
}
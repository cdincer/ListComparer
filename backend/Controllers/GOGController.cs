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
using backend.Controllers.Entities;

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

    [HttpPost(Name = "GOGWishlist")]
    public string Post([FromBody] object options)
    {
        GOGWishlistOptions Options = Newtonsoft.Json.JsonConvert.DeserializeObject<GOGWishlistOptions>(options.ToString());
        GOGHelper Helper = new GOGHelper();
        //My test user
        string url = "https://www.gog.com/en/u/listcomparer/wishlist"; //Options.profileAddress;
        //g_rgWishlistData variable to look for.This is our wishlist variable.
        HttpClient client = new HttpClient();
        string response = client.GetStringAsync(url).Result;
        List<GOGWishlist> GTBR = Helper.TitleHarvester(response);
        GTBR = Helper.PriceHarvester(response, GTBR);
        var JsonGTBR = JsonConvert.SerializeObject(GTBR);

        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(response);

        return JsonGTBR;
    }


}

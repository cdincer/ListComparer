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
using backend.Helper;

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
        AddressHelper GetWishListPage = new AddressHelper();
        GOGHelper Helper = new GOGHelper();
        HttpClient client = new HttpClient();
        GOGWishlistOptions Options = Newtonsoft.Json.JsonConvert.DeserializeObject<GOGWishlistOptions>(options.ToString());
        List<GOGWishlist> FinalList = new List<GOGWishlist>();
        string url, JsonGTBR, response = "";
        bool overcapacityCheck = false;
        url = Options.profileAddress;

        while (overcapacityCheck)
        {
            response = client.GetStringAsync(url).Result;
            overcapacityCheck = Helper.CheckNotOverCapacity(response);
            List<GOGWishlist> MidList = Helper.TitleHarvester(response);
            MidList = Helper.PriceHarvester(response, MidList);
            FinalList.AddRange(MidList);

        }

        JsonGTBR = JsonConvert.SerializeObject(FinalList);
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(response);

        return JsonGTBR;
    }


}

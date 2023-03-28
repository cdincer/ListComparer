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
    private readonly string ExtraGOGWishlistPage = "GOGWishListExtraPage";
    private readonly string UserIDPlaceHolder = "PlaceholderValue";
    public GOGController(ILogger<GOGController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "GOGWishlist")]
    public string Post([FromBody] object options)
    {
        AddressHelper GetAddress = new AddressHelper();
        GOGHelper Helper = new GOGHelper();
        HttpClient client = new HttpClient();
        GOGWishlistOptions Options = Newtonsoft.Json.JsonConvert.DeserializeObject<GOGWishlistOptions>(options.ToString());
        List<GOGWishlist> FinalList = new List<GOGWishlist>();
        string? url, JsonGTBR, response = "";
        url = Options.profileAddress;

        response = client.GetStringAsync(url).Result;
        string UserID = Helper.FindUserID(response);
        url = GetAddress.GetTargetAddress(ExtraGOGWishlistPage);
        url = url.Replace(UserIDPlaceHolder, UserID);
        response = client.GetStringAsync(url).Result;

        List<GOGWishlist> GTBR = Helper.TitleHarvesterForJSON(response);
        GTBR = Helper.PriceHarvesterForJSON(response, GTBR);
        JsonGTBR = JsonConvert.SerializeObject(GTBR);

        return JsonGTBR;
    }


}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using backend.Controllers.Entities;
using backend.Helper;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class BargainController : ControllerBase
{
    [HttpGet(Name = "Bargains")]
    public async Task<string> Get()
    {
        EpicHelper epicHelper = new EpicHelper();
        string BargainTBR = "";
        List<BargainFreeGames> EpicGames = new List<BargainFreeGames>();
        EpicGames = epicHelper.NameHarvester();
        EpicGames = epicHelper.TimeHarvesterStart(EpicGames);
        EpicGames = epicHelper.TimeHarvesterEnd(EpicGames);

        AmazonHelper amazonHelper = new AmazonHelper();
        List<BargainFreeGames>  AmazonGames = await amazonHelper.NameHarvester();
        EpicGames.AddRange((IEnumerable<BargainFreeGames>)AmazonGames);

        BargainTBR = JsonConvert.SerializeObject(EpicGames);
        return BargainTBR;
    }

}

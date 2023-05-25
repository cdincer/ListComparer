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
    public string Get()
    {
        EpicHelper epicHelper = new EpicHelper();
        string BargainTBR = "";
        List<EpicFreeGames> Names = new List<EpicFreeGames>();
        Names = epicHelper.NameHarvester();
        Names = epicHelper.TimeHarvesterStart(Names);
        Names = epicHelper.TimeHarvesterEnd(Names);
        BargainTBR = JsonConvert.SerializeObject(Names);
        return BargainTBR;
    }

}

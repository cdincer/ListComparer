using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Controllers.Entities
{
    public class GOGHelper
    {
            public List<GOGWishlist> TitleHarvester(string response)
    {
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

        List<GOGWishlist> GTBR = new List<GOGWishlist>();
        foreach (string Item in ItemsToAdd)
        {
            GTBR.Add(new GOGWishlist { Name = Item, Price = "45" });
        }
        return GTBR;
    }

    public List<GOGWishlist> PriceHarvester(string response, List<GOGWishlist> HarvestedTitles)
    {
        int begin = response.IndexOf("var gogData");
        int end = response.IndexOf("var translationData") - 4;//End with some to spare that's why we subtract the 4.

        StringBuilder BasicWishListBuilder = new StringBuilder();
        for (int i = begin; i < end; i++)
        {
            BasicWishListBuilder.Append(response[i]);
        }
        BasicWishListBuilder.Replace("var gogData = ", "");

        List<string> ItemsToAdd = new List<string>();
        while (BasicWishListBuilder.ToString().Contains("\"amount\":"))
        {
            int startIndex = BasicWishListBuilder.ToString().IndexOf("\"amount\":");
            int gameIndex = startIndex + 10;
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

        List<GOGWishlist> GTBR = new List<GOGWishlist>();
        int TitleIndex = 0;
        foreach (string Item in ItemsToAdd)
        {
            GTBR.Add(new GOGWishlist { Name = HarvestedTitles[TitleIndex].Name, Price = Item });
            TitleIndex++;
        }
        return GTBR;
    }

    }
}
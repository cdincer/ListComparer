using System.Text;
using backend.Controllers;
using HtmlAgilityPack;

namespace backend.Helper
{
    public class EpicHelper
    {

        public List<EpicFreeGames> NameHarvester()
        {
            //https://store-site-backend-static.ak.epicgames.com/freeGamesPromotions
            string url = "https://store-site-backend-static.ak.epicgames.com/freeGamesPromotions";
            HttpClient client = new HttpClient();
            string response = client.GetStringAsync(url).Result;

            int begin = response.IndexOf("{");
            int end = response.LastIndexOf("}");
            List<EpicFreeGames> GTBR = new List<EpicFreeGames>();
            List<string> ItemsToAdd = new List<string>();

            StringBuilder BasicWishListBuilder = new StringBuilder();
            for (int i = begin; i < end; i++)
            {
                BasicWishListBuilder.Append(response[i]);
            }
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
            foreach (string Item in ItemsToAdd)
            {
                GTBR.Add(new EpicFreeGames { Name = Item });
            }
            return GTBR;
        }

        public List<EpicFreeGames> TimeHarvesterStart(List<EpicFreeGames> HarvestedTitles)
        {
            //https://store-site-backend-static.ak.epicgames.com/freeGamesPromotions
            string url = "https://store-site-backend-static.ak.epicgames.com/freeGamesPromotions";
            HttpClient client = new HttpClient();
            string response = client.GetStringAsync(url).Result;

            int begin = response.IndexOf("{");
            int end = response.LastIndexOf("}");
            List<EpicFreeGames> GTBR = new List<EpicFreeGames>();
            List<string> ItemsToAdd = new List<string>();

            StringBuilder BasicWishListBuilder = new StringBuilder();
            for (int i = begin; i < end; i++)
            {
                BasicWishListBuilder.Append(response[i]);
            }
            while (BasicWishListBuilder.ToString().Contains("\"startDate\":"))
            {
                int startIndex = BasicWishListBuilder.ToString().IndexOf("\"startDate\":");
                int gameIndex = startIndex + 13;
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
            int TitleIndex = 0;
            foreach (string Item in ItemsToAdd)
            {
                GTBR.Add(new EpicFreeGames { Name = HarvestedTitles[TitleIndex].Name, TimeStart = Item });
                TitleIndex++;
            }
            return GTBR;
        }

        //Only real free products have a ExpiryDate, dummy products 
        //like Mysterious Game and Borderlands 3 Pass don't have anything
        public List<EpicFreeGames> TimeHarvesterEnd(List<EpicFreeGames> HarvestedTitles)
        {
            //https://store-site-backend-static.ak.epicgames.com/freeGamesPromotions
            string url = "https://store-site-backend-static.ak.epicgames.com/freeGamesPromotions";
            HttpClient client = new HttpClient();
            string response = client.GetStringAsync(url).Result;

            int begin = response.IndexOf("{");
            int end = response.LastIndexOf("}");
            List<EpicFreeGames> GTBR = new List<EpicFreeGames>();
            List<string> ItemsToAdd = new List<string>();

            StringBuilder BasicWishListBuilder = new StringBuilder();
            for (int i = begin; i < end; i++)
            {
                BasicWishListBuilder.Append(response[i]);
            }
            while (BasicWishListBuilder.ToString().Contains("\"endDate\":"))
            {
                int startIndex = BasicWishListBuilder.ToString().IndexOf("\"endDate\":");
                int gameIndex = startIndex + 11;
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
            int TitleIndex = 0;
            foreach (string Item in ItemsToAdd)
            {
                GTBR.Add(new EpicFreeGames { Name = HarvestedTitles[TitleIndex].Name, TimeStart = HarvestedTitles[TitleIndex].TimeStart, TimeEnd = Item });
                TitleIndex++;
            }
            return GTBR;
        }

    }
}
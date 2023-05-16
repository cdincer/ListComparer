using System.Text;
using backend.Controllers;
using HtmlAgilityPack;

namespace backend.Helper
{
    public class EpicHelper
    {

        public string NameHarvester()
        {
            //https://store-site-backend-static.ak.epicgames.com/freeGamesPromotions
            string url = "https://store-site-backend-static.ak.epicgames.com/freeGamesPromotions";
            HttpClient client = new HttpClient();
            string response = client.GetStringAsync(url).Result;

            int begin = response.IndexOf("{");
            int end = response.LastIndexOf("}");
            List<GOGWishlist> GTBR = new List<GOGWishlist>();
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
                GTBR.Add(new GOGWishlist { Name = Item, Price = "45" });
            }
            string Title = "";
            return "";
        }

        //Only real free products have a ExpiryDate, dummy products 
        //like Mysterious Game and Borderlands 3 Pass don't have anything
        public string TimeHarvester()
        {
            //https://store-site-backend-static.ak.epicgames.com/freeGamesPromotions
            string url = "https://store-site-backend-static.ak.epicgames.com/freeGamesPromotions";
            HttpClient client = new HttpClient();
            string response = client.GetStringAsync(url).Result;

            int begin = response.IndexOf("{");
            int end = response.LastIndexOf("}");
            List<GOGWishlist> GTBR = new List<GOGWishlist>();
            List<string> ItemsToAdd = new List<string>();

            StringBuilder BasicWishListBuilder = new StringBuilder();
            for (int i = begin; i < end; i++)
            {
                BasicWishListBuilder.Append(response[i]);
            }
            while (BasicWishListBuilder.ToString().Contains("\"expiryDate\":"))
            {
                int startIndex = BasicWishListBuilder.ToString().IndexOf("\"expiryDate\":");
                int gameIndex = startIndex + 14;
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
                GTBR.Add(new GOGWishlist { Name = Item, Price = "45" });
            }
            string Title = "";
            return "";
        }

    }
}
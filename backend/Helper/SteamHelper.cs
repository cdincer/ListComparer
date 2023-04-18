using System.Text;
using backend.Controllers;
using HtmlAgilityPack;

namespace backend.Helper
{
    public class SteamHelper
    {
        ///Get all the Appid from the initial wishlist so it can be used in the NamePriceHarvester
        public List<SteamWishList> WishlistHarvester(string response)
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
                STBR.Add(new SteamWishList { Appid = Item, Added = "PlaceHolder" });
            }
            BasicWishListBuilder.Clear();
            return STBR;
        }

        //Use the appid to make calls to indiviual pages to get more details.
        public List<SteamWishList> NamePriceHarvester(List<SteamWishList> WishList)
        {
            string url = "https://store.steampowered.com/app/";
            List<SteamWishList> STBR = new List<SteamWishList>();

            foreach (SteamWishList item in WishList)
            {
                HttpClient client = new HttpClient();
                string response = client.GetStringAsync(url + item.Appid).Result;
                string ItemPrice = "";
                string Title = "";
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(response);
                HtmlNode title = document.GetElementbyId("appHubAppName");
                HtmlNodeCollection price = document.DocumentNode.SelectNodes("//div[contains(@class, 'game_purchase_price')]");
                HtmlNodeCollection SummarGridList = document.DocumentNode.SelectNodes("//div[contains(@class, 'grid_content')]");
                HtmlNode PublisherGrid = SummarGridList[1];
                string Publisher = PublisherGrid.InnerText.Replace("\t", "").Replace("\r", "").Replace("\n", "");

                if (price != null)
                {
                    ItemPrice = price[0].InnerHtml;
                    price.Clear();
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

                STBR.Add(new SteamWishList { Appid = item.Appid, Title = Title, Added = "-", Price = ItemPrice, Publisher = Publisher });
                client.Dispose();
            }
            return STBR;
        }
    }
}
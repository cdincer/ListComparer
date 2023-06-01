using System.Text;
using backend.Controllers;
using PuppeteerSharp;

namespace backend.Helper
{
    public class AmazonHelper
    {
        //https://scrapfly.io/blog/web-scraping-graphql-with-python/
        public async void NameHarvester()
        {
            string url = "https://gaming.amazon.com/home";
            string response = "";
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            IBrowser browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });
            var page = await browser.NewPageAsync();
            page.DefaultTimeout = 0; // or you can set this as 0
            await page.GoToAsync(url, WaitUntilNavigation.Networkidle2);
            var content = await page.GetContentAsync();

            Console.WriteLine(content);


            int begin = response.IndexOf("{");
            int end = response.LastIndexOf("}");
            List<BargainFreeGames> GTBR = new List<BargainFreeGames>();
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
                GTBR.Add(new BargainFreeGames { Name = Item });
            }
        }
    }
}
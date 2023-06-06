using System.Text;
using backend.Controllers;
using PuppeteerSharp;

namespace backend.Helper
{
    public class  AmazonHelper
    {
        //<button aria-label=\"Claim game Sengoku 2\"
        //<button aria-label=\"Claim game Planescape Torment: Enhanced Edition\"
        //Endings Example for Mutation Nation: title=\"Ends in\">Ends in</span> 177 days
        public async Task<List<BargainFreeGames>> NameHarvester()
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
            await browser.CloseAsync();
            response = content;
            int begin = response.IndexOf("<button aria-label=\"Claim game");
            int end = response.LastIndexOf("<button aria-label=\"Claim game");
            List<BargainFreeGames> GTBR = new List<BargainFreeGames>();
            List<string> ItemsToAdd = new List<string>();

            StringBuilder BasicWishListBuilder = new StringBuilder();
            for (int i = begin; i < end; i++)
            {
                BasicWishListBuilder.Append(response[i]);
            }
            while (BasicWishListBuilder.ToString().Contains("<button aria-label=\"Claim game"))
            {
                int startIndex = BasicWishListBuilder.ToString().IndexOf("<button aria-label=\"Claim game");
                int gameIndex = startIndex + 31;
                StringBuilder GameName = new StringBuilder();
                while (BasicWishListBuilder[gameIndex] != '\"')
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
            return GTBR;
        }
    }
}
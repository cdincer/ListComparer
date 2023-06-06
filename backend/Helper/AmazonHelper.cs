using System.Text;
using backend.Controllers;
using PuppeteerSharp;

namespace backend.Helper
{
    public class AmazonHelper
    {
        // <button class="tw-interactive tw-block tw-border-radius-pill tw-full-width tw-interactable tw-interactable--alpha tw-interactable--selected" data-type="Game" data-a-target="offer-filter-button-Game-selected"><div class="tw-pd-x-1 tw-pd-y-05 tw-sm-pd-x-2 tw-sm-pd-y-1"><div class="tw-pd-x-05 tw-pd-y-05"><p class="offer-filters__button__title offer-filters__button__title--Game tw-amazon-ember-bold tw-c-text-white tw-font-size-7 tw-md-font-size-4 tw-nowrap tw-sm-font-size-5" title="Weekly game">Weekly game</p></div></div></button>
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
            await browser.CloseAsync();

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
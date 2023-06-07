using System.Text;
using backend.Controllers;
using PuppeteerSharp;
using PuppeteerSharp.Input;

namespace backend.Helper
{
    public class AmazonHelper
    {
        //<button aria-label=\"Claim game Sengoku 2\"
        //<button aria-label=\"Claim game Planescape Torment: Enhanced Edition\"
        //Endings Example for Mutation Nation: title=\"Ends in\">Ends in</span> 177 days
        //<p class="offer-filters__button__title offer-filters__button__title--Game tw-amazon-ember-bold tw-c-text-white tw-font-size-7 tw-md-font-size-4 tw-nowrap tw-sm-font-size-5" title="Weekly game">Weekly game</p>
        //<button class="tw-interactive tw-block tw-full-width tw-interactable tw-interactable--inverted tw-interactable--selected" data-target="language-selector-link" data-a-target="language-selector-link" data-language="en-US"><div class="language-selector__option language-selector__option--celeste tw-align-items-center tw-flex tw-full-width tw-md-pd-x-2 tw-pd-x-2 tw-pd-y-0"><div data-a-target="language-en-US" class="tw-c-text-inherit tw-flex-grow-1"><p class="tw-amazon-ember-bold tw-c-text-white tw-font-size-6 tw-md-font-size-5 tw-strong" title="English">English</p></div><div data-a-target="language-selector-link-checkmark" class="tw-c-text-white tw-flex tw-flex-shrink-1 tw-mg-l-2"><figure aria-label="Check" class="tw-svg"><svg class="tw-svg__asset tw-svg__asset--check tw-svg__asset--inherit" width="18px" height="18px" version="1.1" viewBox="0 0 20 20" x="0px" y="0px"><path d="M7.429 15c-.245 0-.49-.09-.683-.269l-3.428-3.2a1 1 0 0 1 1.364-1.463l2.747 2.564 7.889-7.363a1 1 0 0 1 1.365 1.462l-8.571 8a1.002 1.002 0 0 1-.683.27" fill-rule="evenodd"></path></svg></figure></div></div></button>
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
            //await page.WaitForSelectorAsync(".language-selector__menu");
            await page.ClickAsync(".language-selector__menu");
            //await page.WaitForSelectorAsync("button[data-language='en-US']");
            await page.ClickAsync("button[data-language='en-US']");
            response = await page.GetContentAsync();
            await browser.CloseAsync();
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
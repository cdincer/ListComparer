using System.Text;
using backend.Controllers;
using HtmlAgilityPack;
using PuppeteerSharp;
using PuppeteerSharp.Input;

namespace backend.Helper
{
    public class AmazonHelper
    {
        //Endings Example for Mutation Nation: title=\"Ends in\">Ends in</span> 177 days
        //German:<button aria-label="Spiel Tandem: A Tale of Shadows (USK 12) abholen" class="tw-interactive tw-button" data-a-target="FGWPOffer"><span class="tw-button__text" data-a-target="tw-button-text"><p class="tw-pd-x-05 tw-pd-y-05" title="Spiel aktivieren">Spiel aktivieren</p></span></button>
        //English:<button aria-label="Claim game Tandem: A Tale of Shadows" class="tw-interactive tw-button" data-a-target="FGWPOffer"><span class="tw-button__text" data-a-target="tw-button-text"><p class="tw-pd-x-05 tw-pd-y-05" title="Claim game">Claim game</p></span></button>
        //Card German:<div aria-label="Tandem: A Tale of Shadows (USK 12)" class="item-card__action" tabindex="0" data-a-target="claim-prime-offer-card"><div class="tw-relative"><div data-a-target="item-card" class="tw-card tw-relative"><div class="tw-flex tw-flex-column tw-flex-nowrap"><div class="tw-absolute tw-left-0 tw-top-0"><div data-a-target="item-card-badges" class="item-card-badges"><div class="sm-badge-group tw-align-items-center tw-flex tw-z-default"><div class="sm-badge--first" style="z-index: 2;"><div data-a-target="badge-prime" class="sm-badge sm-badge--none sm-badge--prime tw-align-items-center tw-border-bottom-left-radius-none tw-border-bottom-right-radius-bubble tw-border-top-left-radius-extra-extra-large tw-border-top-right-radius-bubble tw-flex tw-justify-content-center tw-pd-x-1 tw-relative"><figure aria-label="LogoPrimeNoSmile" class="tw-svg"><svg class="tw-svg__asset tw-svg__asset--logoprimenosmile tw-svg__asset--inherit" width="35px" height="21px" version="1.1" viewBox="0 0 149 55" x="0px" y="0px"><path fill-rule="evenodd" d="M14.573 37.109c2.122 0 3.686-.72 4.691-2.16 1.004-1.44 1.507-3.715 1.507-6.823 0-3.145-.493-5.438-1.479-6.879-.986-1.44-2.558-2.16-4.719-2.16-1.97 0-3.829.511-5.571 1.535v14.952c1.667 1.023 3.525 1.535 5.571 1.535zm-12.45 17.34c-.493 0-.853-.105-1.081-.313-.226-.208-.341-.579-.341-1.109v-37.92c0-.531.115-.899.341-1.108.228-.208.588-.313 1.081-.313h4.093c.872 0 1.402.417 1.592 1.25l.398 1.478c1.137-1.098 2.51-1.97 4.121-2.614a13.148 13.148 0 014.918-.967c3.676 0 6.585 1.345 8.727 4.037 2.142 2.691 3.213 6.33 3.213 10.915 0 3.146-.531 5.894-1.592 8.244-1.061 2.351-2.502 4.16-4.321 5.429-1.82 1.27-3.905 1.905-6.254 1.905-1.555 0-3.032-.247-4.435-.739-1.402-.493-2.595-1.175-3.581-2.047v12.45c0 .53-.105.901-.312 1.109-.209.208-.579.313-1.11.313H2.123zm34.049-11.712c-.493 0-.853-.113-1.081-.341-.227-.228-.341-.587-.341-1.08V15.107c0-.531.114-.899.341-1.108.228-.208.588-.313 1.081-.313h4.093c.872 0 1.402.417 1.592 1.25l.739 3.07c1.516-1.667 2.946-2.851 4.292-3.552a9.16 9.16 0 014.293-1.052h.796c.53 0 .909.104 1.136.312.228.208.341.579.341 1.109v4.775c0 .494-.104.853-.312 1.08-.209.228-.578.342-1.109.342-.265 0-.606-.019-1.023-.057a18.554 18.554 0 00-1.592-.057c-.872 0-1.932.124-3.183.37s-2.313.559-3.184.937v19.103c0 .493-.105.852-.312 1.08-.209.228-.58.341-1.11.341h-5.457m22.717 0c-.492 0-.853-.113-1.08-.341-.228-.228-.341-.587-.341-1.08V15.107c0-.531.113-.899.341-1.108.227-.208.588-.313 1.08-.313h5.458c.53 0 .9.105 1.108.313.208.209.314.577.314 1.108v26.209c0 .493-.106.852-.314 1.08-.208.228-.578.341-1.108.341h-5.458zm2.729-33.542c-1.441 0-2.596-.398-3.467-1.194-.873-.796-1.308-1.876-1.308-3.241 0-1.365.435-2.445 1.308-3.241.871-.796 2.026-1.193 3.467-1.193 1.44 0 2.596.397 3.468 1.193.871.796 1.308 1.876 1.308 3.241 0 1.365-.437 2.445-1.308 3.241-.872.796-2.028 1.194-3.468 1.194zm12.611 33.542c-.494 0-.854-.113-1.081-.341-.227-.228-.341-.587-.341-1.08V15.107c0-.531.114-.899.341-1.108.227-.208.587-.313 1.081-.313h4.093c.872 0 1.402.417 1.591 1.25l.455 1.535c2.009-1.326 3.819-2.263 5.43-2.813 1.61-.55 3.269-.825 4.975-.825 3.411 0 5.817 1.214 7.219 3.638 1.933-1.288 3.752-2.216 5.458-2.785a16.596 16.596 0 015.288-.853c2.652 0 4.708.739 6.167 2.218 1.459 1.477 2.19 3.543 2.19 6.196v20.069c0 .493-.106.852-.313 1.08-.209.228-.579.341-1.109.341h-5.457c-.493 0-.853-.113-1.08-.341-.228-.228-.342-.587-.342-1.08v-18.25c0-2.577-1.156-3.865-3.468-3.865-2.047 0-4.113.492-6.197 1.477v20.638c0 .493-.104.852-.312 1.08-.209.228-.578.341-1.109.341H92.25c-.493 0-.852-.113-1.08-.341-.227-.228-.341-.587-.341-1.08v-18.25c0-2.577-1.157-3.865-3.467-3.865-2.123 0-4.207.512-6.254 1.534v20.581c0 .493-.106.852-.313 1.08-.209.228-.579.341-1.109.341h-5.457m60.717-16.998c1.932 0 3.334-.294 4.207-.882.871-.587 1.307-1.487 1.307-2.7 0-2.388-1.421-3.582-4.264-3.582-3.638 0-5.742 2.237-6.31 6.709 1.516.304 3.202.455 5.06.455zm1.875 17.85c-4.7 0-8.309-1.287-10.83-3.865-2.521-2.577-3.78-6.272-3.78-11.086 0-4.926 1.287-8.793 3.865-11.599 2.578-2.803 6.16-4.206 10.745-4.206 3.526 0 6.283.853 8.273 2.558 1.989 1.706 2.984 3.98 2.984 6.822 0 2.843-1.071 4.995-3.212 6.454-2.141 1.459-5.296 2.188-9.465 2.188-2.161 0-4.038-.208-5.629-.625.227 2.539.995 4.358 2.302 5.457 1.308 1.101 3.289 1.65 5.942 1.65 1.06 0 2.094-.067 3.098-.199 1.003-.133 2.397-.428 4.178-.882a2.248 2.248 0 01.626-.114c.644 0 .966.437.966 1.308v2.615c0 .607-.085 1.033-.255 1.279-.171.248-.503.465-.995.653-2.768 1.062-5.705 1.592-8.813 1.592z"></path></svg></figure></div></div><div class="sm-badge--cascade" style="z-index: 1;"><div data-a-target="badge-fgwp" class="sm-badge sm-badge--free-game sm-badge--none tw-align-items-center tw-border-bottom-left-radius-none tw-border-bottom-right-radius-bubble tw-border-top-left-radius-none tw-border-top-right-radius-bubble tw-flex tw-justify-content-center tw-pd-x-1 tw-relative"><p class="tw-font-size-6" title="kostenloses Spiel">kostenloses Spiel</p></div></div></div></div></div><div data-a-target="card-image" class="tw-border-top-left-radius-extra-extra-large tw-border-top-right-radius-extra-extra-large tw-card-img tw-flex-shrink-0 tw-overflow-hidden"><figure class="tw-aspect tw-aspect--16x9 tw-aspect--align-top"><picture class="tw-picture"><img class="tw-image" alt="Tandem: A Tale of Shadows (USK 12)" srcset="https://m.media-amazon.com/images/I/41ayiPEttyL._FMwebp_.jpg 1x,https://m.media-amazon.com/images/I/41ayiPEttyL._FMwebp_.jpg 2x" src="https://m.media-amazon.com/images/I/41ayiPEttyL._FMwebp_.jpg"></picture></figure></div><div class="item-card__availability-callout__container item-card__availability-callout__container--free-game tw-c-background-free-game"><div data-a-target="item-card-availability-callout" class="item-card__availability-callout tw-align-items-center tw-c-background-elevation-2 tw-flex tw-flex-wrap"><div class="tw-align-items-center tw-flex tw-flex-wrap tw-md-pd-x-3 tw-md-pd-y-1 tw-pd-x-2 tw-pd-y-1"><div class="tw-block tw-mg-r-1"><div class="item-card__availability-date"><p class="tw-c-text-white tw-font-size-7"><span class="tw-amazon-ember-regular tw-c-text-base tw-font-size-7" title="Endet in">Endet in</span> 13 Tagen</p></div></div></div></div></div><div class="item-card-details item-card-details--free-game tw-border-bottom-left-radius-extra-extra-large tw-border-bottom-right-radius-extra-extra-large tw-c-background-free-game tw-flex tw-flex-column tw-justify-content-between tw-md-pd-x-3 tw-pd-b-3 tw-pd-t-1 tw-pd-x-2"><div class="item-card-details__body tw-flex tw-flex-column tw-flex-grow-1 tw-justify-content-center"><div class="item-card-details__body__primary"><h3 class="tw-amazon-ember-bold tw-c-text-white tw-font-size-6 tw-md-font-size-5 tw-strong" title="Tandem: A Tale of Shadows (USK 12)">Tandem: A Tale of Shadows (USK 12)</h3></div></div><div class="item-card-details__footer tw-align-items-center tw-flex tw-justify-content-between"><div class="tw-pd-r-05"><div class="item-card__claim-button"><button aria-label="Spiel Tandem: A Tale of Shadows (USK 12) abholen" class="tw-interactive tw-button" data-a-target="FGWPOffer"><span class="tw-button__text" data-a-target="tw-button-text"><p class="tw-pd-x-05 tw-pd-y-05" title="Spiel aktivieren">Spiel aktivieren</p></span></button></div></div><div class=""></div></div></div></div></div></div></div>
        //Card English:<div aria-label="Tandem: A Tale of Shadows" class="item-card__action" tabindex="0" data-a-target="claim-prime-offer-card"><div class="tw-relative"><div data-a-target="item-card" class="tw-card tw-relative"><div class="tw-flex tw-flex-column tw-flex-nowrap"><div class="tw-absolute tw-left-0 tw-top-0"><div data-a-target="item-card-badges" class="item-card-badges"><div class="sm-badge-group tw-align-items-center tw-flex tw-z-default"><div class="sm-badge--first" style="z-index: 2;"><div data-a-target="badge-prime" class="sm-badge sm-badge--none sm-badge--prime tw-align-items-center tw-border-bottom-left-radius-none tw-border-bottom-right-radius-bubble tw-border-top-left-radius-extra-extra-large tw-border-top-right-radius-bubble tw-flex tw-justify-content-center tw-pd-x-1 tw-relative"><figure aria-label="LogoPrimeNoSmile" class="tw-svg"><svg class="tw-svg__asset tw-svg__asset--logoprimenosmile tw-svg__asset--inherit" width="35px" height="21px" version="1.1" viewBox="0 0 149 55" x="0px" y="0px"><path fill-rule="evenodd" d="M14.573 37.109c2.122 0 3.686-.72 4.691-2.16 1.004-1.44 1.507-3.715 1.507-6.823 0-3.145-.493-5.438-1.479-6.879-.986-1.44-2.558-2.16-4.719-2.16-1.97 0-3.829.511-5.571 1.535v14.952c1.667 1.023 3.525 1.535 5.571 1.535zm-12.45 17.34c-.493 0-.853-.105-1.081-.313-.226-.208-.341-.579-.341-1.109v-37.92c0-.531.115-.899.341-1.108.228-.208.588-.313 1.081-.313h4.093c.872 0 1.402.417 1.592 1.25l.398 1.478c1.137-1.098 2.51-1.97 4.121-2.614a13.148 13.148 0 014.918-.967c3.676 0 6.585 1.345 8.727 4.037 2.142 2.691 3.213 6.33 3.213 10.915 0 3.146-.531 5.894-1.592 8.244-1.061 2.351-2.502 4.16-4.321 5.429-1.82 1.27-3.905 1.905-6.254 1.905-1.555 0-3.032-.247-4.435-.739-1.402-.493-2.595-1.175-3.581-2.047v12.45c0 .53-.105.901-.312 1.109-.209.208-.579.313-1.11.313H2.123zm34.049-11.712c-.493 0-.853-.113-1.081-.341-.227-.228-.341-.587-.341-1.08V15.107c0-.531.114-.899.341-1.108.228-.208.588-.313 1.081-.313h4.093c.872 0 1.402.417 1.592 1.25l.739 3.07c1.516-1.667 2.946-2.851 4.292-3.552a9.16 9.16 0 014.293-1.052h.796c.53 0 .909.104 1.136.312.228.208.341.579.341 1.109v4.775c0 .494-.104.853-.312 1.08-.209.228-.578.342-1.109.342-.265 0-.606-.019-1.023-.057a18.554 18.554 0 00-1.592-.057c-.872 0-1.932.124-3.183.37s-2.313.559-3.184.937v19.103c0 .493-.105.852-.312 1.08-.209.228-.58.341-1.11.341h-5.457m22.717 0c-.492 0-.853-.113-1.08-.341-.228-.228-.341-.587-.341-1.08V15.107c0-.531.113-.899.341-1.108.227-.208.588-.313 1.08-.313h5.458c.53 0 .9.105 1.108.313.208.209.314.577.314 1.108v26.209c0 .493-.106.852-.314 1.08-.208.228-.578.341-1.108.341h-5.458zm2.729-33.542c-1.441 0-2.596-.398-3.467-1.194-.873-.796-1.308-1.876-1.308-3.241 0-1.365.435-2.445 1.308-3.241.871-.796 2.026-1.193 3.467-1.193 1.44 0 2.596.397 3.468 1.193.871.796 1.308 1.876 1.308 3.241 0 1.365-.437 2.445-1.308 3.241-.872.796-2.028 1.194-3.468 1.194zm12.611 33.542c-.494 0-.854-.113-1.081-.341-.227-.228-.341-.587-.341-1.08V15.107c0-.531.114-.899.341-1.108.227-.208.587-.313 1.081-.313h4.093c.872 0 1.402.417 1.591 1.25l.455 1.535c2.009-1.326 3.819-2.263 5.43-2.813 1.61-.55 3.269-.825 4.975-.825 3.411 0 5.817 1.214 7.219 3.638 1.933-1.288 3.752-2.216 5.458-2.785a16.596 16.596 0 015.288-.853c2.652 0 4.708.739 6.167 2.218 1.459 1.477 2.19 3.543 2.19 6.196v20.069c0 .493-.106.852-.313 1.08-.209.228-.579.341-1.109.341h-5.457c-.493 0-.853-.113-1.08-.341-.228-.228-.342-.587-.342-1.08v-18.25c0-2.577-1.156-3.865-3.468-3.865-2.047 0-4.113.492-6.197 1.477v20.638c0 .493-.104.852-.312 1.08-.209.228-.578.341-1.109.341H92.25c-.493 0-.852-.113-1.08-.341-.227-.228-.341-.587-.341-1.08v-18.25c0-2.577-1.157-3.865-3.467-3.865-2.123 0-4.207.512-6.254 1.534v20.581c0 .493-.106.852-.313 1.08-.209.228-.579.341-1.109.341h-5.457m60.717-16.998c1.932 0 3.334-.294 4.207-.882.871-.587 1.307-1.487 1.307-2.7 0-2.388-1.421-3.582-4.264-3.582-3.638 0-5.742 2.237-6.31 6.709 1.516.304 3.202.455 5.06.455zm1.875 17.85c-4.7 0-8.309-1.287-10.83-3.865-2.521-2.577-3.78-6.272-3.78-11.086 0-4.926 1.287-8.793 3.865-11.599 2.578-2.803 6.16-4.206 10.745-4.206 3.526 0 6.283.853 8.273 2.558 1.989 1.706 2.984 3.98 2.984 6.822 0 2.843-1.071 4.995-3.212 6.454-2.141 1.459-5.296 2.188-9.465 2.188-2.161 0-4.038-.208-5.629-.625.227 2.539.995 4.358 2.302 5.457 1.308 1.101 3.289 1.65 5.942 1.65 1.06 0 2.094-.067 3.098-.199 1.003-.133 2.397-.428 4.178-.882a2.248 2.248 0 01.626-.114c.644 0 .966.437.966 1.308v2.615c0 .607-.085 1.033-.255 1.279-.171.248-.503.465-.995.653-2.768 1.062-5.705 1.592-8.813 1.592z"></path></svg></figure></div></div><div class="sm-badge--cascade" style="z-index: 1;"><div data-a-target="badge-fgwp" class="sm-badge sm-badge--free-game sm-badge--none tw-align-items-center tw-border-bottom-left-radius-none tw-border-bottom-right-radius-bubble tw-border-top-left-radius-none tw-border-top-right-radius-bubble tw-flex tw-justify-content-center tw-pd-x-1 tw-relative"><p class="tw-font-size-6" title="free game">free game</p></div></div></div></div></div><div data-a-target="card-image" class="tw-border-top-left-radius-extra-extra-large tw-border-top-right-radius-extra-extra-large tw-card-img tw-flex-shrink-0 tw-overflow-hidden"><figure class="tw-aspect tw-aspect--16x9 tw-aspect--align-top"><picture class="tw-picture"><img class="tw-image" alt="Tandem: A Tale of Shadows" srcset="https://m.media-amazon.com/images/I/41ayiPEttyL._FMwebp_.jpg 1x,https://m.media-amazon.com/images/I/41ayiPEttyL._FMwebp_.jpg 2x" src="https://m.media-amazon.com/images/I/41ayiPEttyL._FMwebp_.jpg"></picture></figure></div><div class="item-card__availability-callout__container item-card__availability-callout__container--free-game tw-c-background-free-game"><div data-a-target="item-card-availability-callout" class="item-card__availability-callout tw-align-items-center tw-c-background-elevation-2 tw-flex tw-flex-wrap"><div class="tw-align-items-center tw-flex tw-flex-wrap tw-md-pd-x-3 tw-md-pd-y-1 tw-pd-x-2 tw-pd-y-1"><div class="tw-block tw-mg-r-1"><div class="item-card__availability-date"><p class="tw-c-text-white tw-font-size-7"><span class="tw-amazon-ember-regular tw-c-text-base tw-font-size-7" title="Ends in">Ends in</span> 13 days</p></div></div></div></div></div><div class="item-card-details item-card-details--free-game tw-border-bottom-left-radius-extra-extra-large tw-border-bottom-right-radius-extra-extra-large tw-c-background-free-game tw-flex tw-flex-column tw-justify-content-between tw-md-pd-x-3 tw-pd-b-3 tw-pd-t-1 tw-pd-x-2"><div class="item-card-details__body tw-flex tw-flex-column tw-flex-grow-1 tw-justify-content-center"><div class="item-card-details__body__primary"><h3 class="tw-amazon-ember-bold tw-c-text-white tw-font-size-6 tw-md-font-size-5 tw-strong" title="Tandem: A Tale of Shadows">Tandem: A Tale of Shadows</h3></div></div><div class="item-card-details__footer tw-align-items-center tw-flex tw-justify-content-between"><div class="tw-pd-r-05"><div class="item-card__claim-button"><button aria-label="Claim game Tandem: A Tale of Shadows" class="tw-interactive tw-button" data-a-target="FGWPOffer"><span class="tw-button__text" data-a-target="tw-button-text"><p class="tw-pd-x-05 tw-pd-y-05" title="Claim game">Claim game</p></span></button></div></div><div class=""></div></div></div></div></div></div></div>

        public async Task<List<BargainFreeGames>> NameHarvester()
        {
            string url = "https://gaming.amazon.com/home";
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            IBrowser browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            page.DefaultTimeout = 0; // or you can set this as 0
            await page.GoToAsync(url, WaitUntilNavigation.Networkidle2);
            var content = await page.GetContentAsync();
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(content);
            HtmlNodeCollection AvailableGames = document.DocumentNode.SelectNodes("//div[contains(@data-a-target, 'claim-prime-offer-card')]");
            HtmlNodeCollection AvailableDates = document.DocumentNode.SelectNodes("//div[contains(@class, 'item-card__availability-date')]");
            await browser.CloseAsync();
            List<BargainFreeGames> GTBR = new List<BargainFreeGames>();
            foreach (HtmlNode Item in AvailableGames)
            {
                int startIndex = Item.OuterHtml.ToString().IndexOf("<div aria-label=\"");
                int gameIndex = startIndex + 17;
                string Days = Item.InnerText;
                StringBuilder ExpirationDate = new StringBuilder();
                for (int i = 0; i < Days.Length; i++)
                {
                    if (48 <= Days[i] && Days[i] <= 57)
                    {
                        ExpirationDate.Append(Days[i]);
                    }
                    if (ExpirationDate.Length >= 3)
                        break;
                }
                ExpirationDate.Append(" days left");
                string GameNameText = Item.OuterHtml.ToString();
                StringBuilder GameName = new StringBuilder();
                while (GameNameText[gameIndex] != '\"')
                {
                    GameName.Append(GameNameText[gameIndex]);
                    gameIndex++;
                }
                GTBR.Add(new BargainFreeGames { Name = GameName.ToString(), TimeEnd = ExpirationDate.ToString(), Website = "Prime Gaming" });
                GameName.Clear();
                ExpirationDate.Clear();
            }

            return GTBR;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Controllers;

namespace backend.Helper
{
    public class GOGHelper
    {

        string OverCapacityCheckURL = "https://static.gog.com/www/default/-img/overcapacity.jpg";

        public string FindUserID(string response)
        {
            int begin = response.IndexOf("gog-user=") + 2;
            int end = begin + 200; //Extra padded amount for future proofing so 
                                   //increase in UserID's can be accounted for
            StringBuilder BasicWishListBuilder = new StringBuilder();
            StringBuilder ClientID = new StringBuilder();

            for (int i = begin; i < end; i++)
            {
                BasicWishListBuilder.Append(response[i]);
            }
            begin = BasicWishListBuilder.ToString().IndexOf('"') + 1;
            while (BasicWishListBuilder[begin] != '"')
            {
                ClientID.Append(BasicWishListBuilder[begin]);
                begin++;
            }

            return ClientID.ToString();
        }

        public bool CheckNotOverCapacity(string response)
        {
            int begin = response.IndexOf(OverCapacityCheckURL);
            if (begin > 0)
            {
                return false;
            }
            return true;
        }

        public List<GOGWishlist> TitleHarvesterForScraping(string response)
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

        public List<GOGWishlist> PriceHarvesterForScraping(string response, List<GOGWishlist> HarvestedTitles)
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
                //Find price variables
                int startIndex = BasicWishListBuilder.ToString().IndexOf("\"amount\":");
                int gameIndex = startIndex + 10;
                StringBuilder PriceBuilder = new StringBuilder();
                bool ProcessFurther = true;
                //Find isTBA variables
                int startTBA = BasicWishListBuilder.ToString().IndexOf("\"isTBA\":");
                int TBAindex = startTBA + 8;
                //Start processing TBA
                StringBuilder TBACheck = new StringBuilder();
                while (BasicWishListBuilder[TBAindex] != ',')
                {
                    TBACheck.Append(BasicWishListBuilder[TBAindex]);
                    TBAindex++;
                }
                string TBAtest = TBACheck.ToString();
                if (TBAtest == "true")
                {
                    PriceBuilder.Append("TBA");
                    ProcessFurther = false;
                }
                //Start Processing Price
                while (BasicWishListBuilder[gameIndex] != '"' && ProcessFurther)
                {
                    PriceBuilder.Append(BasicWishListBuilder[gameIndex]);
                    gameIndex++;
                }
                //Finally add our price to list
                ItemsToAdd.Add(PriceBuilder.ToString());
                BasicWishListBuilder.Remove(startIndex, 8);
                BasicWishListBuilder.Remove(startTBA, 6);

                PriceBuilder.Clear();
                TBACheck.Clear();
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

        public List<GOGWishlist> TitleHarvesterForJSON(string response)
        {
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
            return GTBR;
        }

        public List<GOGWishlist> PriceHarvesterForJSON(string response, List<GOGWishlist> HarvestedTitles)
        {
            int begin = response.IndexOf("{");
            int end = response.LastIndexOf("}");//End with some to spare that's why we subtract the 4.
            StringBuilder BasicWishListBuilder = new StringBuilder();
            List<string> ItemsToAdd = new List<string>();
            List<GOGWishlist> GTBR = new List<GOGWishlist>();

            for (int i = begin; i < end; i++)
            {
                BasicWishListBuilder.Append(response[i]);
            }

            while (BasicWishListBuilder.ToString().Contains("\"amount\":"))
            {
                //Find price variables
                int startIndex = BasicWishListBuilder.ToString().IndexOf("\"amount\":");
                int gameIndex = startIndex + 10;
                StringBuilder PriceBuilder = new StringBuilder();
                bool ProcessFurther = true;
                //Find isTBA variables
                int startTBA = BasicWishListBuilder.ToString().IndexOf("\"isTBA\":");
                int TBAindex = startTBA + 8;
                //Start processing TBA
                StringBuilder TBACheck = new StringBuilder();
                while (BasicWishListBuilder[TBAindex] != ',')
                {
                    TBACheck.Append(BasicWishListBuilder[TBAindex]);
                    TBAindex++;
                }
                string TBAtest = TBACheck.ToString();
                if (TBAtest == "true")
                {
                    PriceBuilder.Append("TBA");
                    ProcessFurther = false;
                }
                //Start Processing Price
                while (BasicWishListBuilder[gameIndex] != '"' && ProcessFurther)
                {
                    PriceBuilder.Append(BasicWishListBuilder[gameIndex]);
                    gameIndex++;
                }
                //Finally add our price to list
                ItemsToAdd.Add(PriceBuilder.ToString());
                BasicWishListBuilder.Remove(startIndex, 8);
                BasicWishListBuilder.Remove(startTBA, 6);

                PriceBuilder.Clear();
                TBACheck.Clear();
            }

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
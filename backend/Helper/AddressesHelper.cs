using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Controllers;
using Controllers.Entities;
using Newtonsoft.Json;

namespace backend.Helper
{
    public class AddressHelper
    {
        public string GetTargetAddress(string name)
        {
            var seedData = System.IO.File.ReadAllText("../frontend/src/Addresses.json");
            var AddressItems = JsonConvert.DeserializeObject<List<Address>>(seedData);
            string Value = "";
            for (int i = 0; i < AddressItems.Count; i++)
            {
                if (AddressItems[i].Name == name)
                {
                    Value = AddressItems[i].Value;
                    break;
                }
            }
            return Value;
        }
    }
}
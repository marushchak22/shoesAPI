using System.Collections.Generic;
using Newtonsoft.Json;

namespace shoesAPI.Models
{


    public class SneakersModel
    {
       
        public string shoeName { get; set; }
        public string brand { get; set; }
        public string colorway { get; set; }
        public string styleID { get; set; }
        public string thumbnail { get; set; }
        public lowestResellPrice lowestResellPrice { get; set; }

    }

    public class lowestResellPrice
    {
        public double stockX { get; set; }
        public double goat { get; set; }
        public double flightClub { get; set; }
    }
}





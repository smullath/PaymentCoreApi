using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCoreApi.Model
{
    public class payment
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "cardownername")]
        public string CardOwnerName { get; set; }

        [JsonProperty(PropertyName = "cardnumber")]
        public string CardNumber { get; set; }

        [JsonProperty(PropertyName = "expirationdate")]
        public string ExpDate { get; set; }

        [JsonProperty(PropertyName = "seccode")]
        public string SecurityCode { get; set; }

        //[JsonProperty(PropertyName = "isComplete")]
        //public bool Completed { get; set; }
    }
}


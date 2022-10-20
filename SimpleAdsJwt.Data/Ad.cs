using System;
using System.Text.Json.Serialization;

namespace SimpleAdsJwt.Data
{
    public class Ad
    {
        public int Id { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string PhoneNumber { get; set; }
        public string Text { get; set; }
   
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}

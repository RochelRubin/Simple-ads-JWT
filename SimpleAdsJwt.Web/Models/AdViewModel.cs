using SimpleAdsJwt.Data;

namespace SimpleAdsJwt.Web.Models
{
    public class AdViewModel
    {
        public Ad Ad { get; set; }
        public bool CanDelete { get; set; }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SimpleAdsJwt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAdsJwt.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdController : ControllerBase
    {
        private string _connectionString;
        

        public AdController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
          
        }
        [HttpGet]
        [Route("getads")]
        public List<Ad> GetAds()
        {
            var repo = new AdRepo(_connectionString);
            return repo.GetAds();
        }
        [HttpPost]
        [Authorize]
        [Route("addad")]
        public void AddAd(Ad ad)
        {
            var repo = new AdRepo(_connectionString);
            var email = User.FindFirst("user")?.Value;
            var user = repo.GetByEmail(email);
            
            ad.UserId = user.Id;
            ad.DateSubmitted = DateTime.Now;
            repo.AddAd(ad);
        }
        [HttpPost]
        [Authorize]
        [Route("deletead")]
        public void DeleteAd(int id)
        {
            var repo = new AdRepo(_connectionString);
            repo.Delete(id);
        }
        [HttpGet]
        [Route("myaccount")]
        public List<Ad> GetAdsForUser(int id)
        {
            var repo = new AdRepo(_connectionString);
            return repo.GetAds(id);
        }
    }
}

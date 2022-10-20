using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAdsJwt.Data
{
    public class AdRepo
    {
        private readonly string _connectionString;

        public AdRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Delete(int id)
        {
            using var ctx = new SimpleAdDataContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"delete from ads where id={id}");
         
        }
        public List<Ad> GetAds()
        {
            using var ctx = new SimpleAdDataContext(_connectionString);
            return ctx.Ads.Include(a=>a.User).OrderByDescending(a => a.DateSubmitted).ToList();
        }
        public void AddAd(Ad ad)
        {
            using var ctx = new SimpleAdDataContext(_connectionString);
          
            ctx.Ads.Add(ad);
            ctx.SaveChanges();
        }
        public List<Ad>GetAds(int id)
        {
            using var ctx = new SimpleAdDataContext(_connectionString);
            return ctx.Ads.Where(c => c.UserId == id).OrderByDescending(a => a.DateSubmitted).ToList();
        }

        public void AddUser(User user, string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = hash;
            using var ctx = new SimpleAdDataContext(_connectionString);
            ctx.Users.Add(user);
            ctx.SaveChanges();
        }

        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            var isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isValidPassword)
            {
                return null;
            }

            return user;

        }

        public User GetByEmail(string email)
        {
            using var ctx = new SimpleAdDataContext(_connectionString);
            return ctx.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}


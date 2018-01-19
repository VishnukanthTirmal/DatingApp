using System.Collections.Generic;
using Datingapp.API.Data;
using Datingapp.API.Models;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context= context;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
           using (var hmac=new System.Security.Cryptography.HMACSHA512())
           {
               passwordSalt=hmac.Key;
               passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

           }

        }
        public void SeedUsers()
        {
            //Delete existing users
            _context.Users.RemoveRange(_context.Users);
            _context.SaveChanges();

            //Seed Users
            var userData= System.IO.File.ReadAllText("Data/UserseedData.json");
            var users= JsonConvert.DeserializeObject<List<User>>(userData);
            foreach (var  user  in users)
            {
                byte[] passwordHash, passowrdSalt;
                CreatePasswordHash("password",out passwordHash, out passowrdSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passowrdSalt;
                user.UserName= user.UserName.ToLower();
                _context.Users.Add(user);

            }
            _context.SaveChanges();

        }
       
    }
}
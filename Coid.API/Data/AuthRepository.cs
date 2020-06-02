using System;
using System.Threading.Tasks;
using Coid.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Coid.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ContextData context;

        public AuthRepository(ContextData context){
            this.context = context;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await context.Users.Include(u=> u.Photos).FirstOrDefaultAsync(u=>u.Username == username.ToLower());
            if(user != null){
                if(!VerifyPasswordHash(password, user.passwordHash, user.passwordSalt))
                    user = null;
            }
            return user; 
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            bool isValid = true;
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)){
                var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < passwordHash.Length; i++){
                    if(passwordHash[i] != ComputeHash[i])
                        isValid = false;
                }
            }
            return isValid;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreateHashPassword(password, out passwordHash, out passwordSalt);
            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        private void CreateHashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            bool isExist = false;
            if(await context.Users.AnyAsync(u => u.Username == username))
                isExist = true;
            return isExist;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coid.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Coid.API.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly ContextData context;

        public DataRepository(ContextData context){
            this.context = context;
        }
        public async Task Add<T>(T obj) where T : class
        {
            await context.AddAsync<T>(obj);
        }

        public void Remove<T>(T obj) where T : class
        {
            context.Remove<T>(obj);

        }

        public async Task<Photo> GetPhoto(int id)
        {
            return await context.Photos.FindAsync(id);
        }

        public async Task<IEnumerable<Photo>> GetPhotos()
        {
            return await context.Photos.ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await context.Users.FindAsync(id);
            
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<Vidoe> GetVidoe(int id)
        {
            return await context.Vidoes.FindAsync(id);
        }

        public async Task<IEnumerable<Vidoe>> GetVidoes()
        {
            return await context.Vidoes.ToListAsync();
        }

        public async Task<bool> Save()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AnyCoron()
        {
           return await context.Corons.AnyAsync(c => c.Date.Equals(DateTime.Now));
        }

        public async Task<IEnumerable<Coron>> GetCorons()
        {
            return await context.Corons.OrderBy(p=>p.Date).ToListAsync();
        }
    }
}
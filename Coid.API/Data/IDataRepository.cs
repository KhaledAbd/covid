using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coid.API.Models;

namespace Coid.API.Data
{
    public interface IDataRepository
    {
        Task Add<T>(T obj) where T : class;

        void Remove<T>(T obj) where T : class;

        Task<bool> Save();

        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<IEnumerable<Photo>> GetPhotos();
        Task<Photo> GetPhoto(int id);

        Task<IEnumerable<Vidoe>> GetVidoes();

        Task<Vidoe> GetVidoe(int id);

        Task<bool> AnyCoron();

        Task<IEnumerable<Coron>> GetCorons();
    }
}
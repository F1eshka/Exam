using EkzamenADO.Models;
using System.Collections.Generic;

namespace EkzamenADO.DataAccess
{
    public interface IDbManager
    {
        bool RegisterUser(User user, string password);
        User? Login(string email, string password);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        List<AdWithCategory> GetAdsByUser(int userId);
        public void AddAd(Ad ad);

    }
}

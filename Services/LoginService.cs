using EkzamenADO.DataAccess;
using EkzamenADO.Models;

namespace EkzamenADO.Services
{
    public class LoginService : ILoginService
    {
        private readonly IDbManager _db;

        public LoginService(IDbManager db)
        {
            _db = db;
        }

        public User? Login(string email, string password)
        {
            return _db.Login(email, password);
        }

        public bool Register(string email, string password)
        {
            var user = new User
            {
                Name = "Користувач",
                Email = email,
                Phone = "0000000000"
            };

            return _db.RegisterUser(user, password);
        }
    }
}

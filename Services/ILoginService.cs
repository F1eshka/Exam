using EkzamenADO.Models;

namespace EkzamenADO.Services
{
    public interface ILoginService
    {
        User? Login(string email, string password);
        bool Register(string email, string password);
    }
}

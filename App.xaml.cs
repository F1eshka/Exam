using System.Windows;
using EkzamenADO.DataAccess;
using EkzamenADO.Services;

namespace EkzamenADO
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IDbManager dbManager = new DbManager(); 
            ILoginService loginService = new LoginService(dbManager);

            var loginWindow = new LoginWindow(loginService);
            loginWindow.Show();
        }
    }
}

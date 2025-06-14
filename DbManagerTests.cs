using EkzamenADO.DataAccess;
using EkzamenADO.Models;
using System.Collections.Generic;
using Xunit;

namespace EkzamenADO.Tests
{
    public class DbManagerTests
    {
        [Fact]
        public void GetAdsByUser_ShouldReturn_List()
        {
            var connectionString = "Data Source=DESKTOP-Q4ID39U\\SQLEXPRESS;Initial Catalog=EkzamenADO;Integrated Security=True";

            var db = new DbManager(connectionString);

            int testUserId = 1; 

            // Act
            List<AdWithCategory> ads = db.GetAdsByUser(testUserId);

            // Assert
            Assert.NotNull(ads); // Список должен быть создан
            Assert.True(ads.Count >= 0, "Список может быть пустым, но не должен быть null");
        }
    }
}

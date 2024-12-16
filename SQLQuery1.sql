INSERT INTO Status (Title) VALUES ('В ожидании'), ('В работе'), ('Выполнено');

INSERT INTO Client (FirstName, LastName, PhoneNumber)
VALUES 
    ('Иван', 'Иванов', '89001112233'),
    ('Петр', 'Петров', '89004445566'),
    ('Сергей', 'Сергеев', '89007778899');


INSERT INTO Executor (FirstName, LastName, PhoneNumber)
VALUES 
    ('Алексей', 'Алексеев', '89001122334'),
    ('Дмитрий', 'Дмитриев', '89002233445'),
    ('Евгений', 'Евгеньев', '89003344556');


SELECT RequestId, RequstNumber, Equipment, Type, Description, CreatedDate
FROM Request
WHERE StatusId = 1 
ORDER BY CreatedDate DESC;

using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskRequestDem.Data;
using TaskRequestDem.Model;
using TaskRequestDem.Pages.RequestRazor;
using Xunit;
using System.Linq;
using System.Threading.Tasks;

namespace TaskRequestDem.Tests
{
    public class IndexModelTests
    {
        [Fact]
        public async Task OnGetAsync_ShouldReturnRequestList_WhenDataIsAvailable()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            // Создаем контекст базы данных
            using (var context = new AppDbContext(options))
            {
                // Добавляем тестовые данные
                context.Requests.Add(new Request
                {
                    RequestId = 1,
                    RequstNumber = 123,
                    Equipment = "Станок",
                    Type = "Неисправность",
                    Description = "Не работает",
                    ClientId = 1,
                    StatusId = 1,
                });
                await context.SaveChangesAsync();
            }

            // Создаем модель страницы с использованием контекста
            using (var context = new AppDbContext(options))
            {
                var pageModel = new IndexModel(context);

                // Act
                await pageModel.OnGetAsync();

                // Assert
                // Проверяем, что количество заявок больше 0
                Assert.NotEmpty(pageModel.Request);
                Assert.Equal(1, pageModel.Request.Count);
                Assert.Equal("Станок", pageModel.Request.First().Equipment);
            }
        }

        [Fact]
        public async Task OnGetAsync_ShouldReturnEmptyList_WhenNoDataIsAvailable()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDbEmpty")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var pageModel = new IndexModel(context);

                // Act
                await pageModel.OnGetAsync();

                // Assert
                Assert.Empty(pageModel.Request);
            }
        }
    }
}

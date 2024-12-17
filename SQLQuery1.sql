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

dotnet add package xunit
dotnet add package Moq
dotnet add package Microsoft.AspNetCore.Mvc.RazorPages

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using TaskRequestDem.Data;
using TaskRequestDem.Model;
using TaskRequestDem.Pages.RequestRazor;
using Xunit;

namespace TaskRequestDem.Tests
{
    public class DeleteModelTests
    {
        [Fact]
        public async Task OnGetAsync_ReturnsNotFound_WhenIdIsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            var context = new AppDbContext(options);
            var pageModel = new DeleteModel(context);

            // Act
            var result = await pageModel.OnGetAsync(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task OnGetAsync_ReturnsNotFound_WhenRequestNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            var context = new AppDbContext(options);
            var pageModel = new DeleteModel(context);

            // Act
            var result = await pageModel.OnGetAsync(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task OnGetAsync_ReturnsPage_WhenRequestExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            var context = new AppDbContext(options);
            context.Requests.Add(new Request { RequestId = 1 });
            await context.SaveChangesAsync();

            var pageModel = new DeleteModel(context);

            // Act
            var result = await pageModel.OnGetAsync(1);

            // Assert
            Assert.IsType<PageResult>(result);
            Assert.Equal(1, pageModel.Request.RequestId);
        }

        [Fact]
        public async Task OnPostAsync_DeletesRequest_WhenRequestExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            var context = new AppDbContext(options);
            context.Requests.Add(new Request { RequestId = 1 });
            await context.SaveChangesAsync();

            var pageModel = new DeleteModel(context);

            // Act
            var result = await pageModel.OnPostAsync(1);

            // Assert
            Assert.IsType<RedirectToPageResult>(result);
            Assert.Null(await context.Requests.FindAsync(1));
        }

        [Fact]
        public async Task OnPostAsync_ReturnsNotFound_WhenIdIsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            var context = new AppDbContext(options);
            var pageModel = new DeleteModel(context);

            // Act
            var result = await pageModel.OnPostAsync(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}

Delails
public class DetailsModelTests
{
    [Fact]
    public async Task OnGetAsync_ReturnsNotFound_WhenIdIsNull()
    {
        var context = new Mock<AppDbContext>();
        var model = new DetailsModel(context.Object);

        var result = await model.OnGetAsync(null);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsPage_WhenRequestExists()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        using var context = new AppDbContext(options);
        context.Requests.Add(new Request { RequestId = 1, Title = "TestRequest" });
        await context.SaveChangesAsync();

        var model = new DetailsModel(context);

        var result = await model.OnGetAsync(1);

        Assert.IsType<PageResult>(result);
        Assert.Equal("TestRequest", model.Request.Title);
    }
}


edit
public class EditModelTests
{
    [Fact]
    public async Task OnPostAsync_ReturnsNotFound_WhenRequestDoesNotExist()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        using var context = new AppDbContext(options);
        var model = new EditModel(context)
        {
            Request = new Request { RequestId = 99 }
        };

        var result = await model.OnPostAsync();

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task OnPostAsync_SavesChanges_WhenRequestIsValid()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        using var context = new AppDbContext(options);
        context.Requests.Add(new Request { RequestId = 1, Title = "OldTitle" });
        await context.SaveChangesAsync();

        var model = new EditModel(context)
        {
            Request = new Request { RequestId = 1, Title = "NewTitle" }
        };

        var result = await model.OnPostAsync();

        Assert.IsType<RedirectToPageResult>(result);
        var updatedRequest = await context.Requests.FindAsync(1);
        Assert.Equal("NewTitle", updatedRequest.Title);
    }
}


Create
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskRequestDem.Data;
using TaskRequestDem.Model;
using TaskRequestDem.Pages.RequestRazor;
using Xunit;

namespace TaskRequestDem.Tests
{
    public class CreateModelTests
    {
        /// <summary>
        /// Проверяет, что метод OnGet корректно возвращает PageResult.
        /// </summary>
        [Fact]
        public void OnGet_ReturnsPageResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            var context = new AppDbContext(options);
            var pageModel = new CreateModel(context);

            // Act
            var result = pageModel.OnGet();

            // Assert
            Assert.IsType<PageResult>(result);
        }

        /// <summary>
        /// Проверяет, что OnPostAsync успешно добавляет новую заявку и перенаправляет на Index.
        /// </summary>
        [Fact]
        public async Task OnPostAsync_AddsRequestAndRedirects()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            using var context = new AppDbContext(options);
            var pageModel = new CreateModel(context)
            {
                Request = new Request
                {
                    RequestId = 1,
                    Title = "New Task",
                    Description = "Test Description"
                }
            };

            // Act
            var result = await pageModel.OnPostAsync();

            // Assert
            // Проверка на перенаправление.
            var redirectResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("./Index", redirectResult.PageName);

            // Проверка, что заявка была добавлена в базу данных.
            var request = await context.Requests.FirstOrDefaultAsync(r => r.RequestId == 1);
            Assert.NotNull(request);
            Assert.Equal("New Task", request.Title);
        }

        /// <summary>
        /// Проверяет, что OnPostAsync не падает при пустой модели Request.
        /// </summary>
        [Fact]
        public async Task OnPostAsync_ReturnsPageResult_WhenRequestIsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            using var context = new AppDbContext(options);
            var pageModel = new CreateModel(context)
            {
                Request = null // Симуляция пустой модели.
            };

            // Act
            var result = await pageModel.OnPostAsync();

            // Assert
            Assert.IsType<PageResult>(result);
        }
    }
}



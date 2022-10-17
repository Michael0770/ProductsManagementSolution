using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProductsApi;
using ProductsApi.Controllers;
using ProductsApi.Models;
using System.Web.Mvc;

namespace ProductApi.Tests
{
    [TestClass]
    public class ProductControllerTest
    {
        private ProductDbContext context;

        [TestInitialize]
        public void Init()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseInMemoryDatabase(databaseName: "ProductTestDatabase" + Guid.NewGuid().ToString())
            .Options;
            context = new ProductDbContext(options);
            
            context.Products.Add(new Product() { Id = 2, Name = "PB", Price = 23.05, IsActive = false, Type = ProductType.Books });
            context.Products.Add(new Product() { Id = 3, Name = "PC", Price = 12, IsActive = true, Type = ProductType.Furniture });
            context.Products.Add(new Product() { Id = 1, Name = "PA", Price = 123.45, IsActive = true, Type = ProductType.Furniture });

            context.SaveChanges();
        }

        [TestMethod]
        public void GetAllSuccessTest()
        {
            ProductController controller = new ProductController(context);
            var results = controller.Get();

            Assert.IsTrue(results.Count() == 3);
        }

        [TestMethod]
        public async Task GetByIdSuccessTest()
        {
            ProductController controller = new ProductController(context);
            var result = await controller.GetById(2);


            Assert.IsTrue(result is(OkObjectResult) && ((result as OkObjectResult).Value as Product).Name == "PB");
        }

        [TestMethod]
        public void PostSuccessTest()
        {
            ProductController controller = new ProductController(context);
            var result = controller.Post(new Product() { Id = 4, Name = "PD", Price = 32.12, IsActive = false, Type = ProductType.Furniture });

            var results = controller.Get();

            Assert.IsTrue(results.Count() == 4);
        }

        [TestMethod]
        public async Task PutSuccessTest()
        {
            ProductController controller = new ProductController(context);
            await controller.Put(1, new Product() { Name = "PA UPDATE", Price = 32.12, IsActive = false, Type = ProductType.Furniture });

            var results = controller.Get();

            Assert.IsTrue(results.First().Name == "PA UPDATE");
        }

        [TestMethod]
        public async Task DeleteSuccessTest()
        {
            ProductController controller = new ProductController(context);
            await controller.Delete(3);

            var results = controller.Get();

            Assert.IsTrue(results.Count() == 2);
        }

        [TestMethod]
        public async Task DeleteInvalidIdTest()
        {
            ProductController controller = new ProductController(context);
            var result = await controller.Delete(10);

            Assert.IsTrue(result is(Microsoft.AspNetCore.Mvc.NotFoundResult));
        }

        [TestMethod]
        public async Task PutInvalidIdTest()
        {
            ProductController controller = new ProductController(context);
            var result = await controller.Put(99, new Product() { Name = "PA UPDATE", Price = 32.12, IsActive = false, Type = ProductType.Furniture });

            Assert.IsTrue(result is (Microsoft.AspNetCore.Mvc.NotFoundResult));
        }

        [TestMethod]
        public async Task GetInvalidIdTest()
        {
            ProductController controller = new ProductController(context);
            var result = await controller.GetById(99);

            Assert.IsTrue(result is (Microsoft.AspNetCore.Mvc.NotFoundResult));
        }

        [TestMethod]
        public async Task GetSortTest()
        {
            ProductController controller = new ProductController(context);
            var results = controller.Get();

            Assert.IsTrue(results.ElementAt(0).Name == context.Products.ToList().ElementAt(2).Name);
        }
    }
}
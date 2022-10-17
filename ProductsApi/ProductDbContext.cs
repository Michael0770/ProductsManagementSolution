using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;
using System;

namespace ProductsApi
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductDbContext(DbContextOptions<ProductDbContext> options)
           : base(options)
        {
        }
    }
}

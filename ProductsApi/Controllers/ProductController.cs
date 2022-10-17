using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public ProductController(ProductDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _context.Products.OrderBy(P=>P.Name);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);

            if(product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task Post(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, Product product)
        {
            var oldProduct = _context.Products.SingleOrDefault(p => p.Id == id);

            if(oldProduct != null)
            {
                oldProduct.Name = product.Name;
                oldProduct.Price = product.Price;
                oldProduct.IsActive = product.IsActive;
                oldProduct.Type = product.Type;

                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var product = _context.Products.Find(id);

            if (product != null)
            {
                _context.Products.Remove(product);

                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}

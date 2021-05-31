using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ecommerce.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
using Microsoft.AspNetCore.Hosting;

namespace Ecommerce.API.Controllers
{
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [Route("api/Product/List")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var List = await _db.Product.ToListAsync();
                return Ok(List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("api/Product/GetId")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                ProductViewModel product = await _db.Product.FirstOrDefaultAsync(x => x.ProductId == id);
                if (product == null)
                {
                    return BadRequest();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("api/Product/Create")]
        public async Task<IActionResult> Add([FromBody]ProductModel model)
        {
            try
            {
                ProductViewModel Data = new ProductViewModel()
                {
                    ProductId = Guid.NewGuid().ToString(),
                    ProductName = model.ProductName,
                    ProductPrice = model.ProductPrice,
                    ProductCategory = model.ProductCategory,
                    ProductImage = GetImage(model.ProductImage),
                    ProductPublishDate = model.ProductPublishDate,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                _db.Product.Add(Data);
                await _db.SaveChangesAsync();
                return Ok(new { Message = "Product is Created", Product = Data });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        string Filename;
        private string GetImage(string Image)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            Image = regex.Replace(Image, string.Empty);
            byte[] Files = Convert.FromBase64String(Image);
            string webRootPath = _hostingEnvironment.WebRootPath;
            string path = webRootPath + "/ImageStorage";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            var data = Image.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    Filename = Guid.NewGuid().ToString() + ".png";
                    break;
                case "/9J/4":
                    Filename = Guid.NewGuid().ToString() + ".jpg";
                    break;
            }
            string imgPath = Path.Combine(path, Filename);
            System.IO.File.WriteAllBytes(imgPath, Files);
            string Images = "/ImageStorage/" + Filename;
            return Images;
        }

        [HttpPut]
        [Route("api/Product/Update")]
        public async Task<IActionResult> Edit([FromBody]ProductsModel model)
        {
            try
            {
                var Product = _db.Product.FirstOrDefault(x => x.ProductId == model.ProductId);
                Product.ProductId = model.ProductId;
                Product.ProductName = model.ProductName;
                Product.ProductPrice = model.ProductPrice;
                Product.ProductCategory = model.ProductCategory;
                Product.ProductImage = GetImage(model.ProductImage);
                Product.ProductPublishDate = Product.ProductPublishDate;
                Product.IsActive = true;
                Product.CreatedDate = Product.CreatedDate;
                Product.UpdatedDate = DateTime.Now;
                _db.Product.Update(Product);
                await _db.SaveChangesAsync();
                return Ok(new { Message = "Product is Updated", product = Product });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("api/Product/Delete")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                ProductViewModel Products = _db.Product.Find(id);
                _db.Product.Remove(Products);
                await _db.SaveChangesAsync();
                return Ok(new { Message = "Product is Deleted" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
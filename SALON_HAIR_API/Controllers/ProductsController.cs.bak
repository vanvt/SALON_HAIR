
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using ULTIL_HELPER;
using Microsoft.AspNetCore.Authorization;
namespace SALON_HAIR_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : CustomControllerBase
    {
        private readonly IProduct _product;
        private readonly IUser _user;
        private readonly IProductUnit _productUnit;
        private readonly IStatus _status;
        private readonly IPhoto _photo;
        public ProductsController(IStatus status,IProductUnit productUnit,IProduct product, IUser user, IPhoto photo)
        {
            _photo = photo;
            _status = status;
            _product = product;
            _user = user;
            _productUnit = productUnit;
        }

        // GET: api/Products
        [HttpGet]
        public IActionResult GetProduct(int page = 1, int rowPerPage = 50, string keyword = "", long productCategoryId =0)
        {
            var data = _product.SearchAllFileds(keyword);
            if (productCategoryId != 0)
            {
                data = data.Where(e => e.ProductCategoryId == productCategoryId);
            }
            return OkList(_product.Paging( data,page,rowPerPage).Include(e=>e.Unit).Include(e=>e.Photo));
        }
        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var product = await _product.GetAll().Where(e=>e.Id==id).FirstOrDefaultAsync();

                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] long id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != product.Id)
            {
                return BadRequest();
            }
            try
            {
                product.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                await _product.EditAsync(product);

                product.Unit = await _productUnit.FindAsync(product.UnitId);
                product.Photo = await _photo.FindAsync(product.PhotoId);
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }           
            catch (Exception e)
            {

                throw;
            }
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                product.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                product.Unit = await _productUnit.FindAsync(product.UnitId);
                product.Photo = await _photo.FindAsync(product.PhotoId);
                await _product.AddAsync(product);
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var product = await _product.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                await _product.DeleteAsync(product);

                return Ok(product);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        private bool ProductExists(long id)
        {
            return _product.Any<Product>(e => e.Id == id);
        }
        [HttpPut("change-status/{id}")]
        public async Task<IActionResult> ChangeStatusAsync([FromBody] Product product, [FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var oldProduct = await _product.FindAsync(id);
             
                if (product == null)
                {
                    return NotFound();
                }
                
                oldProduct.Status = product.Status.ToUpper();
                await _product.EditAsync(oldProduct);
                //oldProduct.Unit = await _productUnit.FindAsync(oldProduct.UnitId);
                //product.Photo = await _photo.FindAsync(product.PhotoId);
                _product.LoadAllReference(oldProduct);
                return Ok(oldProduct);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}


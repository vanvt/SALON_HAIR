
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using ULTIL_HELPER;
using Microsoft.AspNetCore.Authorization;
using SALON_HAIR_API.Exceptions;
using SALON_HAIR_API.ViewModels;

namespace SALON_HAIR_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : CustomControllerBase
    {
        private readonly IProduct _product;
        private readonly IProductSalonBranch _productSalonBranch;
        private readonly IUser _user;
        private readonly IProductUnit _productUnit;
        private readonly IStatus _status;
        private readonly IPhoto _photo;
        private readonly IProductStatus _productStatus;
        public ProductsController(IProductSalonBranch productSalonBranch,IProductStatus productStatus, IStatus status, IProductUnit productUnit, IProduct product, IUser user, IPhoto photo)
        {
            _productSalonBranch = productSalonBranch;
            _productStatus = productStatus;
            _photo = photo;
            _status = status;
            _product = product;
            _user = user;
            _productUnit = productUnit;
        }
        // GET: api/Products
        [HttpGet]
        public IActionResult GetProduct(int page = 1, int rowPerPage = 50, string keyword = "", long productCategoryId = 0, long productStatusId = 0)
        {
            var data = _product.SearchAllFileds(keyword);
            data = GetByCurrentSalon(data);
            data = GetByCurrentSpaBranch(data);
            if (productCategoryId != 0)
            {
                data = data.Where(e => e.ProductCategoryId == productCategoryId);
            }
            if (productStatusId != 0)
            {
                data = data.Where(e => e.ProductStatusId == productStatusId);
            }
            data = data.OrderByDescending(e => e.Id);
            data = data.Include(e => e.Unit).Include(e => e.Photo);
            return OkList(data);
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
                var product = await _product.GetAll().Where(e => e.Id == id).FirstOrDefaultAsync();

                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id, e);
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
                product.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
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

                throw new UnexpectedException(product, e);
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
                product.SalonId = JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals("salonId"));
                product.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));              
                await _product.AddAsync(product);
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            catch (Exception e)
            {
                throw new UnexpectedException(product, e);
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
        [HttpPut("change-status-mutiple")]
        public async Task<IActionResult> ChangeStatusMutiple([FromBody] ProductIdsVM productsVM)
        {
            //return null;
            try
            {
                var productStatus = _productStatus.FindBy(e => e.Code.Equals(productsVM.StatusCode)).FirstOrDefault();
                if (productStatus == null)
                {
                    return BadRequest($"Can't not found status code {productsVM}");
                }
                var products = _product.FindBy(e => productsVM.Ids.Contains(e.Id));
                products.Select(e => e.Code);
                await products.ForEachAsync(e => e.ProductStatusId = productStatus.Id);
                await _product.EditRangeAsync(products);
                //await Task.WhenAll(t1,t2);
                //return Ok(productsVM);
                return Ok(productsVM);

            }
            catch (Exception e)
            {

                throw new UnexpectedException(productsVM, e);
            }

        }
        [HttpDelete("delete-mutiple")]
        public async Task<IActionResult> ShowInvoice([FromBody] ProductIdsDelete productsVM)
        {
            try
            {
                var products = _product.FindBy(e => productsVM.Ids.Contains(e.Id));
                await products.ForEachAsync(e => e.Status = "DELETED");
                await _product.EditRangeAsync(products);

                return Ok(productsVM);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(productsVM, e);
            }

        }
        [HttpPut("stop-selling")]
        public async Task<IActionResult> StopSelling([FromBody] ProductIdsDelete productsVM)
        {
            //return null;
            try
            {
                var productStatus = _productStatus.FindBy(e => e.Code.Equals("STOP")).FirstOrDefault();
                if (productStatus == null)
                {
                    return BadRequest($"Can't not found status code {productsVM}");
                }
                var products = _product.FindBy(e => productsVM.Ids.Contains(e.Id));
                await products.ForEachAsync(e => e.ProductStatusId = productStatus.Id);
                await _product.EditRangeAsync(products);
                return Ok(productsVM);

            }
            catch (Exception e)
            {

                throw new UnexpectedException(productsVM, e);
            }

        }
        private IQueryable<Product> GetByCurrentSpaBranch(IQueryable<Product> data)
        {
            var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

            if (currentSalonBranch != default || currentSalonBranch != 0)
            {
             var listPackageAvailable = _productSalonBranch
            .FindBy(e => e.SalonBranchId == currentSalonBranch)
            .Where(e => e.Status.Equals("ENABLE"))
            .Select(e => e.ProductId);
                data = data.Where(e => listPackageAvailable.Contains(e.Id));
            }
            return data;
        }
        private IQueryable<Product> GetByCurrentSalon(IQueryable<Product> data)
        {
            var salonId = JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID));
            data = data.Where(e => e.SalonId == salonId);
            return data;
        }
    }
}


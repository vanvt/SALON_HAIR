
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
namespace SALON_HAIR_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProductSourcesController : CustomControllerBase
    {
        private readonly IProductSource _productSource;
        private readonly IUser _user;

        public ProductSourcesController(IProductSource productSource, IUser user)
        {
            _productSource = productSource;
            _user = user;
        }

        // GET: api/ProductSources
        [HttpGet]
        public IActionResult GetProductSource(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _productSource.SearchAllFileds(keyword).Where
                (e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals("salonId"))); ;
            data = data.OrderBy(e => e.Name);
            var dataReturn = _productSource.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/ProductSources/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductSource([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var productSource = await _productSource.FindAsync(id);

                if (productSource == null)
                {
                    return NotFound();
                }
                return Ok(productSource);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/ProductSources/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductSource([FromRoute] long id, [FromBody] ProductSource productSource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != productSource.Id)
            {
                return BadRequest();
            }
            try
            {
                productSource.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _productSource.EditAsync(productSource);
                return CreatedAtAction("GetProductSource", new { id = productSource.Id }, productSource);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ProductSourceExists(id))
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

                throw new UnexpectedException(productSource, e);
            }
        }

        // POST: api/ProductSources
        [HttpPost]
        public async Task<IActionResult> PostProductSource([FromBody] ProductSource productSource)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                productSource.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _productSource.AddAsync(productSource);
                return CreatedAtAction("GetProductSource", new { id = productSource.Id }, productSource);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(productSource, e);
            }

        }

        // DELETE: api/ProductSources/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductSource([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var productSource = await _productSource.FindAsync(id);
                if (productSource == null)
                {
                    return NotFound();
                }

                await _productSource.DeleteAsync(productSource);

                return Ok(productSource);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id, e);
            }

        }

        private bool ProductSourceExists(long id)
        {
            return _productSource.Any<ProductSource>(e => e.Id == id);
        }
    }
}


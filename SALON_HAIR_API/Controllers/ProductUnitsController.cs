
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
    public class ProductUnitsController : CustomControllerBase
    {
        private readonly IProductUnit _productUnit;
        private readonly IUser _user;

        public ProductUnitsController(IProductUnit productUnit, IUser user)
        {
            _productUnit = productUnit;
            _user = user;
        }

        // GET: api/ProductUnits
        [HttpGet]
        public IActionResult GetProductUnit(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _productUnit.SearchAllFileds(keyword);
            var dataReturn =   _productUnit.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/ProductUnits/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductUnit([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var productUnit = await _productUnit.FindAsync(id);

                if (productUnit == null)
                {
                    return NotFound();
                }
                return Ok(productUnit);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/ProductUnits/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductUnit([FromRoute] long id, [FromBody] ProductUnit productUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != productUnit.Id)
            {
                return BadRequest();
            }
            try
            {
                productUnit.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _productUnit.EditAsync(productUnit);
                return CreatedAtAction("GetProductUnit", new { id = productUnit.Id }, productUnit);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ProductUnitExists(id))
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

                  throw new UnexpectedException(productUnit,e);
            }
        }

        // POST: api/ProductUnits
        [HttpPost]
        public async Task<IActionResult> PostProductUnit([FromBody] ProductUnit productUnit)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                productUnit.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _productUnit.AddAsync(productUnit);
                return CreatedAtAction("GetProductUnit", new { id = productUnit.Id }, productUnit);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(productUnit,e);
            }
          
        }

        // DELETE: api/ProductUnits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductUnit([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var productUnit = await _productUnit.FindAsync(id);
                if (productUnit == null)
                {
                    return NotFound();
                }

                await _productUnit.DeleteAsync(productUnit);

                return Ok(productUnit);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool ProductUnitExists(long id)
        {
            return _productUnit.Any<ProductUnit>(e => e.Id == id);
        }
    }
}


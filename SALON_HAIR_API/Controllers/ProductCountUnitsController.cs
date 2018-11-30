
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
    public class ProductCountUnitsController : CustomControllerBase
    {
        private readonly IProductCountUnit _productCountUnit;
        private readonly IUser _user;

        public ProductCountUnitsController(IProductCountUnit productCountUnit, IUser user)
        {
            _productCountUnit = productCountUnit;
            _user = user;
        }

        // GET: api/ProductCountUnits
        [HttpGet]
        public IActionResult GetProductCountUnit(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _productCountUnit.SearchAllFileds(keyword);
            var dataReturn =   _productCountUnit.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/ProductCountUnits/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductCountUnit([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var productCountUnit = await _productCountUnit.FindAsync(id);

                if (productCountUnit == null)
                {
                    return NotFound();
                }
                return Ok(productCountUnit);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/ProductCountUnits/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCountUnit([FromRoute] long id, [FromBody] ProductCountUnit productCountUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != productCountUnit.Id)
            {
                return BadRequest();
            }
            try
            {
                productCountUnit.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _productCountUnit.EditAsync(productCountUnit);
                return CreatedAtAction("GetProductCountUnit", new { id = productCountUnit.Id }, productCountUnit);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCountUnitExists(id))
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

                  throw new UnexpectedException(productCountUnit,e);
            }
        }

        // POST: api/ProductCountUnits
        [HttpPost]
        public async Task<IActionResult> PostProductCountUnit([FromBody] ProductCountUnit productCountUnit)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                productCountUnit.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _productCountUnit.AddAsync(productCountUnit);
                return CreatedAtAction("GetProductCountUnit", new { id = productCountUnit.Id }, productCountUnit);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(productCountUnit,e);
            }
          
        }

        // DELETE: api/ProductCountUnits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCountUnit([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var productCountUnit = await _productCountUnit.FindAsync(id);
                if (productCountUnit == null)
                {
                    return NotFound();
                }

                await _productCountUnit.DeleteAsync(productCountUnit);

                return Ok(productCountUnit);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool ProductCountUnitExists(long id)
        {
            return _productCountUnit.Any<ProductCountUnit>(e => e.Id == id);
        }
    }
}


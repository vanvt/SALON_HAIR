
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
    public class DiscountUnitsController : CustomControllerBase
    {
        private readonly IDiscountUnit _discountUnit;
        private readonly IUser _user;

        public DiscountUnitsController(IDiscountUnit discountUnit, IUser user)
        {
            _discountUnit = discountUnit;
            _user = user;
        }

        // GET: api/DiscountUnits
        [HttpGet]
        public IActionResult GetDiscountUnit(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _discountUnit.SearchAllFileds(keyword);
            var dataReturn =   _discountUnit.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/DiscountUnits/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountUnit([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var discountUnit = await _discountUnit.FindAsync(id);

                if (discountUnit == null)
                {
                    return NotFound();
                }
                return Ok(discountUnit);
            }
            catch (Exception e)
            {
                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/DiscountUnits/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscountUnit([FromRoute] long id, [FromBody] DiscountUnit discountUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != discountUnit.Id)
            {
                return BadRequest();
            }
            try
            {
                discountUnit.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                await _discountUnit.EditAsync(discountUnit);
                return CreatedAtAction("GetDiscountUnit", new { id = discountUnit.Id }, discountUnit);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!DiscountUnitExists(id))
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

                  throw new UnexpectedException(discountUnit,e);
            }
        }

        // POST: api/DiscountUnits
        [HttpPost]
        public async Task<IActionResult> PostDiscountUnit([FromBody] DiscountUnit discountUnit)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                discountUnit.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                await _discountUnit.AddAsync(discountUnit);
                return CreatedAtAction("GetDiscountUnit", new { id = discountUnit.Id }, discountUnit);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(discountUnit,e);
            }
          
        }

        // DELETE: api/DiscountUnits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscountUnit([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var discountUnit = await _discountUnit.FindAsync(id);
                if (discountUnit == null)
                {
                    return NotFound();
                }

                await _discountUnit.DeleteAsync(discountUnit);

                return Ok(discountUnit);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool DiscountUnitExists(long id)
        {
            return _discountUnit.Any<DiscountUnit>(e => e.Id == id);
        }
    }
}


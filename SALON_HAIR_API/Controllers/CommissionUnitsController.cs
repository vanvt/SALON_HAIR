
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
    public class CommissionUnitsController : CustomControllerBase
    {
        private readonly ICommissionUnit _commissionUnit;
        private readonly IUser _user;

        public CommissionUnitsController(ICommissionUnit commissionUnit, IUser user)
        {
            _commissionUnit = commissionUnit;
            _user = user;
        }

        // GET: api/CommissionUnits
        [HttpGet]
        public IActionResult GetCommissionUnit(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _commissionUnit.SearchAllFileds(keyword);
            var dataReturn =   _commissionUnit.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/CommissionUnits/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommissionUnit([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var commissionUnit = await _commissionUnit.FindAsync(id);

                if (commissionUnit == null)
                {
                    return NotFound();
                }
                return Ok(commissionUnit);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/CommissionUnits/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommissionUnit([FromRoute] long id, [FromBody] CommissionUnit commissionUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != commissionUnit.Id)
            {
                return BadRequest();
            }
            try
            {
                commissionUnit.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _commissionUnit.EditAsync(commissionUnit);
                return CreatedAtAction("GetCommissionUnit", new { id = commissionUnit.Id }, commissionUnit);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!CommissionUnitExists(id))
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

                  throw new UnexpectedException(commissionUnit,e);
            }
        }

        // POST: api/CommissionUnits
        [HttpPost]
        public async Task<IActionResult> PostCommissionUnit([FromBody] CommissionUnit commissionUnit)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                commissionUnit.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _commissionUnit.AddAsync(commissionUnit);
                return CreatedAtAction("GetCommissionUnit", new { id = commissionUnit.Id }, commissionUnit);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(commissionUnit,e);
            }
          
        }

        // DELETE: api/CommissionUnits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommissionUnit([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var commissionUnit = await _commissionUnit.FindAsync(id);
                if (commissionUnit == null)
                {
                    return NotFound();
                }

                await _commissionUnit.DeleteAsync(commissionUnit);

                return Ok(commissionUnit);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool CommissionUnitExists(long id)
        {
            return _commissionUnit.Any<CommissionUnit>(e => e.Id == id);
        }
    }
}


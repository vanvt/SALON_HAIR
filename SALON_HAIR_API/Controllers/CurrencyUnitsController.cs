
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
    public class CurrencyUnitsController : CustomControllerBase
    {
        private readonly ICurrencyUnit _currencyUnit;
        private readonly IUser _user;

        public CurrencyUnitsController(ICurrencyUnit currencyUnit, IUser user)
        {
            _currencyUnit = currencyUnit;
            _user = user;
        }

        // GET: api/CurrencyUnits
        [HttpGet]
        public IActionResult GetCurrencyUnit(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _currencyUnit.SearchAllFileds(keyword);
            var dataReturn =   _currencyUnit.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/CurrencyUnits/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrencyUnit([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var currencyUnit = await _currencyUnit.FindAsync(id);

                if (currencyUnit == null)
                {
                    return NotFound();
                }
                return Ok(currencyUnit);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/CurrencyUnits/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurrencyUnit([FromRoute] long id, [FromBody] CurrencyUnit currencyUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != currencyUnit.Id)
            {
                return BadRequest();
            }
            try
            {
                currencyUnit.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _currencyUnit.EditAsync(currencyUnit);
                return CreatedAtAction("GetCurrencyUnit", new { id = currencyUnit.Id }, currencyUnit);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyUnitExists(id))
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

                  throw new UnexpectedException(currencyUnit,e);
            }
        }

        // POST: api/CurrencyUnits
        [HttpPost]
        public async Task<IActionResult> PostCurrencyUnit([FromBody] CurrencyUnit currencyUnit)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                currencyUnit.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _currencyUnit.AddAsync(currencyUnit);
                return CreatedAtAction("GetCurrencyUnit", new { id = currencyUnit.Id }, currencyUnit);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(currencyUnit,e);
            }
          
        }

        // DELETE: api/CurrencyUnits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrencyUnit([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var currencyUnit = await _currencyUnit.FindAsync(id);
                if (currencyUnit == null)
                {
                    return NotFound();
                }

                await _currencyUnit.DeleteAsync(currencyUnit);

                return Ok(currencyUnit);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool CurrencyUnitExists(long id)
        {
            return _currencyUnit.Any<CurrencyUnit>(e => e.Id == id);
        }
    }
}


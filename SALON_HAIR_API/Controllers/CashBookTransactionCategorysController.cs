
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
    public class CashBookTransactionCategorysController : CustomControllerBase
    {
        private readonly ICashBookTransactionCategory _cashBookTransactionCategory;
        private readonly IUser _user;

        public CashBookTransactionCategorysController(ICashBookTransactionCategory cashBookTransactionCategory, IUser user)
        {
            _cashBookTransactionCategory = cashBookTransactionCategory;
            _user = user;
        }

        // GET: api/CashBookTransactionCategorys
        [HttpGet]
        public IActionResult GetCashBookTransactionCategory(string type ="",int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _cashBookTransactionCategory.SearchAllFileds(keyword);
            data = GetByCurrentSalon(data);
            data = data.Where(e => e.Type.Equals(type));
            var dataReturn =   _cashBookTransactionCategory.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/CashBookTransactionCategorys/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCashBookTransactionCategory([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var cashBookTransactionCategory = await _cashBookTransactionCategory.FindAsync(id);

                if (cashBookTransactionCategory == null)
                {
                    return NotFound();
                }
                return Ok(cashBookTransactionCategory);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/CashBookTransactionCategorys/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCashBookTransactionCategory([FromRoute] long id, [FromBody] CashBookTransactionCategory cashBookTransactionCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != cashBookTransactionCategory.Id)
            {
                return BadRequest();
            }
            try
            {
                cashBookTransactionCategory.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _cashBookTransactionCategory.EditAsync(cashBookTransactionCategory);
                return CreatedAtAction("GetCashBookTransactionCategory", new { id = cashBookTransactionCategory.Id }, cashBookTransactionCategory);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!CashBookTransactionCategoryExists(id))
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

                  throw new UnexpectedException(cashBookTransactionCategory,e);
            }
        }

        // POST: api/CashBookTransactionCategorys
        [HttpPost]
        public async Task<IActionResult> PostCashBookTransactionCategory([FromBody] CashBookTransactionCategory cashBookTransactionCategory)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                cashBookTransactionCategory.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _cashBookTransactionCategory.AddAsync(cashBookTransactionCategory);
                return CreatedAtAction("GetCashBookTransactionCategory", new { id = cashBookTransactionCategory.Id }, cashBookTransactionCategory);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(cashBookTransactionCategory,e);
            }
          
        }

        // DELETE: api/CashBookTransactionCategorys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCashBookTransactionCategory([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var cashBookTransactionCategory = await _cashBookTransactionCategory.FindAsync(id);
                if (cashBookTransactionCategory == null)
                {
                    return NotFound();
                }

                await _cashBookTransactionCategory.DeleteAsync(cashBookTransactionCategory);

                return Ok(cashBookTransactionCategory);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool CashBookTransactionCategoryExists(long id)
        {
            return _cashBookTransactionCategory.Any<CashBookTransactionCategory>(e => e.Id == id);
        }
      
        private IQueryable<CashBookTransactionCategory> GetByCurrentSalon(IQueryable<CashBookTransactionCategory> data)
        {
            data = data.Where(e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID)));
            return data;
        }
}
}


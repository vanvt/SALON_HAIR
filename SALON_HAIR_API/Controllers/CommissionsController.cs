
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
    public class CommissionsController : CustomControllerBase
    {
        private readonly ICommission _commission;
        private readonly IUser _user;

        public CommissionsController(ICommission commission, IUser user)
        {
            _commission = commission;
            _user = user;
        }

        // GET: api/Commissions
        [HttpGet]
        public IActionResult GetCommission(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "",long staffCommisonGroupId = 0)
        {
            if (staffCommisonGroupId != 0)
            {
                return OkList(_commission.Paging(_commission.SearchAllFileds(keyword).Where(e => e.StaffCommisonGroupId == staffCommisonGroupId), page, rowPerPage)
                    .Include(e => e.RetailCommisionUnit)
                    .Include(e => e.WholesaleCommisionUnit)
                    .Include(e => e.LimitCommisionUnit)
                    .Include(e => e.ServiceCategory)
                    .Include(e => e.CommissionDetail)
                        .ThenInclude(e => e.Service)
                    .Include(e => e.CommissionDetail).ThenInclude(e => e.RetailCommisionUnit)
                    .Include(e => e.CommissionDetail).ThenInclude(e => e.WholesaleCommisionUnit)
                    .Include(e => e.CommissionDetail).ThenInclude(e => e.LimitCommisionUnit)
                        );                  
            }
            return OkList(_commission.Paging( _commission.SearchAllFileds(keyword),page,rowPerPage)
                  .Include(e => e.RetailCommisionUnit)
                    .Include(e => e.WholesaleCommisionUnit)
                    .Include(e => e.LimitCommisionUnit)
                    .Include(e => e.ServiceCategory)
                    .Include(e => e.CommissionDetail).ThenInclude(e => e.Service));                  
        }
        // GET: api/Commissions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommission([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var commission = await _commission.FindAsync(id);

                if (commission == null)
                {
                    return NotFound();
                }
                return Ok(commission);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/Commissions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommission([FromRoute] long id, [FromBody] Commission commission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != commission.Id)
            {
                return BadRequest();
            }
            try
            {
                commission.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                await _commission.EditAsync(commission);
                var data = await _commission.FindBy(e => e.Id == commission.Id).Include(e => e.RetailCommisionUnit)
               .Include(e => e.WholesaleCommisionUnit)
               .Include(e => e.LimitCommisionUnit)
               .Include(e => e.ServiceCategory)
               .Include(e => e.CommissionDetail)
                   .ThenInclude(e => e.Service)
               .Include(e => e.CommissionDetail).ThenInclude(e => e.RetailCommisionUnit)
               .Include(e => e.CommissionDetail).ThenInclude(e => e.WholesaleCommisionUnit)
               .Include(e => e.CommissionDetail).ThenInclude(e => e.LimitCommisionUnit).FirstOrDefaultAsync();
                return CreatedAtAction("GetCommission", new { id = commission.Id }, data);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!CommissionExists(id))
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

                  throw new UnexpectedException(commission,e);
            }
        }

        // POST: api/Commissions
        [HttpPost]
        public async Task<IActionResult> PostCommission([FromBody] Commission commission)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                commission.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _commission.AddAsync(commission);

                var data = await _commission.FindBy(e => e.Id == commission.Id).Include(e => e.RetailCommisionUnit)
                    .Include(e => e.WholesaleCommisionUnit)
                    .Include(e => e.LimitCommisionUnit)
                    .Include(e => e.ServiceCategory)
                    .Include(e => e.CommissionDetail)
                        .ThenInclude(e => e.Service)
                    .Include(e => e.CommissionDetail).ThenInclude(e => e.RetailCommisionUnit)
                    .Include(e => e.CommissionDetail).ThenInclude(e => e.WholesaleCommisionUnit)
                    .Include(e => e.CommissionDetail).ThenInclude(e => e.LimitCommisionUnit).FirstOrDefaultAsync();
                return CreatedAtAction("GetCommission", new { id = commission.Id }, data);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(commission,e);
            }
          
        }

        // DELETE: api/Commissions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommission([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var commission = await _commission.FindAsync(id);
                if (commission == null)
                {
                    return NotFound();
                }

                await _commission.DeleteAsync(commission);

                return Ok(commission);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool CommissionExists(long id)
        {
            return _commission.Any<Commission>(e => e.Id == id);
        }
    }
}


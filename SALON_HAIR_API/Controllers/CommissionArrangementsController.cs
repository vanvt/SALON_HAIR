
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
    public class CommissionArrangementsController : CustomControllerBase
    {
        private readonly ICommissionArrangement _commissionArrangement;
        private readonly IUser _user;

        public CommissionArrangementsController(ICommissionArrangement commissionArrangement, IUser user)
        {
            _commissionArrangement = commissionArrangement;
            _user = user;
        }

        // GET: api/CommissionArrangements
        [HttpGet]
        public IActionResult GetCommissionArrangement(long invoiceId, int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _commissionArrangement.SearchAllFileds(keyword);
            var dataReturn =   _commissionArrangement.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/CommissionArrangements/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommissionArrangement([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var commissionArrangement = await _commissionArrangement.FindAsync(id);

                if (commissionArrangement == null)
                {
                    return NotFound();
                }
                return Ok(commissionArrangement);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/CommissionArrangements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommissionArrangement([FromRoute] long id, [FromBody] CommissionArrangement commissionArrangement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != commissionArrangement.Id)
            {
                return BadRequest();
            }
            try
            {
                commissionArrangement.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _commissionArrangement.EditAsync(commissionArrangement);
                return CreatedAtAction("GetCommissionArrangement", new { id = commissionArrangement.Id }, commissionArrangement);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!CommissionArrangementExists(id))
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

                  throw new UnexpectedException(commissionArrangement,e);
            }
        }

        // POST: api/CommissionArrangements
        [HttpPost]
        public async Task<IActionResult> PostCommissionArrangement([FromBody] CommissionArrangement commissionArrangement)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                commissionArrangement.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _commissionArrangement.AddAsync(commissionArrangement);
                return CreatedAtAction("GetCommissionArrangement", new { id = commissionArrangement.Id }, commissionArrangement);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(commissionArrangement,e);
            }
          
        }

        // DELETE: api/CommissionArrangements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommissionArrangement([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var commissionArrangement = await _commissionArrangement.FindAsync(id);
                if (commissionArrangement == null)
                {
                    return NotFound();
                }

                await _commissionArrangement.DeleteAsync(commissionArrangement);

                return Ok(commissionArrangement);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool CommissionArrangementExists(long id)
        {
            return _commissionArrangement.Any<CommissionArrangement>(e => e.Id == id);
        }

        private IQueryable<CommissionArrangement> GetByCurrentSpaBranch(IQueryable<CommissionArrangement> data)
        {
            var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

            if (currentSalonBranch != default || currentSalonBranch != 0)
            {
                data = data.Where(e => e.SalonBranchId == currentSalonBranch);
            }
            return data;
        }
        private IQueryable<CommissionArrangement> GetByCurrentSalon(IQueryable<CommissionArrangement> data)
        {
            var salonId = JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID));
            data = data.Where(e => e.SalonId == salonId);
            return data;
        }
    }
}


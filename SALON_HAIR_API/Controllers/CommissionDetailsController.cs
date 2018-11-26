
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
    public class CommissionDetailsController : CustomControllerBase
    {
        private readonly ICommissionDetail _commissionDetail;
        private readonly IUser _user;

        public CommissionDetailsController(ICommissionDetail commissionDetail, IUser user)
        {
            _commissionDetail = commissionDetail;
            _user = user;
        }

        // GET: api/CommissionDetails
        [HttpGet]
        public IActionResult GetCommissionDetail(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _commissionDetail.SearchAllFileds(keyword);
            var dataReturn =   _commissionDetail.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/CommissionDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommissionDetail([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var commissionDetail = await _commissionDetail.FindAsync(id);

                if (commissionDetail == null)
                {
                    return NotFound();
                }
                return Ok(commissionDetail);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/CommissionDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommissionDetail([FromRoute] long id, [FromBody] CommissionDetail commissionDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != commissionDetail.Id)
            {
                return BadRequest();
            }
            try
            {
                commissionDetail.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _commissionDetail.EditAsync(commissionDetail);
                return CreatedAtAction("GetCommissionDetail", new { id = commissionDetail.Id }, commissionDetail);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!CommissionDetailExists(id))
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

                  throw new UnexpectedException(commissionDetail,e);
            }
        }

        // POST: api/CommissionDetails
        [HttpPost]
        public async Task<IActionResult> PostCommissionDetail([FromBody] CommissionDetail commissionDetail)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                commissionDetail.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _commissionDetail.AddAsync(commissionDetail);
                return CreatedAtAction("GetCommissionDetail", new { id = commissionDetail.Id }, commissionDetail);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(commissionDetail,e);
            }
          
        }

        // DELETE: api/CommissionDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommissionDetail([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var commissionDetail = await _commissionDetail.FindAsync(id);
                if (commissionDetail == null)
                {
                    return NotFound();
                }

                await _commissionDetail.DeleteAsync(commissionDetail);

                return Ok(commissionDetail);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool CommissionDetailExists(long id)
        {
            return _commissionDetail.Any<CommissionDetail>(e => e.Id == id);
        }
    }
}



using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using ULTIL_HELPER;
using Microsoft.AspNetCore.Authorization;
namespace SALON_HAIR_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class SalonBranchsController : CustomControllerBase
    {
        private readonly ISalonBranch _salonBranch;
        private readonly IUser _user;
        public SalonBranchsController(ISalonBranch salonBranch, IUser user)
        {
            _salonBranch = salonBranch;
            _user = user;
        }

        // GET: api/SalonBranchs
        [HttpGet]
        public IActionResult GetSalonBranch(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            return OkList(_salonBranch.Paging( _salonBranch.SearchAllFileds(keyword,orderBy,orderType),page,rowPerPage));
        }
        // GET: api/SalonBranchs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalonBranch([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var salonBranch = await _salonBranch.FindAsync(id);

                if (salonBranch == null)
                {
                    return NotFound();
                }
                return Ok(salonBranch);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        // PUT: api/SalonBranchs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalonBranch([FromRoute] long id, [FromBody] SalonBranch salonBranch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != salonBranch.Id)
            {
                return BadRequest();
            }
            try
            {
                salonBranch.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                await _salonBranch.EditAsync(salonBranch);
                return CreatedAtAction("GetSalonBranch", new { id = salonBranch.Id }, salonBranch);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!SalonBranchExists(id))
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

                throw;
            }
        }

        // POST: api/SalonBranchs
        [HttpPost]
        public async Task<IActionResult> PostSalonBranch([FromBody] SalonBranch salonBranch)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                salonBranch.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));

                await _salonBranch.AddAsync(salonBranch);
                return CreatedAtAction("GetSalonBranch", new { id = salonBranch.Id }, salonBranch);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        // DELETE: api/SalonBranchs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalonBranch([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var salonBranch = await _salonBranch.FindAsync(id);
                if (salonBranch == null)
                {
                    return NotFound();
                }

                await _salonBranch.DeleteAsync(salonBranch);

                return Ok(salonBranch);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        private bool SalonBranchExists(long id)
        {
            return _salonBranch.Any<SalonBranch>(e => e.Id == id);
        }
    }
}


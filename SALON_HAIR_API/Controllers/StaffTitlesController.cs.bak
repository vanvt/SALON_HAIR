
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
    public class StaffTitlesController : CustomControllerBase
    {
        private readonly IStaffTitle _staffTitle;
        private readonly IUser _user;

        public StaffTitlesController(IStaffTitle staffTitle, IUser user)
        {
            _staffTitle = staffTitle;
            _user = user;
        }

        // GET: api/StaffTitles
        [HttpGet]
        public IActionResult GetStaffTitle(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            return OkList(_staffTitle.Paging( _staffTitle.SearchAllFileds(keyword),page,rowPerPage));
        }
        // GET: api/StaffTitles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffTitle([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var staffTitle = await _staffTitle.FindAsync(id);

                if (staffTitle == null)
                {
                    return NotFound();
                }
                return Ok(staffTitle);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        // PUT: api/StaffTitles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaffTitle([FromRoute] long id, [FromBody] StaffTitle staffTitle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != staffTitle.Id)
            {
                return BadRequest();
            }
            try
            {
                staffTitle.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                await _staffTitle.EditAsync(staffTitle);
                return CreatedAtAction("GetStaffTitle", new { id = staffTitle.Id }, staffTitle);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!StaffTitleExists(id))
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

        // POST: api/StaffTitles
        [HttpPost]
        public async Task<IActionResult> PostStaffTitle([FromBody] StaffTitle staffTitle)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                staffTitle.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                await _staffTitle.AddAsync(staffTitle);
                return CreatedAtAction("GetStaffTitle", new { id = staffTitle.Id }, staffTitle);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        // DELETE: api/StaffTitles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaffTitle([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var staffTitle = await _staffTitle.FindAsync(id);
                if (staffTitle == null)
                {
                    return NotFound();
                }

                await _staffTitle.DeleteAsync(staffTitle);

                return Ok(staffTitle);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        private bool StaffTitleExists(long id)
        {
            return _staffTitle.Any<StaffTitle>(e => e.Id == id);
        }
    }
}


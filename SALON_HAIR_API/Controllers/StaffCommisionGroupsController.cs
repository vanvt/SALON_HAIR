
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
    public class StaffCommisionGroupsController : CustomControllerBase
    {
        private readonly IStaffCommissionGroup _staffCategory;
        private readonly IUser _user;

        public StaffCommisionGroupsController(IStaffCommissionGroup staffCategory, IUser user)
        {
            _staffCategory = staffCategory;
            _user = user;
        }

        // GET: api/StaffCommisionGroupsController
        [HttpGet]
        public IActionResult GetStaffCategory(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            return OkList(_staffCategory.Paging( _staffCategory.SearchAllFileds(keyword),page,rowPerPage).Include(e=>e.StaffTitle));
        }
        // GET: api/StaffCommisionGroupsController/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffCategory([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var staffCategory = await _staffCategory.FindAsync(id);

                if (staffCategory == null)
                {
                    return NotFound();
                }
                return Ok(staffCategory);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        // PUT: api/StaffCommisionGroupsController/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaffCategory([FromRoute] long id, [FromBody] StaffCommisonGroup staffCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != staffCategory.Id)
            {
                return BadRequest();
            }
            try
            {
                staffCategory.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                await _staffCategory.EditAsync(staffCategory);
                return CreatedAtAction("GetStaffCategory", new { id = staffCategory.Id }, staffCategory);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!StaffCategoryExists(id))
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

        // POST: api/StaffCommisionGroupsController
        [HttpPost]
        public async Task<IActionResult> PostStaffCategory([FromBody] StaffCommisonGroup staffCategory)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                staffCategory.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                await _staffCategory.AddAsync(staffCategory);
                return CreatedAtAction("GetStaffCategory", new { id = staffCategory.Id }, staffCategory);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        // DELETE: api/StaffCommisionGroupsController/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaffCategory([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var staffCategory = await _staffCategory.FindAsync(id);
                if (staffCategory == null)
                {
                    return NotFound();
                }

                await _staffCategory.DeleteAsync(staffCategory);

                return Ok(staffCategory);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        private bool StaffCategoryExists(long id)
        {
            return _staffCategory.Any<StaffCommisonGroup>(e => e.Id == id);
        }
    }
}


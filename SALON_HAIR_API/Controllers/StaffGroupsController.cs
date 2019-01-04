
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
    public class StaffGroupsController : CustomControllerBase
    {
        private readonly IStaffGroup _staffGroup;
        private readonly IUser _user;

        public StaffGroupsController(IStaffGroup staffGroup, IUser user)
        {
            _staffGroup = staffGroup;
            _user = user;
        }

        // GET: api/StaffGroups
        [HttpGet]
        public IActionResult GetStaffGroup(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "",string include="")
        {
            var data = _staffGroup.SearchAllFileds(keyword);
            data = GetByCurrentSalon(data);
            var dataReturn =   _staffGroup.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/StaffGroups/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffGroup([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var staffGroup = await _staffGroup.FindAsync(id);

                if (staffGroup == null)
                {
                    return NotFound();
                }
                return Ok(staffGroup);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/StaffGroups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaffGroup([FromRoute] long id, [FromBody] StaffGroup staffGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != staffGroup.Id)
            {
                return BadRequest();
            }
            try
            {
                staffGroup.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                await _staffGroup.EditAsync(staffGroup);
                return CreatedAtAction("GetStaffGroup", new { id = staffGroup.Id }, staffGroup);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!StaffGroupExists(id))
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

                  throw new UnexpectedException(staffGroup,e);
            }
        }

        // POST: api/StaffGroups
        [HttpPost]
        public async Task<IActionResult> PostStaffGroup([FromBody] StaffGroup staffGroup)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                staffGroup.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                await _staffGroup.AddAsync(staffGroup);
                return CreatedAtAction("GetStaffGroup", new { id = staffGroup.Id }, staffGroup);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(staffGroup,e);
            }
          
        }

        // DELETE: api/StaffGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaffGroup([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var staffGroup = await _staffGroup.FindAsync(id);
                if (staffGroup == null)
                {
                    return NotFound();
                }

                await _staffGroup.DeleteAsync(staffGroup);

                return Ok(staffGroup);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool StaffGroupExists(long id)
        {
            return _staffGroup.Any<StaffGroup>(e => e.Id == id);
        }
        private IQueryable<StaffGroup> GetByCurrentSalon(IQueryable<StaffGroup> data)
        {
            var salonId = JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID));
            data = data.Where(e => e.SalonId == salonId);
            return data;
        }
    }
}


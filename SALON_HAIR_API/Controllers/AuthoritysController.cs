
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
    public class AuthoritysController : CustomControllerBase
    {
        private readonly IAuthority _authority;
        private readonly IUser _user;

        public AuthoritysController(IAuthority authority, IUser user)
        {
            _authority = authority;
            _user = user;
        }

        // GET: api/Authoritys
        [HttpGet]
        public IActionResult GetAuthority(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _authority.SearchAllFileds(keyword).Where
                (e=>e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals("salonId")));            
            var dataReturn = _authority.LoadAllCollecttion(data);
            return OkList(dataReturn);
        }
        // GET: api/Authoritys/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthority([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var authority = await _authority.FindAsync(id);

                if (authority == null)
                {
                    return NotFound();
                }
                return Ok(authority);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/Authoritys/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthority([FromRoute] long id, [FromBody] Authority authority)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != authority.Id)
            {
                return BadRequest();
            }
            try
            {
             
                await _authority.EditAsync(authority);
                return CreatedAtAction("GetAuthority", new { id = authority.Id }, authority);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorityExists(id))
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

                  throw new UnexpectedException(authority,e);
            }
        }

        // POST: api/Authoritys
        [HttpPost]
        public async Task<IActionResult> PostAuthority([FromBody] Authority authority)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            
                await _authority.AddAsync(authority);
                return CreatedAtAction("GetAuthority", new { id = authority.Id }, authority);
            }
            catch (Exception e)
            {
                throw new UnexpectedException(authority,e);
            }
          
        }

        // DELETE: api/Authoritys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthority([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var authority = await _authority.FindAsync(id);
                if (authority == null)
                {
                    return NotFound();
                }

                await _authority.DeleteAsync(authority);

                return Ok(authority);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool AuthorityExists(long id)
        {
            return _authority.Any<Authority>(e => e.Id == id);
        }
        private IQueryable<Booking> GetByCurrentSpaBranch(IQueryable<Booking> data)
        {
            var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

            if (currentSalonBranch != default || currentSalonBranch != 0)
            {
                data = data.Where(e => e.SalonBranchId == currentSalonBranch);
            }
            return data;
        }
        private IQueryable<Booking> GetByCurrentSalon(IQueryable<Booking> data)
        {
            var salonId = JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID));
            data = data.Where(e => e.SalonId == salonId);
            return data;
        }
    }
}


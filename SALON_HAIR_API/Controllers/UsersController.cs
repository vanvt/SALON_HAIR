
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
using SALON_HAIR_API.ViewModels;

namespace SALON_HAIR_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : CustomControllerBase
    {
        private readonly ISecurityHelper SecurityHelper;
        private readonly IUser _user;
        public UsersController(IUser user, ISecurityHelper SecurityHelper)
        {
            this.SecurityHelper = SecurityHelper;
            _user = user;
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult GetUser(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _user.SearchAllFileds(keyword).Where
                (e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals("salonId"))); ;
            var dataReturn =   _user.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var Idtoken = JwtHelper.GetIdFromToken(User.Claims);
                if (Idtoken != id)
                {
                    return BadRequest();
                }
                //var jbtUser = await _jbtUser.FindBy(e => e.Id == id).Include(x => x.JbtUserAuthority).FirstOrDefaultAsync();
                var user = await _user.FindBy(e => e.Id == id)
                    .Include(x => x.Salon)
                    .Include(e => e.Photo)
                    .Include(e => e.SalonBranchCurrent)
                    .Include(e=>e.Salon).ThenInclude(e=>e.Photo)
                    .FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
           // return null;
        }
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser()
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var Idtoken = JwtHelper.GetIdFromToken(User.Claims);
                if (Idtoken == 0)
                {
                    return BadRequest();
                }
                //var jbtUser = await _jbtUser.FindBy(e => e.Id == id).Include(x => x.JbtUserAuthority).FirstOrDefaultAsync();

                //var user = await _user.FindBy(e => e.Id == Idtoken).Include(x => x.Salon).Include(e=>e.p) FirstOrDefaultAsync();
                var user = await _user.FindBy(e => e.Id == Idtoken)
                    .Include(x => x.Salon)
                    .Include(e => e.SalonBranchCurrent)
                    .Include(e=>e.Photo)
                    .Include(e => e.Salon).ThenInclude(e => e.Photo)
                    .FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(User.Claims, e);
            }
            // return null;
        }


        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] long id, [FromBody] UserVM user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != user.Id)
            {
                return BadRequest();
            }
            try
            {
                //user.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                user.PasswordHash = string.IsNullOrEmpty(user.Password) ? _user.FindBy(e => e.Id == id).AsNoTracking().FirstOrDefault().PasswordHash :
               SecurityHelper.BCryptPasswordEncoder(user.Password);
                await _user.EditAsync(user);
                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

                  throw new UnexpectedException(user,e);
            }
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserVM user)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                user.PasswordHash = SecurityHelper.BCryptPasswordEncoder(user.Password);
                user.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _user.AddAsync(user);
                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(user,e);
            }
          
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _user.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                await _user.DeleteAsync(user);

                return Ok(user);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool UserExists(long id)
        {
            return _user.Any<User>(e => e.Id == id);
        }
    }
}


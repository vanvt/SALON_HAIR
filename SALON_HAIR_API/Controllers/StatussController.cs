
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
    public class StatussController : CustomControllerBase
    {
        private readonly IStatus _status;
        private readonly IUser _user;
        public StatussController(IStatus status, IUser user)
        {
            _status = status;
            _user = user;
        }

        // GET: api/Statuss
        [HttpGet]
        public IActionResult GetStatus(int page = 1, int rowPerPage = 50, string keyword = "")
        {
            return OkList(_status.Paging( _status.SearchAllFileds(keyword),page,rowPerPage));
        }
        // GET: api/Statuss/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatus([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var status = await _status.FindAsync(id);

                if (status == null)
                {
                    return NotFound();
                }
                return Ok(status);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        // PUT: api/Statuss/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus([FromRoute] long id, [FromBody] Status status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != status.Id)
            {
                return BadRequest();
            }
            try
            {
                status.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                await _status.EditAsync(status);
                return CreatedAtAction("GetStatus", new { id = status.Id }, status);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(id))
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

        // POST: api/Statuss
        [HttpPost]
        public async Task<IActionResult> PostStatus([FromBody] Status status)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                status.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                await _status.AddAsync(status);
                return CreatedAtAction("GetStatus", new { id = status.Id }, status);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        // DELETE: api/Statuss/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var status = await _status.FindAsync(id);
                if (status == null)
                {
                    return NotFound();
                }

                await _status.DeleteAsync(status);

                return Ok(status);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        private bool StatusExists(long id)
        {
            return _status.Any<Status>(e => e.Id == id);
        }
    }
}

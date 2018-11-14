
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
    public class SalonsController : CustomControllerBase
    {
        private readonly ISalon _salon;
        private readonly IUser _user;
        public SalonsController(ISalon salon, IUser user)
        {
            _salon = salon;
            _user = user;
        }

        // GET: api/Salons
        [HttpGet]
        public IActionResult GetSalon(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            return OkList(_salon.Paging( _salon.SearchAllFileds(keyword,orderBy,orderType),page,rowPerPage));
        }
        // GET: api/Salons/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalon([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var salon = await _salon.FindAsync(id);

                if (salon == null)
                {
                    return NotFound();
                }
                return Ok(salon);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        // PUT: api/Salons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalon([FromRoute] long id, [FromBody] Salon salon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != salon.Id)
            {
                return BadRequest();
            }
            try
            {
                salon.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                await _salon.EditAsync(salon);
                return CreatedAtAction("GetSalon", new { id = salon.Id }, salon);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!SalonExists(id))
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

        // POST: api/Salons
        [HttpPost]
        public async Task<IActionResult> PostSalon([FromBody] Salon salon)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                salon.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                await _salon.AddAsync(salon);
                return CreatedAtAction("GetSalon", new { id = salon.Id }, salon);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        // DELETE: api/Salons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalon([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var salon = await _salon.FindAsync(id);
                if (salon == null)
                {
                    return NotFound();
                }

                await _salon.DeleteAsync(salon);

                return Ok(salon);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        private bool SalonExists(long id)
        {
            return _salon.Any<Salon>(e => e.Id == id);
        }
    }
}


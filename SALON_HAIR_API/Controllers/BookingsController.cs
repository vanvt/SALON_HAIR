
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
    public class BookingsController : CustomControllerBase
    {
        private readonly IBooking _booking;
        private readonly IUser _user;

        public BookingsController(IBooking booking, IUser user)
        {
            _booking = booking;
            _user = user;
        }

        // GET: api/Bookings
        [HttpGet]
        public IActionResult GetBooking(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            return OkList(_booking.Paging( _booking.SearchAllFileds(keyword),page,rowPerPage).Include(e=>e.Status));
        }
        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var booking = await _booking.FindAsync(id);

                if (booking == null)
                {
                    return NotFound();
                }
                return Ok(booking);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        // PUT: api/Bookings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking([FromRoute] long id, [FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != booking.Id)
            {
                return BadRequest();
            }
            try
            {
                booking.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                await _booking.EditAsync(booking);
                return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        // POST: api/Bookings
        [HttpPost]
        public async Task<IActionResult> PostBooking([FromBody] Booking booking)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                booking.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                await _booking.AddAsync(booking);
                return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var booking = await _booking.FindAsync(id);
                if (booking == null)
                {
                    return NotFound();
                }

                await _booking.DeleteAsync(booking);

                return Ok(booking);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        private bool BookingExists(long id)
        {
            return _booking.Any<Booking>(e => e.Id == id);
        }
    }
}


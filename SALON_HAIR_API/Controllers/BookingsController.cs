
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
    public class BookingsController : CustomControllerBase
    {
        private readonly ISysObjectAutoIncreament _sysObjectAutoIncreament;
        private readonly IBooking _booking;
        private readonly IBookingCustomer _bookingCustomer;
        private readonly IUser _user;

        public BookingsController(IBookingCustomer bookingCustomer,ISysObjectAutoIncreament sysObjectAutoIncreament,IBooking booking, IUser user)
        {
            _bookingCustomer = bookingCustomer;
            _booking = booking;
            _user = user;
            _sysObjectAutoIncreament = sysObjectAutoIncreament;
        }

        // GET: api/Bookings
        [HttpGet]
        public IActionResult GetBooking(long salonBranchId  = 0,int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            
            var data = _booking.SearchAllFileds(keyword).Where
                (e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals("salonId")))
                ;
            var dataReturn =   _booking.LoadAllInclude(data);
            return OkList(dataReturn);
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
                var bookingCustomer = _bookingCustomer.FindBy(e => e.BookingId == id).Include(e => e.BookingCustomerService);
                booking.BookingCustomer = await bookingCustomer.ToListAsync();
                if (booking == null)
                {
                    return NotFound();
                }
                return Ok(booking);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
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
                booking.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
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

                  throw new UnexpectedException(booking,e);
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
                booking.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                booking.SalonId = JwtHelper.GetCurrentInformationLong(User, e => e.Type.Equals("salonId"));
                booking.BookingCode = "ES" + _sysObjectAutoIncreament.
                    GetCodeByObjectAsync(nameof(Booking), booking.SalonId).
                    Result.ObjectIndex.ToString("000000");

                await _booking.AddAsync(booking);
                return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
            }
            catch (Exception e)
            {
                throw new UnexpectedException(booking,e);
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

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool BookingExists(long id)
        {
            return _booking.Any<Booking>(e => e.Id == id);
        }
    }
}


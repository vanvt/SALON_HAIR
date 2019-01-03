
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
using System.Security.Claims;

namespace SALON_HAIR_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BookingsController : CustomControllerBase
    {
        private readonly ISysObjectAutoIncreament _sysObjectAutoIncreament;
        private readonly IBooking _booking;
        private readonly IBookingDetail _bookingCustomer;
        private readonly IUser _user;
        private readonly ICustomer _customer;
        public BookingsController(ICustomer customer,IBookingDetail bookingCustomer, ISysObjectAutoIncreament sysObjectAutoIncreament, IBooking booking, IUser user)
        {
            _bookingCustomer = bookingCustomer;
            _booking = booking;
            _user = user;
            _sysObjectAutoIncreament = sysObjectAutoIncreament;
            _customer = customer;
        }

        // GET: api/Bookings
        [HttpGet]
        public IActionResult GetBooking(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "", string end = "", string start = "",string bookingStatus="")
        {
            var data = _booking.SearchAllFileds(keyword);
            var dataRange = GetDateRangeQuery(start, end);                                  
            data = data.Where(e => e.Date.Value.Date >= dataRange.Item1.Date && e.Date.Value.Date <= dataRange.Item2.Date.Date);
            data = GetByCurrentSalon(data);
            data = GetByCurrentSpaBranch(data);
            data = string.IsNullOrEmpty(bookingStatus)? data: data.Where(e => e.BookingStatus.Equals(bookingStatus));           
            data = OrderBy(data, orderType);

            var dataReturn = _booking.LoadAllInclude(data,nameof(CustomerChannel), nameof(CustomerSource));
            dataReturn = dataReturn.Include(e => e.Invoice);
            return OkList(dataReturn);
        }
        // GET: api/Bookings/5
        [HttpGet("by-customer/{customerId}")]
        public IActionResult GetBooking(long customerId, int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _booking.SearchAllFileds(keyword);
            data = GetByCurrentSalon(data);
            data = GetByCurrentSpaBranch(data);
            data = data.Where(e => e.CustomerId == customerId);
            data = data.Include(e => e.Invoice);
            var dataReturn = _booking.LoadAllInclude(data);
            return OkList(dataReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var booking =  _booking.GetAll().Where(c=>c.Id == id);
                booking = _booking.LoadAllInclude(booking)
                    .Include(e=>e.BookingDetail).ThenInclude(e=>e.BookingDetailService);

                var data = await booking.FirstOrDefaultAsync();
                if (data == null)
                {
                    return NotFound();
                }
                //var bookingCustomer = _bookingCustomer.FindBy(e => e.BookingId == id)
                //    .Include(e => e.BookingDetailService);
                //data.BookingDetail = await bookingCustomer.ToListAsync();                
              
                return Ok(data);
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
                booking.Date = DateTime.Parse(booking.DateString);
                booking.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                await _booking.EditAsyncOnetoManyAsync(booking);
                booking.Customer = _customer.Find(booking.CustomerId);
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
                throw new UnexpectedException(booking, e);
            }
        }

        [HttpPut("prepay/{id}")]
        public async Task<IActionResult> PutBookingAsPrePay([FromRoute] long id, [FromBody] Booking booking)
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

                var bookingupdate = _booking.Find(id);
                //booking.Date = DateTime.Parse(booking.DateString);
                bookingupdate.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                bookingupdate.BookingPrepayPayment = booking.BookingPrepayPayment;
                await _booking.EditAsPrePayAsync(bookingupdate);
                bookingupdate.Customer = _customer.Find(bookingupdate.CustomerId);
               
                return CreatedAtAction("GetBooking", new { id = bookingupdate.Id }, bookingupdate);
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
                throw new UnexpectedException(booking, e);
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

                booking.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                //booking.SalonId = JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals("salonId"));               
                booking.Date = DateTime.Parse(booking.DateString);
                booking.BookingCode = "ES" + _sysObjectAutoIncreament.
                    GetCodeByObjectAsync(nameof(Booking), booking.SalonId).
                    Result.ObjectIndex.ToString("000000");

                await _booking.AddRemoveNoNeedAsync(booking);
                booking.Customer = _customer.Find(booking.CustomerId);
                return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
            }
            catch (Exception e)
            {
                throw new UnexpectedException(booking, e);
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

                throw new UnexpectedException(id, e);
            }

        }
        [HttpPut("checkin/{id}")]
        public async Task<IActionResult> CheckinBooking([FromRoute] long id, [FromBody] Booking booking)
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
                //booking.Date = DateTime.Parse(booking.DateString);
                var customer = booking.Customer;
                var customerChannel = booking.CustomerChannel;
                var sourceChannel = booking.SourceChannel;

                booking.Customer = null;
                booking.CustomerChannel = null;
                booking.SourceChannel = null;
                booking.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                await _booking.EditAsyncCheckinAsync(booking);

                booking.Customer = customer;
                booking.CustomerChannel = customerChannel;
                booking.SourceChannel = sourceChannel;
                //booking.Customer = _customer.Find(booking.CustomerId);
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
                throw new UnexpectedException(booking, e);
            }
        }
        [HttpPut("checkout/{id}")]
        public async Task<IActionResult> CheckoutBooking([FromRoute] long id, [FromBody] Booking booking)
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
                //booking.Date = DateTime.Parse(booking.DateString);
                booking.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                await _booking.EditAsyncCheckoutAsync(booking);
                //booking.Customer = _customer.Find(booking.CustomerId);
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
                throw new UnexpectedException(booking, e);
            }
        }
        private bool BookingExists(long id)
        {
            return _booking.Any<Booking>(e => e.Id == id);
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
        private Tuple<DateTime ,DateTime> GetDateRangeQuery(string start,string end)
        {
            start += "";
            end += "";
            var st = DateTime.Now;
            var en = DateTime.Now.AddDays(30.0);
            if (!string.IsNullOrEmpty(start))
            {
             
                st = DateTime.Parse(start);
            }
            if (!string.IsNullOrEmpty(end))
            {
                en = DateTime.Parse(end);
            }
            return Tuple.Create(st, en);
        }
        private IQueryable<Booking> OrderBy(IQueryable<Booking> data,string orderType )
        {
            if (orderType.Equals("asc"))
            {
                data = data.OrderBy(e => e.Date.Value);
            }
            else
            {
                data = data.OrderByDescending(e => e.Date.Value);
            }
            return data;
        }

    }
}

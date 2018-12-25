
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
    public class BookingPrepayPaymentsController : CustomControllerBase
    {
        private readonly IBookingPrepayPayment _bookingPrepayPayment;
        private readonly IUser _user;

        public BookingPrepayPaymentsController(IBookingPrepayPayment bookingPrepayPayment, IUser user)
        {
            _bookingPrepayPayment = bookingPrepayPayment;
            _user = user;
        }

        // GET: api/BookingPrepayPayments
        [HttpGet]
        public IActionResult GetBookingPrepayPayment(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _bookingPrepayPayment.SearchAllFileds(keyword);
            var dataReturn =   _bookingPrepayPayment.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/BookingPrepayPayments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingPrepayPayment([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var bookingPrepayPayment = await _bookingPrepayPayment.FindAsync(id);

                if (bookingPrepayPayment == null)
                {
                    return NotFound();
                }
                return Ok(bookingPrepayPayment);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/BookingPrepayPayments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingPrepayPayment([FromRoute] long id, [FromBody] BookingPrepayPayment bookingPrepayPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != bookingPrepayPayment.Id)
            {
                return BadRequest();
            }
            try
            {
                bookingPrepayPayment.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _bookingPrepayPayment.EditAsync(bookingPrepayPayment);
                return CreatedAtAction("GetBookingPrepayPayment", new { id = bookingPrepayPayment.Id }, bookingPrepayPayment);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!BookingPrepayPaymentExists(id))
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

                  throw new UnexpectedException(bookingPrepayPayment,e);
            }
        }

        // POST: api/BookingPrepayPayments
        [HttpPost]
        public async Task<IActionResult> PostBookingPrepayPayment([FromBody] BookingPrepayPayment bookingPrepayPayment)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                bookingPrepayPayment.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _bookingPrepayPayment.AddAsync(bookingPrepayPayment);
                return CreatedAtAction("GetBookingPrepayPayment", new { id = bookingPrepayPayment.Id }, bookingPrepayPayment);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(bookingPrepayPayment,e);
            }
          
        }

        // DELETE: api/BookingPrepayPayments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingPrepayPayment([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var bookingPrepayPayment = await _bookingPrepayPayment.FindAsync(id);
                if (bookingPrepayPayment == null)
                {
                    return NotFound();
                }

                await _bookingPrepayPayment.DeleteAsync(bookingPrepayPayment);

                return Ok(bookingPrepayPayment);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool BookingPrepayPaymentExists(long id)
        {
            return _bookingPrepayPayment.Any<BookingPrepayPayment>(e => e.Id == id);
        }

        //private IQueryable<BookingPrepayPayment> GetByCurrentSpaBranch(IQueryable<BookingPrepayPayment> data)
        //{
        //    var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

        //    if (currentSalonBranch != default || currentSalonBranch != 0)
        //    {
        //        data = data.Where(e => e.SalonBranchId == currentSalonBranch);
        //    }
        //    return data;
        //}
        //private IQueryable<BookingPrepayPayment> GetByCurrentSalon(IQueryable<BookingPrepayPayment> data)
        //{
        //    data = data.Where(e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID)));
        //    return data;
        //}
}
}


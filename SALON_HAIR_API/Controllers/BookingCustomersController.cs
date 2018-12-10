
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
    public class BookingCustomersController : CustomControllerBase
    {
        private readonly IBookingCustomer _bookingCustomer;
        private readonly IUser _user;

        public BookingCustomersController(IBookingCustomer bookingCustomer, IUser user)
        {
            _bookingCustomer = bookingCustomer;
            _user = user;
        }

        // GET: api/BookingCustomers
        [HttpGet]
        public IActionResult GetBookingCustomer(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _bookingCustomer.SearchAllFileds(keyword);
            var dataReturn =   _bookingCustomer.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/BookingCustomers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingCustomer([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var bookingCustomer = await _bookingCustomer.FindAsync(id);

                if (bookingCustomer == null)
                {
                    return NotFound();
                }
                return Ok(bookingCustomer);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/BookingCustomers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingCustomer([FromRoute] long id, [FromBody] BookingCustomer bookingCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != bookingCustomer.Id)
            {
                return BadRequest();
            }
            try
            {
                bookingCustomer.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _bookingCustomer.EditAsync(bookingCustomer);
                return CreatedAtAction("GetBookingCustomer", new { id = bookingCustomer.Id }, bookingCustomer);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!BookingCustomerExists(id))
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

                  throw new UnexpectedException(bookingCustomer,e);
            }
        }

        // POST: api/BookingCustomers
        [HttpPost]
        public async Task<IActionResult> PostBookingCustomer([FromBody] BookingCustomer bookingCustomer)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                bookingCustomer.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _bookingCustomer.AddAsync(bookingCustomer);
                return CreatedAtAction("GetBookingCustomer", new { id = bookingCustomer.Id }, bookingCustomer);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(bookingCustomer,e);
            }
          
        }

        // DELETE: api/BookingCustomers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingCustomer([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var bookingCustomer = await _bookingCustomer.FindAsync(id);
                if (bookingCustomer == null)
                {
                    return NotFound();
                }

                await _bookingCustomer.DeleteAsync(bookingCustomer);

                return Ok(bookingCustomer);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool BookingCustomerExists(long id)
        {
            return _bookingCustomer.Any<BookingCustomer>(e => e.Id == id);
        }
    }
}


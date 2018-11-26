
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
    public class BookingStatussController : CustomControllerBase
    {
        private readonly IBookingStatus _bookingStatus;
        private readonly IUser _user;

        public BookingStatussController(IBookingStatus bookingStatus, IUser user)
        {
            _bookingStatus = bookingStatus;
            _user = user;
        }

        // GET: api/BookingStatuss
        [HttpGet]
        public IActionResult GetBookingStatus(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _bookingStatus.SearchAllFileds(keyword);
            var dataReturn =   _bookingStatus.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/BookingStatuss/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingStatus([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var bookingStatus = await _bookingStatus.FindAsync(id);

                if (bookingStatus == null)
                {
                    return NotFound();
                }
                return Ok(bookingStatus);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/BookingStatuss/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingStatus([FromRoute] long id, [FromBody] BookingStatus bookingStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != bookingStatus.Id)
            {
                return BadRequest();
            }
            try
            {
                bookingStatus.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _bookingStatus.EditAsync(bookingStatus);
                return CreatedAtAction("GetBookingStatus", new { id = bookingStatus.Id }, bookingStatus);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!BookingStatusExists(id))
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

                  throw new UnexpectedException(bookingStatus,e);
            }
        }

        // POST: api/BookingStatuss
        [HttpPost]
        public async Task<IActionResult> PostBookingStatus([FromBody] BookingStatus bookingStatus)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                bookingStatus.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _bookingStatus.AddAsync(bookingStatus);
                return CreatedAtAction("GetBookingStatus", new { id = bookingStatus.Id }, bookingStatus);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(bookingStatus,e);
            }
          
        }

        // DELETE: api/BookingStatuss/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingStatus([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var bookingStatus = await _bookingStatus.FindAsync(id);
                if (bookingStatus == null)
                {
                    return NotFound();
                }

                await _bookingStatus.DeleteAsync(bookingStatus);

                return Ok(bookingStatus);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool BookingStatusExists(long id)
        {
            return _bookingStatus.Any<BookingStatus>(e => e.Id == id);
        }
    }
}



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
    public class BookingLogsController : CustomControllerBase
    {
        private readonly IBookingLog _bookingLog;
        private readonly IUser _user;

        public BookingLogsController(IBookingLog bookingLog, IUser user)
        {
            _bookingLog = bookingLog;
            _user = user;
        }

        // GET: api/BookingLogs
        [HttpGet]
        public IActionResult GetBookingLog(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _bookingLog.SearchAllFileds(keyword);
            var dataReturn =   _bookingLog.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/BookingLogs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingLog([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var bookingLog = await _bookingLog.FindAsync(id);

                if (bookingLog == null)
                {
                    return NotFound();
                }
                return Ok(bookingLog);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/BookingLogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingLog([FromRoute] long id, [FromBody] BookingLog bookingLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != bookingLog.Id)
            {
                return BadRequest();
            }
            try
            {
                bookingLog.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _bookingLog.EditAsync(bookingLog);
                return CreatedAtAction("GetBookingLog", new { id = bookingLog.Id }, bookingLog);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!BookingLogExists(id))
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

                  throw new UnexpectedException(bookingLog,e);
            }
        }

        // POST: api/BookingLogs
        [HttpPost]
        public async Task<IActionResult> PostBookingLog([FromBody] BookingLog bookingLog)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                bookingLog.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _bookingLog.AddAsync(bookingLog);
                return CreatedAtAction("GetBookingLog", new { id = bookingLog.Id }, bookingLog);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(bookingLog,e);
            }
          
        }

        // DELETE: api/BookingLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingLog([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var bookingLog = await _bookingLog.FindAsync(id);
                if (bookingLog == null)
                {
                    return NotFound();
                }

                await _bookingLog.DeleteAsync(bookingLog);

                return Ok(bookingLog);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool BookingLogExists(long id)
        {
            return _bookingLog.Any<BookingLog>(e => e.Id == id);
        }
    }
}


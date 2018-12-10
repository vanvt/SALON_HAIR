
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
    public class BookingCustomerServicesController : CustomControllerBase
    {
        private readonly IBookingCustomerService _bookingCustomerService;
        private readonly IUser _user;

        public BookingCustomerServicesController(IBookingCustomerService bookingCustomerService, IUser user)
        {
            _bookingCustomerService = bookingCustomerService;
            _user = user;
        }

        // GET: api/BookingCustomerServices
        [HttpGet]
        public IActionResult GetBookingCustomerService(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _bookingCustomerService.SearchAllFileds(keyword);
            var dataReturn =   _bookingCustomerService.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/BookingCustomerServices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingCustomerService([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var bookingCustomerService = await _bookingCustomerService.FindAsync(id);

                if (bookingCustomerService == null)
                {
                    return NotFound();
                }
                return Ok(bookingCustomerService);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/BookingCustomerServices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingCustomerService([FromRoute] long id, [FromBody] BookingCustomerService bookingCustomerService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != bookingCustomerService.Id)
            {
                return BadRequest();
            }
            try
            {
                bookingCustomerService.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _bookingCustomerService.EditAsync(bookingCustomerService);
                return CreatedAtAction("GetBookingCustomerService", new { id = bookingCustomerService.Id }, bookingCustomerService);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!BookingCustomerServiceExists(id))
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

                  throw new UnexpectedException(bookingCustomerService,e);
            }
        }

        // POST: api/BookingCustomerServices
        [HttpPost]
        public async Task<IActionResult> PostBookingCustomerService([FromBody] BookingCustomerService bookingCustomerService)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                bookingCustomerService.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _bookingCustomerService.AddAsync(bookingCustomerService);
                return CreatedAtAction("GetBookingCustomerService", new { id = bookingCustomerService.Id }, bookingCustomerService);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(bookingCustomerService,e);
            }
          
        }

        // DELETE: api/BookingCustomerServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingCustomerService([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var bookingCustomerService = await _bookingCustomerService.FindAsync(id);
                if (bookingCustomerService == null)
                {
                    return NotFound();
                }

                await _bookingCustomerService.DeleteAsync(bookingCustomerService);

                return Ok(bookingCustomerService);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool BookingCustomerServiceExists(long id)
        {
            return _bookingCustomerService.Any<BookingCustomerService>(e => e.Id == id);
        }
    }
}


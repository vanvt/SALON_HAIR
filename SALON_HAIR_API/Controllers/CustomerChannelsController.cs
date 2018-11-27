
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
    public class CustomerChannelsController : CustomControllerBase
    {
        private readonly ICustomerChannel _customerChannel;
        private readonly IUser _user;

        public CustomerChannelsController(ICustomerChannel customerChannel, IUser user)
        {
            _customerChannel = customerChannel;
            _user = user;
        }

        // GET: api/CustomerChannels
        [HttpGet]
        public IActionResult GetCustomerChannel(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _customerChannel.SearchAllFileds(keyword);
            var dataReturn =   _customerChannel.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/CustomerChannels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerChannel([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var customerChannel = await _customerChannel.FindAsync(id);

                if (customerChannel == null)
                {
                    return NotFound();
                }
                return Ok(customerChannel);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/CustomerChannels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerChannel([FromRoute] long id, [FromBody] CustomerChannel customerChannel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != customerChannel.Id)
            {
                return BadRequest();
            }
            try
            {
                customerChannel.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _customerChannel.EditAsync(customerChannel);
                return CreatedAtAction("GetCustomerChannel", new { id = customerChannel.Id }, customerChannel);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerChannelExists(id))
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

                  throw new UnexpectedException(customerChannel,e);
            }
        }

        // POST: api/CustomerChannels
        [HttpPost]
        public async Task<IActionResult> PostCustomerChannel([FromBody] CustomerChannel customerChannel)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                customerChannel.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _customerChannel.AddAsync(customerChannel);
                return CreatedAtAction("GetCustomerChannel", new { id = customerChannel.Id }, customerChannel);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(customerChannel,e);
            }
          
        }

        // DELETE: api/CustomerChannels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerChannel([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var customerChannel = await _customerChannel.FindAsync(id);
                if (customerChannel == null)
                {
                    return NotFound();
                }

                await _customerChannel.DeleteAsync(customerChannel);

                return Ok(customerChannel);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool CustomerChannelExists(long id)
        {
            return _customerChannel.Any<CustomerChannel>(e => e.Id == id);
        }
    }
}


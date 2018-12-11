
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
    public class CustomerSourcesController : CustomControllerBase
    {
        private readonly ICustomerSource _customerSource;
        private readonly IUser _user;

        public CustomerSourcesController(ICustomerSource customerSource, IUser user)
        {
            _customerSource = customerSource;
            _user = user;
        }

        // GET: api/CustomerSources
        [HttpGet]
        public IActionResult GetCustomerSource(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _customerSource.SearchAllFileds(keyword).Where
                (e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals("salonId"))); 
            var dataReturn =   _customerSource.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/CustomerSources/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerSource([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var customerSource = await _customerSource.FindAsync(id);

                if (customerSource == null)
                {
                    return NotFound();
                }
                return Ok(customerSource);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/CustomerSources/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerSource([FromRoute] long id, [FromBody] CustomerSource customerSource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != customerSource.Id)
            {
                return BadRequest();
            }
            try
            {
                customerSource.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _customerSource.EditAsync(customerSource);
                return CreatedAtAction("GetCustomerSource", new { id = customerSource.Id }, customerSource);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerSourceExists(id))
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

                  throw new UnexpectedException(customerSource,e);
            }
        }

        // POST: api/CustomerSources
        [HttpPost]
        public async Task<IActionResult> PostCustomerSource([FromBody] CustomerSource customerSource)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                customerSource.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _customerSource.AddAsync(customerSource);
                return CreatedAtAction("GetCustomerSource", new { id = customerSource.Id }, customerSource);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(customerSource,e);
            }
          
        }

        // DELETE: api/CustomerSources/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerSource([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var customerSource = await _customerSource.FindAsync(id);
                if (customerSource == null)
                {
                    return NotFound();
                }

                await _customerSource.DeleteAsync(customerSource);

                return Ok(customerSource);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool CustomerSourceExists(long id)
        {
            return _customerSource.Any<CustomerSource>(e => e.Id == id);
        }
    }
}


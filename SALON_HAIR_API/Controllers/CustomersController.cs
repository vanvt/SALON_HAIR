
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
    public class CustomersController : CustomControllerBase
    {
        private readonly ICustomer _customer;
        private readonly IUser _user;

        public CustomersController(ICustomer customer, IUser user)
        {
            _customer = customer;
            _user = user;
        }

        // GET: api/Customers
        [HttpGet]
        public IActionResult GetCustomer(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _customer.SearchAllFileds(keyword);
            var dataReturn =   _customer.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var customer = await _customer.FindAsync(id);

                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] long id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != customer.Id)
            {
                return BadRequest();
            }
            try
            {
                customer.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _customer.EditAsync(customer);
                return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

                  throw new UnexpectedException(customer,e);
            }
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                customer.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _customer.AddAsync(customer);
                return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(customer,e);
            }
          
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var customer = await _customer.FindAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }

                await _customer.DeleteAsync(customer);

                return Ok(customer);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool CustomerExists(long id)
        {
            return _customer.Any<Customer>(e => e.Id == id);
        }
    }
}

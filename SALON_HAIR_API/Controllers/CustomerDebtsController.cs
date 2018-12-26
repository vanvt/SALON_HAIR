
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
    public class CustomerDebtsController : CustomControllerBase
    {
        private readonly ICustomerDebt _customerDebt;
        private readonly IUser _user;

        public CustomerDebtsController(ICustomerDebt customerDebt, IUser user)
        {
            _customerDebt = customerDebt;
            _user = user;
        }

        // GET: api/CustomerDebts
        [HttpGet]
        public IActionResult GetCustomerDebt(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _customerDebt.SearchAllFileds(keyword);
            data = GetByCurrentSalon(data);
            data = GetByCurrentSpaBranch(data);
            data = data.OrderBy(e => e.Updated);
            var dataReturn =   _customerDebt.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/CustomerDebts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerDebt([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var customerDebt = await _customerDebt.FindAsync(id);

                if (customerDebt == null)
                {
                    return NotFound();
                }
                return Ok(customerDebt);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/CustomerDebts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerDebt([FromRoute] long id, [FromBody] CustomerDebt customerDebt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != customerDebt.Id)
            {
                return BadRequest();
            }
            try
            {
                customerDebt.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _customerDebt.EditAsync(customerDebt);
                return CreatedAtAction("GetCustomerDebt", new { id = customerDebt.Id }, customerDebt);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerDebtExists(id))
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

                  throw new UnexpectedException(customerDebt,e);
            }
        }

        // POST: api/CustomerDebts
        [HttpPost]
        public async Task<IActionResult> PostCustomerDebt([FromBody] CustomerDebt customerDebt)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                customerDebt.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _customerDebt.AddAsync(customerDebt);
                return CreatedAtAction("GetCustomerDebt", new { id = customerDebt.Id }, customerDebt);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(customerDebt,e);
            }
          
        }

        // DELETE: api/CustomerDebts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerDebt([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var customerDebt = await _customerDebt.FindAsync(id);
                if (customerDebt == null)
                {
                    return NotFound();
                }

                await _customerDebt.DeleteAsync(customerDebt);

                return Ok(customerDebt);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool CustomerDebtExists(long id)
        {
            return _customerDebt.Any<CustomerDebt>(e => e.Id == id);
        }

        private IQueryable<CustomerDebt> GetByCurrentSpaBranch(IQueryable<CustomerDebt> data)
        {
            var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

            if (currentSalonBranch != default || currentSalonBranch != 0)
            {
                data = data.Where(e => e.SalonBranchId == currentSalonBranch);
            }
            return data;
        }
        private IQueryable<CustomerDebt> GetByCurrentSalon(IQueryable<CustomerDebt> data)
        {
            data = data.Where(e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID)));
            return data;
        }
}
}


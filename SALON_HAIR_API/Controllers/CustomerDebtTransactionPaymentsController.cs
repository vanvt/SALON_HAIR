
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
    public class CustomerDebtTransactionPaymentsController : CustomControllerBase
    {
        private readonly ICustomerDebtTransactionPayment _customerDebtTransactionPayment;
        private readonly IUser _user;

        public CustomerDebtTransactionPaymentsController(ICustomerDebtTransactionPayment customerDebtTransactionPayment, IUser user)
        {
            _customerDebtTransactionPayment = customerDebtTransactionPayment;
            _user = user;
        }

        // GET: api/CustomerDebtTransactionPayments
        [HttpGet]
        public IActionResult GetCustomerDebtTransactionPayment(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _customerDebtTransactionPayment.SearchAllFileds(keyword);
            var dataReturn =   _customerDebtTransactionPayment.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/CustomerDebtTransactionPayments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerDebtTransactionPayment([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var customerDebtTransactionPayment = await _customerDebtTransactionPayment.FindAsync(id);

                if (customerDebtTransactionPayment == null)
                {
                    return NotFound();
                }
                return Ok(customerDebtTransactionPayment);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/CustomerDebtTransactionPayments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerDebtTransactionPayment([FromRoute] long id, [FromBody] CustomerDebtTransactionPayment customerDebtTransactionPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != customerDebtTransactionPayment.Id)
            {
                return BadRequest();
            }
            try
            {
                customerDebtTransactionPayment.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _customerDebtTransactionPayment.EditAsync(customerDebtTransactionPayment);
                return CreatedAtAction("GetCustomerDebtTransactionPayment", new { id = customerDebtTransactionPayment.Id }, customerDebtTransactionPayment);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerDebtTransactionPaymentExists(id))
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

                  throw new UnexpectedException(customerDebtTransactionPayment,e);
            }
        }

        // POST: api/CustomerDebtTransactionPayments
        [HttpPost]
        public async Task<IActionResult> PostCustomerDebtTransactionPayment([FromBody] CustomerDebtTransactionPayment customerDebtTransactionPayment)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                customerDebtTransactionPayment.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _customerDebtTransactionPayment.AddAsync(customerDebtTransactionPayment);
                return CreatedAtAction("GetCustomerDebtTransactionPayment", new { id = customerDebtTransactionPayment.Id }, customerDebtTransactionPayment);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(customerDebtTransactionPayment,e);
            }
          
        }

        // DELETE: api/CustomerDebtTransactionPayments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerDebtTransactionPayment([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var customerDebtTransactionPayment = await _customerDebtTransactionPayment.FindAsync(id);
                if (customerDebtTransactionPayment == null)
                {
                    return NotFound();
                }

                await _customerDebtTransactionPayment.DeleteAsync(customerDebtTransactionPayment);

                return Ok(customerDebtTransactionPayment);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool CustomerDebtTransactionPaymentExists(long id)
        {
            return _customerDebtTransactionPayment.Any<CustomerDebtTransactionPayment>(e => e.Id == id);
        }

        private IQueryable<CustomerDebtTransactionPayment> GetByCurrentSpaBranch(IQueryable<CustomerDebtTransactionPayment> data)
        {
            var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

            if (currentSalonBranch != default || currentSalonBranch != 0)
            {
                data = data.Where(e => e.SalonBranchId == currentSalonBranch);
            }
            return data;
        }
        private IQueryable<CustomerDebtTransactionPayment> GetByCurrentSalon(IQueryable<CustomerDebtTransactionPayment> data)
        {
            data = data.Where(e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID)));
            return data;
        }
}
}


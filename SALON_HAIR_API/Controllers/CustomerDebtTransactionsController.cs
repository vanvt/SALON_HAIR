
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
    public class CustomerDebtTransactionsController : CustomControllerBase
    {
        private readonly ICustomerDebtTransaction _customerDebtTransaction;
        private readonly IUser _user;

        public CustomerDebtTransactionsController(ICustomerDebtTransaction customerDebtTransaction, IUser user)
        {
            _customerDebtTransaction = customerDebtTransaction;
            _user = user;
        }

        // GET: api/CustomerDebtTransactions
        [HttpGet]
        public IActionResult GetCustomerDebtTransaction(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _customerDebtTransaction.SearchAllFileds(keyword);
            var dataReturn =   _customerDebtTransaction.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/CustomerDebtTransactions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerDebtTransaction([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var customerDebtTransaction = await _customerDebtTransaction.FindAsync(id);

                if (customerDebtTransaction == null)
                {
                    return NotFound();
                }
                return Ok(customerDebtTransaction);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/CustomerDebtTransactions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerDebtTransaction([FromRoute] long id, [FromBody] CustomerDebtTransaction customerDebtTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != customerDebtTransaction.Id)
            {
                return BadRequest();
            }
            try
            {
                customerDebtTransaction.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _customerDebtTransaction.EditAsync(customerDebtTransaction);
                return CreatedAtAction("GetCustomerDebtTransaction", new { id = customerDebtTransaction.Id }, customerDebtTransaction);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerDebtTransactionExists(id))
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

                  throw new UnexpectedException(customerDebtTransaction,e);
            }
        }

        // POST: api/CustomerDebtTransactions
        [HttpPost]
        public async Task<IActionResult> PostCustomerDebtTransaction([FromBody] CustomerDebtTransaction customerDebtTransaction)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                customerDebtTransaction.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _customerDebtTransaction.AddAsync(customerDebtTransaction);
                return CreatedAtAction("GetCustomerDebtTransaction", new { id = customerDebtTransaction.Id }, customerDebtTransaction);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(customerDebtTransaction,e);
            }
          
        }

        // DELETE: api/CustomerDebtTransactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerDebtTransaction([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var customerDebtTransaction = await _customerDebtTransaction.FindAsync(id);
                if (customerDebtTransaction == null)
                {
                    return NotFound();
                }

                await _customerDebtTransaction.DeleteAsync(customerDebtTransaction);

                return Ok(customerDebtTransaction);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool CustomerDebtTransactionExists(long id)
        {
            return _customerDebtTransaction.Any<CustomerDebtTransaction>(e => e.Id == id);
        }

        private IQueryable<CustomerDebtTransaction> GetByCurrentSpaBranch(IQueryable<CustomerDebtTransaction> data)
        {
            var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

            if (currentSalonBranch != default || currentSalonBranch != 0)
            {
                data = data.Where(e => e.SalonBranchId == currentSalonBranch);
            }
            return data;
        }
        private IQueryable<CustomerDebtTransaction> GetByCurrentSalon(IQueryable<CustomerDebtTransaction> data)
        {
            data = data.Where(e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID)));
            return data;
        }
}
}


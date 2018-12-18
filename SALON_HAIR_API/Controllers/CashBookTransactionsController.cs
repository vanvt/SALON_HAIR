
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
    public class CashBookTransactionsController : CustomControllerBase
    {
        private readonly ICashBookTransaction _cashBookTransaction;
        private readonly IUser _user;
        private readonly ISysObjectAutoIncreament _sysObjectAutoIncreament;
        public CashBookTransactionsController(ISysObjectAutoIncreament sysObjectAutoIncreament ,ICashBookTransaction cashBookTransaction, IUser user)
        {
            _sysObjectAutoIncreament = sysObjectAutoIncreament;
            _cashBookTransaction = cashBookTransaction;
            _user = user;
        }

        // GET: api/CashBookTransactions
        [HttpGet]
        public IActionResult GetCashBookTransaction(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "",string date = "")
        {
            var data = _cashBookTransaction.SearchAllFileds(keyword);          
            data = GetByCurrentSpaBranch(data);
            data = GetByCurrentSalon(data);
            data = data.Where(e => e.Created.Value.Date == GetDateRangeQuery(date).Date);
            var dataReturn =   _cashBookTransaction.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/CashBookTransactions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCashBookTransaction([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var cashBookTransaction = await _cashBookTransaction.FindAsync(id);

                if (cashBookTransaction == null)
                {
                    return NotFound();
                }
                return Ok(cashBookTransaction);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/CashBookTransactions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCashBookTransaction([FromRoute] long id, [FromBody] CashBookTransaction cashBookTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != cashBookTransaction.Id)
            {
                return BadRequest();
            }
            try
            {
                cashBookTransaction.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _cashBookTransaction.EditAsync(cashBookTransaction);
                return CreatedAtAction("GetCashBookTransaction", new { id = cashBookTransaction.Id }, cashBookTransaction);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!CashBookTransactionExists(id))
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

                  throw new UnexpectedException(cashBookTransaction,e);
            }
        }

        // POST: api/CashBookTransactions
        [HttpPost]
        public async Task<IActionResult> PostCashBookTransaction([FromBody] CashBookTransaction cashBookTransaction)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //cashBookTransaction.Code = "ES" + _sysObjectAutoIncreament.
                //   GetCodeByObjectAsync(nameof(CashBookTransaction), cashBookTransaction.SalonId).
                //   Result.ObjectIndex.ToString("000000");
                cashBookTransaction.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _cashBookTransaction.AddAsync(cashBookTransaction);
                return CreatedAtAction("GetCashBookTransaction", new { id = cashBookTransaction.Id }, cashBookTransaction);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(cashBookTransaction,e);
            }
          
        }

        // DELETE: api/CashBookTransactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCashBookTransaction([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var cashBookTransaction = await _cashBookTransaction.FindAsync(id);
                if (cashBookTransaction == null)
                {
                    return NotFound();
                }

                await _cashBookTransaction.DeleteAsync(cashBookTransaction);

                return Ok(cashBookTransaction);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool CashBookTransactionExists(long id)
        {
            return _cashBookTransaction.Any<CashBookTransaction>(e => e.Id == id);
        }

        private IQueryable<CashBookTransaction> GetByCurrentSpaBranch(IQueryable<CashBookTransaction> data)
        {
            var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

            if (currentSalonBranch != default || currentSalonBranch != 0)
            {
                data = data.Where(e => e.SalonBranchId == currentSalonBranch);
            }
            return data;
        }
        private IQueryable<CashBookTransaction> GetByCurrentSalon(IQueryable<CashBookTransaction> data)
        {
            data = data.Where(e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID)));
            return data;
        }
        private DateTime GetDateRangeQuery(string date)
        {
            date += "";

            var st = DateTime.Now;
            if (!string.IsNullOrEmpty(date))
            {
                st = DateTime.Parse(date);
            }

            return st;
        }
    }
}


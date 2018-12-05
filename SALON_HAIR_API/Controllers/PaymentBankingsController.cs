
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
    public class PaymentBankingsController : CustomControllerBase
    {
        private readonly IPaymentBanking _paymentBanking;
        private readonly IUser _user;

        public PaymentBankingsController(IPaymentBanking paymentBanking, IUser user)
        {
            _paymentBanking = paymentBanking;
            _user = user;
        }

        // GET: api/PaymentBankings
        [HttpGet]
        public IActionResult GetPaymentBanking(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _paymentBanking.SearchAllFileds(keyword).Where
                (e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals("salonId"))); ;
            var dataReturn =   _paymentBanking.LoadAllInclude(data);
            dataReturn = dataReturn.Include(e => e.PaymentBankingMethod).ThenInclude(e => e.PaymentMethod);
            return OkList(dataReturn);
        }
        // GET: api/PaymentBankings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentBanking([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var paymentBanking = await _paymentBanking.FindAsync(id);

                if (paymentBanking == null)
                {
                    return NotFound();
                }
                return Ok(paymentBanking);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/PaymentBankings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentBanking([FromRoute] long id, [FromBody] PaymentBanking paymentBanking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != paymentBanking.Id)
            {
                return BadRequest();
            }
            try
            {
                paymentBanking.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _paymentBanking.EditAsync(paymentBanking);
                return CreatedAtAction("GetPaymentBanking", new { id = paymentBanking.Id }, paymentBanking);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentBankingExists(id))
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

                  throw new UnexpectedException(paymentBanking,e);
            }
        }

        // POST: api/PaymentBankings
        [HttpPost]
        public async Task<IActionResult> PostPaymentBanking([FromBody] PaymentBanking paymentBanking)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                paymentBanking.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _paymentBanking.AddAsync(paymentBanking);
                return CreatedAtAction("GetPaymentBanking", new { id = paymentBanking.Id }, paymentBanking);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(paymentBanking,e);
            }
          
        }

        // DELETE: api/PaymentBankings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentBanking([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var paymentBanking = await _paymentBanking.FindAsync(id);
                if (paymentBanking == null)
                {
                    return NotFound();
                }

                await _paymentBanking.DeleteAsync(paymentBanking);

                return Ok(paymentBanking);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool PaymentBankingExists(long id)
        {
            return _paymentBanking.Any<PaymentBanking>(e => e.Id == id);
        }
    }
}


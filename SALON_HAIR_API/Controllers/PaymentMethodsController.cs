
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
    public class PaymentMethodsController : CustomControllerBase
    {
        private readonly IPaymentMethod _paymentMethod;
        private readonly IUser _user;

        public PaymentMethodsController(IPaymentMethod paymentMethod, IUser user)
        {
            _paymentMethod = paymentMethod;
            _user = user;
        }

        // GET: api/PaymentMethods
        [HttpGet]
        public IActionResult GetPaymentMethod(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _paymentMethod.SearchAllFileds(keyword);
            data = GetByCurrentSalon(data);
            //data = data.Where(e => e.Status.Equals(OBJECTSTATUS.ENABLE));
            data = data.Include(e => e.PaymentBankingMethod).ThenInclude(e=>e.Banking);         
            return OkList(data);
        }
        // GET: api/PaymentMethods/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentMethod([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var paymentMethod = await _paymentMethod.FindAsync(id);

                if (paymentMethod == null)
                {
                    return NotFound();
                }
                return Ok(paymentMethod);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/PaymentMethods/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentMethod([FromRoute] long id, [FromBody] PaymentMethod paymentMethod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != paymentMethod.Id)
            {
                return BadRequest();
            }
            try
            {
                paymentMethod.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                await _paymentMethod.EditAsync(paymentMethod);
                return CreatedAtAction("GetPaymentMethod", new { id = paymentMethod.Id }, paymentMethod);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentMethodExists(id))
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

                  throw new UnexpectedException(paymentMethod,e);
            }
        }

        // POST: api/PaymentMethods
        [HttpPost]
        public async Task<IActionResult> PostPaymentMethod([FromBody] PaymentMethod paymentMethod)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                paymentMethod.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                await _paymentMethod.AddAsync(paymentMethod);
                return CreatedAtAction("GetPaymentMethod", new { id = paymentMethod.Id }, paymentMethod);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(paymentMethod,e);
            }
          
        }

        // DELETE: api/PaymentMethods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var paymentMethod = await _paymentMethod.FindAsync(id);
                if (paymentMethod == null)
                {
                    return NotFound();
                }

                await _paymentMethod.DeleteAsync(paymentMethod);

                return Ok(paymentMethod);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool PaymentMethodExists(long id)
        {
            return _paymentMethod.Any<PaymentMethod>(e => e.Id == id);
        }
        private IQueryable<PaymentMethod> GetByCurrentSalon(IQueryable<PaymentMethod> data)
        {
            data = data.Where(e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID)));
            return data;
        }
    }
}


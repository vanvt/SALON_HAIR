
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
    public class InvoicePaymentsController : CustomControllerBase
    {
        private readonly IInvoicePayment _invoicePayment;
        private readonly IUser _user;

        public InvoicePaymentsController(IInvoicePayment invoicePayment, IUser user)
        {
            _invoicePayment = invoicePayment;
            _user = user;
        }

        // GET: api/InvoicePayments
        [HttpGet]
        public IActionResult GetInvoicePayment(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _invoicePayment.SearchAllFileds(keyword);
            var dataReturn =   _invoicePayment.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/InvoicePayments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoicePayment([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var invoicePayment = await _invoicePayment.FindAsync(id);

                if (invoicePayment == null)
                {
                    return NotFound();
                }
                return Ok(invoicePayment);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/InvoicePayments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoicePayment([FromRoute] long id, [FromBody] InvoicePayment invoicePayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != invoicePayment.Id)
            {
                return BadRequest();
            }
            try
            {
                invoicePayment.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _invoicePayment.EditAsync(invoicePayment);
                return CreatedAtAction("GetInvoicePayment", new { id = invoicePayment.Id }, invoicePayment);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!InvoicePaymentExists(id))
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

                  throw new UnexpectedException(invoicePayment,e);
            }
        }

        // POST: api/InvoicePayments
        [HttpPost]
        public async Task<IActionResult> PostInvoicePayment([FromBody] InvoicePayment invoicePayment)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                invoicePayment.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _invoicePayment.AddAsync(invoicePayment);
                return CreatedAtAction("GetInvoicePayment", new { id = invoicePayment.Id }, invoicePayment);
            }
            catch (Exception e)
            {
                throw new UnexpectedException(invoicePayment,e);
            }          
        }

        // DELETE: api/InvoicePayments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoicePayment([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var invoicePayment = await _invoicePayment.FindAsync(id);
                if (invoicePayment == null)
                {
                    return NotFound();
                }

                await _invoicePayment.DeleteAsync(invoicePayment);

                return Ok(invoicePayment);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool InvoicePaymentExists(long id)
        {
            return _invoicePayment.Any<InvoicePayment>(e => e.Id == id);
        }
    }
}


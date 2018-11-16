
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
    public class InvoicesController : CustomControllerBase
    {
        private readonly IInvoice _invoice;
        private readonly IUser _user;

        public InvoicesController(IInvoice invoice, IUser user)
        {
            _invoice = invoice;
            _user = user;
        }

        // GET: api/Invoices
        [HttpGet]
        public IActionResult GetInvoice(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "",bool isDisplay=false)
        {
            var data = _invoice.SearchAllFileds(keyword);
            if (isDisplay)
            {
                data = data.Where(e => e.IsDisplay.Value).Where(e => e.Created.Value.Date == DateTime.Now.Date);
            }
            var dataReturn =   _invoice.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoice([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var invoice = await _invoice.FindAsync(id);

                if (invoice == null)
                {
                    return NotFound();
                }
                return Ok(invoice);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/Invoices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice([FromRoute] long id, [FromBody] Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != invoice.Id)
            {
                return BadRequest();
            }
            try
            {
                invoice.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _invoice.EditAsync(invoice);
                
                return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoice);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

                  throw new UnexpectedException(invoice,e);
            }
        }

        // POST: api/Invoices
        [HttpPost]
        public async Task<IActionResult> PostInvoice([FromBody] Invoice invoice)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                invoice.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _invoice.AddAsync(invoice);
                return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoice);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(invoice,e);
            }
          
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var invoice = await _invoice.FindAsync(id);
                if (invoice == null)
                {
                    return NotFound();
                }

                await _invoice.DeleteAsync(invoice);

                return Ok(invoice);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool InvoiceExists(long id)
        {
            return _invoice.Any<Invoice>(e => e.Id == id);
        }
    }
}



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
    public class InvoiceDetailsController : CustomControllerBase
    {
        private readonly IInvoiceDetail _invoiceDetail;
        private readonly IUser _user;

        public InvoiceDetailsController(IInvoiceDetail invoiceDetail, IUser user)
        {
            _invoiceDetail = invoiceDetail;
            _user = user;
        }

        // GET: api/InvoiceDetails
        [HttpGet]
        public IActionResult GetInvoiceDetail(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _invoiceDetail.SearchAllFileds(keyword);
            var dataReturn =   _invoiceDetail.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/InvoiceDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceDetail([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var invoiceDetail = await _invoiceDetail.FindAsync(id);

                if (invoiceDetail == null)
                {
                    return NotFound();
                }
                return Ok(invoiceDetail);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/InvoiceDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceDetail([FromRoute] long id, [FromBody] InvoiceDetail invoiceDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != invoiceDetail.Id)
            {
                return BadRequest();
            }
            try
            {
                invoiceDetail.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _invoiceDetail.EditAsync(invoiceDetail);
                return CreatedAtAction("GetInvoiceDetail", new { id = invoiceDetail.Id }, invoiceDetail);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceDetailExists(id))
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

                  throw new UnexpectedException(invoiceDetail,e);
            }
        }

        // POST: api/InvoiceDetails
        [HttpPost]
        public async Task<IActionResult> PostInvoiceDetail([FromBody] InvoiceDetail invoiceDetail)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                invoiceDetail.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _invoiceDetail.AddAsync(invoiceDetail);
                return CreatedAtAction("GetInvoiceDetail", new { id = invoiceDetail.Id }, invoiceDetail);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(invoiceDetail,e);
            }
          
        }

        // DELETE: api/InvoiceDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceDetail([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var invoiceDetail = await _invoiceDetail.FindAsync(id);
                if (invoiceDetail == null)
                {
                    return NotFound();
                }

                await _invoiceDetail.DeleteAsync(invoiceDetail);

                return Ok(invoiceDetail);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool InvoiceDetailExists(long id)
        {
            return _invoiceDetail.Any<InvoiceDetail>(e => e.Id == id);
        }
    }
}


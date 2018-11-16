
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
using SALON_HAIR_API.ViewModels;

namespace SALON_HAIR_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceStaffArrangementsController : CustomControllerBase
    {
        private readonly IInvoiceStaffArrangement _invoiceStaffArrangement;
        private readonly IUser _user;
        private readonly IInvoice _invoice;

        public InvoiceStaffArrangementsController(IInvoice invoice,IInvoiceStaffArrangement invoiceStaffArrangement, IUser user)
        {
            _invoice = invoice;
            _invoiceStaffArrangement = invoiceStaffArrangement;
            _user = user;
        }

        // GET: api/InvoiceStaffArrangements
        [HttpGet]
        public IActionResult GetInvoiceStaffArrangement(long invoiceId)
        {

            var data = _invoiceStaffArrangement.GetAll().Where(e=>e.InvoiceId==invoiceId);
            var dataReturn =   _invoiceStaffArrangement.LoadAllInclude(data);
            var invoice= _invoice.Find(invoiceId);

            InvoiceStaffArrangementVM invoiceStaffArrangementVM = new InvoiceStaffArrangementVM {
                Id = invoiceId,
                InvoiceStaffArrangements = dataReturn,               
                Salesman = invoice.Salesman,
                Note = invoice.Note,
                SalesmanId = invoice.SalesmanId
            };

            return Ok(invoiceStaffArrangementVM);
        }       
        // PUT: api/InvoiceStaffArrangements/5
        [HttpPut]
        public async Task<IActionResult> PutInvoiceStaffArrangement([FromRoute] long id, [FromBody] InvoiceStaffArrangementVMPut invoiceStaffArrangement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != invoiceStaffArrangement.Id)
            {
                return BadRequest();
            }
            try
            {
                invoiceStaffArrangement.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                var invoice = _invoice.Find(invoiceStaffArrangement.Id);
                invoice.Note = invoiceStaffArrangement.Note;
                invoice.SalesmanId = invoiceStaffArrangement.SalesmanId;
                await _invoice.EditAsync(invoice);
                await _invoiceStaffArrangement.EditRangeAsync(invoiceStaffArrangement.InvoiceStaffArrangements);
                return CreatedAtAction("GetInvoiceStaffArrangement", new { id = invoiceStaffArrangement.Id }, invoiceStaffArrangement);
            }            
            catch (Exception e)
            {
                  throw new UnexpectedException(invoiceStaffArrangement,e);
            }
        }      
    }
}


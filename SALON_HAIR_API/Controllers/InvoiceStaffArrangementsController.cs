
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
        [HttpGet("{id}")]
        public IActionResult GetInvoiceStaffArrangement(long id)
        {
            var data = _invoiceStaffArrangement.GetAll().Where(e => e.InvoiceId == id && !e.InvoiceDetail.Status.Equals("DELETED"));

            var dataReturn =   _invoiceStaffArrangement.LoadAllInclude(data,nameof(Invoice),nameof(InvoiceDetail));
            //dataReturn = _invoiceStaffArrangement.LoadAllInclude(dataReturn);
            var invoice= _invoice.Find(id);

            InvoiceStaffArrangementVM invoiceStaffArrangementVM = new InvoiceStaffArrangementVM {
                Id = id,
                InvoiceStaffArrangements = dataReturn,               
                Salesman = invoice.Salesman,
                Note = invoice.Note,
                SalesmanId = invoice.SalesmanId

            };

            return Ok(invoiceStaffArrangementVM);
        }
        // PUT: api/InvoiceStaffArrangements/5
        [HttpPut("{id}")]
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
                invoiceStaffArrangement.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                var invoice = _invoice.Find(invoiceStaffArrangement.Id);
                invoice.Note = invoiceStaffArrangement.Note;
                invoice.SalesmanId = invoiceStaffArrangement.SalesmanId;
                await _invoice.EditAsync(invoice);
                await _invoiceStaffArrangement.EditRangeAsync(invoiceStaffArrangement.InvoiceStaffArrangements);

                var data = _invoiceStaffArrangement.GetAll().Where(e => e.InvoiceId == id && !e.InvoiceDetail.Status.Equals("DELETED"));
                var dataReturn = _invoiceStaffArrangement.LoadAllCollecttion(data);
                dataReturn = _invoiceStaffArrangement.LoadAllInclude(dataReturn);
                invoiceStaffArrangement.InvoiceStaffArrangements = dataReturn.ToList() ;
                return CreatedAtAction("GetInvoiceStaffArrangement", new { id = invoiceStaffArrangement.Id }, invoiceStaffArrangement);

            }
            catch (Exception e)
            {
                  throw new UnexpectedException(invoiceStaffArrangement,e);
            }
        }      
    }
}


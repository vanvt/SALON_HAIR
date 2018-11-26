
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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using SALON_HAIR_API.ViewModels;

namespace SALON_HAIR_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class InvoicesController : CustomControllerBase
    {
        private readonly IInvoice _invoice;
        private readonly IUser _user;
        private readonly ICustomerPackage _customerPackage;
        public InvoicesController(ICustomerPackage customerPackage,IInvoice invoice, IUser user)
        {
            _customerPackage = customerPackage;
            _invoice = invoice;
            _user = user;
        }
        // GET: api/Invoices
        [HttpGet]
        public IActionResult GetInvoice(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "",bool isDisplay=false,string date ="")
        {
             date += "";
            var datetime = DateTime.Now;            
            var data = _invoice.SearchAllFileds(keyword);           
            DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out datetime);
            if (datetime == new DateTime())
            {
                datetime = DateTime.Now;
            }
            data = data.Where(e => e.Created.Value.Date == datetime.Date);
           
            if (isDisplay)
            {
                data = data.Where(e => e.IsDisplay.Value);                        
            }
            var dataReturn = _invoice.LoadAllCollecttion(data).Include(e=>e.Customer);
            
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

             
               // var customerPackge = _customerPackage.FindBy(e => e.Inove.InvoiceStatusId == 2 && e.NumberRemaining  > 0).Include(e => e.Package);
                var dataReturn = _invoice.LoadAllCollecttion(invoice);
                dataReturn = _invoice.LoadAllReference(dataReturn);
                //dataReturn.CustomerPackage = customerPackge.ToList();
                return Ok(dataReturn);
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
                invoice.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _invoice.EditAsync(invoice);
                var customerPackge = _customerPackage.FindBy(e => e.Inove.InvoiceStatusId == 2 && e.NumberRemaining  > 0).Include(e=>e.Package);
                var dataReturn = _invoice.LoadAllCollecttion(invoice);
                dataReturn.CustomerPackage = customerPackge.ToList();
                return CreatedAtAction("GetInvoice", new { id = invoice.Id }, dataReturn);
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
                invoice.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                invoice.SalonId =  long.Parse( JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("salonId")));
                await _invoice.AddAsync(invoice);
                invoice = _invoice.LoadAllReference(invoice);
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
        [HttpPut("show-invoice")]
        public async Task<IActionResult> ShowInvoice([FromBody] ListInvoiceIdVM invoiceids)
        {
            //return null;
            try
            {
               var invoces = _invoice.FindBy(e=> invoiceids.Ids.Contains(e.Id));
                await invoces.ForEachAsync(e => e.IsDisplay = true);
                await _invoice.EditRangeAsync(invoces);
               //await Task.WhenAll(t1,t2);
               return Ok(invoces);

            }
            catch (Exception e)
            {

                throw new UnexpectedException(invoiceids, e);
            }

        }
        [HttpPut("pay-invoice/{id}")]
        public async Task<IActionResult> PayInvoice([FromRoute] long id, [FromBody] Invoice invoice)
        {
            //return null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != invoice.Id)
            {
                return BadRequest();
            }
            if (invoice.InvoiceStatusId == 2)
            {
                throw new UnexpectedException(invoice, new Exception("Hóa  đơn này đã được thanh toán rồi."));
            }
            try
            {
                var dataUpdate = _invoice.Find(id);               
                dataUpdate.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                dataUpdate.IsDisplay = false;
                dataUpdate.NotePayment = invoice.NotePayment;
                dataUpdate.Total = invoice.Total;
                dataUpdate.InvoicePayment = invoice.InvoicePayment;
                dataUpdate.DiscountUnitId = invoice.DiscountUnitId;
                dataUpdate.DiscountUnitValue = invoice.DiscountUnitValue;
                //status 2 : cancel
                dataUpdate.InvoiceStatusId = 2;
                var t1 =   _invoice.EditAsync(dataUpdate);              
             
                dataUpdate = _invoice.LoadAllReference(dataUpdate);
                await t1;
                return CreatedAtAction("GetInvoice", new { id = invoice.Id }, dataUpdate);

            }
            catch (Exception e)
            {

                throw new UnexpectedException(invoice, e);
            }

        }
        private bool InvoiceExists(long id)
        {
            return _invoice.Any<Invoice>(e => e.Id == id);
        }
    }
}


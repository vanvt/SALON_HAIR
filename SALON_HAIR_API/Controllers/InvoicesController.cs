
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
using SALON_HAIR_API.Measure;

namespace SALON_HAIR_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class InvoicesController : CustomControllerBase
    {
        private readonly IInvoice _invoice;
        private readonly IPackage _package;
        private readonly IInvoiceDetail _invoiceDetail;
        private readonly IUser _user;
      
        public InvoicesController(IPackage package,IInvoiceDetail invoiceDetail,IInvoice invoice, IUser user)
        {
            _package = package;
            _invoiceDetail = invoiceDetail;
        
            _invoice = invoice;
            _user = user;
        }
        // GET: api/Invoices
        [HttpGet]
        public IActionResult GetInvoice(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = ""
            ,bool isDisplay=false,string date ="")
        {
            var data = _invoice.SearchAllFileds(keyword);
            data = GetByCurrentSalon(data);
            data = GetByCurrentSpaBranch(data);
            data = data.Where(e => e.Created.Value.Date == GetDateRangeQuery(date).Date);                     
            if (isDisplay)
            {
                data = data.Where(e => e.IsDisplay.Value);                        
            }
            var dataReturn = _invoice.LoadAllInclude(data,nameof(WarehouseTransaction));

            return OkList(dataReturn);
        }
        [HttpGet("by-customer/{customerId}")]
        public IActionResult GetInvoice(long customerId, int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _invoice.SearchAllFileds(keyword);
            data = GetByCurrentSalon(data);
            data = GetByCurrentSpaBranch(data);
            data = data.Where(e => e.CustomerId == customerId);
            var dataReturn = _invoice.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/Invoices/5
        [MeasureController("GetInvoice")]   
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoice([FromRoute] long id)
        {
            try
            {              
                var start = DateTime.Now;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var invoices =  _invoice.FindBy(e=>e.Id==id);
                invoices = _invoice.LoadAllCollecttion(invoices, nameof(InvoiceStaffArrangement),nameof(WarehouseTransaction));
                invoices = _invoice.LoadAllInclude(invoices);              
                var dataturn = await invoices.FirstOrDefaultAsync();           
                var end = DateTime.Now;
                if (dataturn == null)
                    return NotFound();
                return Ok(dataturn);
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
                //Invoice invoiceUpdate = _invoice.Find(invoice.Id);


                invoice.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                //invoiceUpdate.DiscountUnit = invoice.DiscountUnit;
                //invoiceUpdate.IsDisplay = invoice.IsDisplay;
                //invoiceUpdate.DiscountValue = invoice.DiscountValue;
                //invoiceUpdate.Total = invoice.DiscountUnit.Equals(DISCOUNTUNIT.MONEY) ? invoiceUpdate.TotalDetails - invoice.DiscountValue : invoiceUpdate.TotalDetails * (1 - invoice.DiscountValue / 100);

                await _invoice.EditAsync(invoice);
                invoice.Total = invoice.DiscountUnit.Equals(DISCOUNTUNIT.MONEY) ? invoice.TotalDetails - invoice.DiscountValue : invoice.TotalDetails * (1 - invoice.DiscountValue / 100);

                invoice = _invoice.LoadAllCollecttion(invoice) ;
                //var PackgeAvailables = GetPackgeAvailablesByCustomerId(invoice.CustomerId);
                return CreatedAtAction("GetInvoice", new { id = invoice.Id } , invoice);
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
                invoice.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                invoice.SalonId =  long.Parse( JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.SALONID)));
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
          
            try
            {
                var dataUpdate = _invoice.Find(id);
                if (PAYSTATUS.PAID.Equals(dataUpdate.PaymentStatus))
                {
                    throw new UnexpectedException(invoice, new Exception("Hóa  đơn này đã được thanh toán rồi."));
                }

                dataUpdate.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                dataUpdate.IsDisplay = false;
                dataUpdate.NotePayment = invoice.NotePayment;
                //dataUpdate.Total = invoice.Total;
                dataUpdate.Total = dataUpdate.DiscountUnit.Equals(DISCOUNTUNIT.MONEY) ? (dataUpdate.TotalDetails - (decimal)dataUpdate.DiscountValue) 
                    : (dataUpdate.TotalDetails - (dataUpdate.TotalDetails*(decimal)dataUpdate.DiscountValue/100));
                dataUpdate.InvoicePayment = invoice.InvoicePayment;               
                dataUpdate.PaymentStatus = PAYSTATUS.PAID;
                await _invoice.EditAsPayAsync(dataUpdate);              
             
                dataUpdate = _invoice.LoadAllReference(dataUpdate);
               
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
        private IQueryable<PackgeAvailable> GetPackgeAvailablesByCustomerId(long? customerId)
        {
            if (customerId == 0)
                return null;
            var listInvoiceDetail = _invoiceDetail.GetAll().Where(
                e => e.Invoice.CustomerId == customerId && 
                e.Invoice.PaymentStatus.Equals(PAYSTATUS.PAID) && 
                e.ObjectType.Equals("PACKAGE") 
                       
                );
            var listPackage = from a in listInvoiceDetail
                              join c in _package.GetAll() on a.ObjectId equals c.Id
                              group a by c into b
                              select new PackgeAvailable
                              {
                                  NumberOfPayed = b.Sum(e => e.Quantity),
                                  NumberOfUsed = b.Where(e => e.Status.Equals("PAYED")).Sum(e => e.Quantity),
                                  NumberRemaining = b.Sum(e => e.Quantity) - b.Where(e => e.Status.Equals("PAYED")).Sum(e => e.Quantity),
                                  Package = b.Key
                              };
            listPackage = listPackage.Where(e => e.NumberRemaining > 0);
            return listPackage;
        }
        private IQueryable<Invoice> GetByCurrentSpaBranch(IQueryable<Invoice> data)
        {
            var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

            if (currentSalonBranch != default || currentSalonBranch != 0)
            {
                data = data.Where(e => e.SalonBranchId == currentSalonBranch);
            }
            return data;
        }
        private IQueryable<Invoice> GetByCurrentSalon(IQueryable<Invoice> data)
        {
            data = data.Where(e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals("salonId")));
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



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
using System.Collections.Generic;

namespace SALON_HAIR_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : CustomControllerBase
    {
        private readonly ICustomer _customer;
              private readonly IPackage _package;
        private readonly IUser _user;
        private readonly IInvoice _invoice;
        private readonly IInvoiceDetail _invoiceDetail;
        public CustomersController(IPackage package, IInvoiceDetail invoiceDetail,IInvoice invoice,ICustomer customer, IUser user)
        {
            _package = package;
            _invoiceDetail = invoiceDetail;
            _invoice = invoice;
            _customer = customer;
            _user = user;
        }

        // GET: api/Customers
        [HttpGet]
        public IActionResult GetCustomer(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _customer.SearchAllFileds(keyword).Where
                (e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals("salonId"))); ;
            var dataReturn =   _customer.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var customer = await _customer.FindAsync(id);

                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] long id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != customer.Id)
            {
                return BadRequest();
            }
            try
            {
                customer.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                await _customer.EditAsync(customer);
                return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

                  throw new UnexpectedException(customer,e);
            }
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                customer.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                await _customer.AddAsync(customer);
                return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(customer,e);
            }
          
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var customer = await _customer.FindAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }

                await _customer.DeleteAsync(customer);

                return Ok(customer);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }
        //[HttpGet("get-package-available/{id}")]
        //public IActionResult GetPackgeAvailablesByCustomerId(long? id)
        //{

        //    if (id == 0)
        //        return OkList(new List<PackgeAvailable>().AsQueryable());
        //    var listInvoiceDetail = _invoiceDetail.GetAll().Where(
        //        e => e.Invoice.CustomerId == id &&
        //        e.Invoice.PaymentStatus.Equals(PAYSTATUS.PAID) &&
        //        e.ObjectType.Equals("PACKAGE")
        //        );
        //    var listPackage = from a in listInvoiceDetail
        //                      join c in _package.GetAll() on a.ObjectId equals c.Id
        //                      group a by c into b
        //                      select new PackgeAvailable
        //                      {
        //                          NumberOfPayed = b.Where(e => !e.IsPaid.Value).Sum(e => e.Quantity),
        //                          NumberOfUsed = b.Where(e => e.IsPaid.Value).Sum(e => e.Quantity),
        //                          NumberRemaining = b.Where(e => !e.IsPaid.Value).Sum(e => e.Quantity) - b.Where(e => e.IsPaid.Value).Sum(e => e.Quantity),
        //                          Package = b.Key
        //                      };
        //    listPackage = listPackage.Where(e => e.NumberRemaining > 0);
        //    return OkList(listPackage);
        //}

        private bool CustomerExists(long id)
        {
            return _customer.Any<Customer>(e => e.Id == id);
        }
        private IQueryable<Customer> GetByCurrentSpaBranch(IQueryable<Customer> data)
        {
            var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

            if (currentSalonBranch != default || currentSalonBranch != 0)
            {
                data = data.Where(e => e.SalonBranchId == currentSalonBranch);
            }
            return data;
        }
        private IQueryable<Customer> GetByCurrentSalon(IQueryable<Customer> data)
        {
            var salonId = JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID));
            data = data.Where(e => e.SalonId == salonId);
            return data;
        }
    }
}


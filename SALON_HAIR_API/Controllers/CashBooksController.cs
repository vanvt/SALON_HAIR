
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
    public class CashBooksController : CustomControllerBase
    {
        private readonly ICashBook _cashBook;
        private readonly IUser _user;

        public CashBooksController(ICashBook cashBook, IUser user)
        {
            _cashBook = cashBook;
            _user = user;
        }

        // GET: api/CashBooks
        [HttpGet]
        public IActionResult GetCashBook(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "", string date = "")
        {
            var data = _cashBook.SearchAllFileds(keyword);
            data = GetByCurrentSpaBranch(data);
            data = GetByCurrentSalon(data);
            data = data.Where(e => e.Created.Value.Date == GetDateRangeQuery(date).Date);
            var dataReturn =   _cashBook.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/CashBooks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCashBook([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var cashBook = await _cashBook.FindAsync(id);

                if (cashBook == null)
                {
                    return NotFound();
                }
                return Ok(cashBook);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/CashBooks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCashBook([FromRoute] long id, [FromBody] CashBook cashBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != cashBook.Id)
            {
                return BadRequest();
            }
            try
            {
                cashBook.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _cashBook.EditAsync(cashBook);
                return CreatedAtAction("GetCashBook", new { id = cashBook.Id }, cashBook);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!CashBookExists(id))
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

                  throw new UnexpectedException(cashBook,e);
            }
        }

        // POST: api/CashBooks
        [HttpPost]
        public async Task<IActionResult> PostCashBook([FromBody] CashBook cashBook)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                cashBook.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _cashBook.AddAsync(cashBook);
                return CreatedAtAction("GetCashBook", new { id = cashBook.Id }, cashBook);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(cashBook,e);
            }
          
        }

        // DELETE: api/CashBooks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCashBook([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var cashBook = await _cashBook.FindAsync(id);
                if (cashBook == null)
                {
                    return NotFound();
                }

                await _cashBook.DeleteAsync(cashBook);

                return Ok(cashBook);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool CashBookExists(long id)
        {
            return _cashBook.Any<CashBook>(e => e.Id == id);
        }

        private IQueryable<CashBook> GetByCurrentSpaBranch(IQueryable<CashBook> data)
        {
            var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

            if (currentSalonBranch != default || currentSalonBranch != 0)
            {
                data = data.Where(e => e.SalonBranchId == currentSalonBranch);
            }
            return data;
        }
        private IQueryable<CashBook> GetByCurrentSalon(IQueryable<CashBook> data)
        {
            data = data.Where(e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID)));
            return data;
        }
        private Tuple<DateTime, DateTime> GetDateRangeQuery(string start, string end)
        {
            start += "";
            end += "";
            var st = DateTime.Now;
            var en = DateTime.Now.AddDays(30.0);
            if (!string.IsNullOrEmpty(start))
            {

                st = DateTime.Parse(start);
            }
            if (!string.IsNullOrEmpty(end))
            {
                en = DateTime.Parse(end);
            }
            return Tuple.Create(st, en);
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


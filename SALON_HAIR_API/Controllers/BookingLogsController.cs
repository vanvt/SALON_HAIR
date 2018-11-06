
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using ULTIL_HELPER;
using Microsoft.AspNetCore.Authorization;
namespace SALON_HAIR_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BookingLogsController : CustomControllerBase
    {
        private readonly IBookingLog _bookingLog;
        private readonly IUser _user;

        public BookingLogsController(IBookingLog bookingLog, IUser user)
        {
            _bookingLog = bookingLog;
            _user = user;
        }

        // GET: api/BookingLogs
        [HttpGet]
        public IActionResult GetBookingLog(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "",long bookingId = 0)
        {
            if (bookingId != 0)
            {
                return OkList(_bookingLog.Paging(_bookingLog.SearchAllFileds(keyword).Where(e=>e.BookingId==bookingId), page, rowPerPage).Include(e => e.Status));
            }
            return OkList(_bookingLog.Paging( _bookingLog.SearchAllFileds(keyword),page,rowPerPage).Include(e=>e.Status));
        }     
    }
}


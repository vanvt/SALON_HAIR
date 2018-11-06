﻿
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using ULTIL_HELPER;
using Microsoft.AspNetCore.Authorization;
using SALON_HAIR_CORE.Service;
using SALON_HAIR_API.Exceptions;

namespace SALON_HAIR_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class StaffsController : CustomControllerBase
    {
        private readonly IPhoto _photo;
        private readonly IStaff _staff;
        private readonly IUser _user;
        private readonly IStaffTitle _staffTitle;
        private readonly IStaffService _staffService;
        private readonly IStaffCommissionGroup _commissionGroup;
        public StaffsController(IPhoto photo, IStaffCommissionGroup commissionGroup,IStaff staff, IStaffTitle staffTitle,IUser user, IStaffService staffService)
        {
            _photo = photo;
            _commissionGroup = commissionGroup;
            _staffTitle = staffTitle;
               _staffService = staffService;
            _staff = staff;
            _user = user;
        }

        // GET: api/Staffs
        [HttpGet]
        public IActionResult GetStaff(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            return OkList(_staff.Paging(_staff.SearchAllFileds(keyword), page, rowPerPage)
                .Include(e=>e.StaffTitle)
                .Include(e=>e.StaffCommisionGroup)
                .Include(e => e.Photo)
                .Include(e=>e.StaffService).ThenInclude(e=>e.Service)              
                );
        }
        // GET: api/Staffs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaff([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var staff = await _staff.FindAsync(id);

                if (staff == null)
                {
                    return NotFound();
                }
                return Ok(staff);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        // PUT: api/Staffs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaff([FromRoute] long id, [FromBody] Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != staff.Id)
            {
                return BadRequest();
            }
            try
            {
                 if (staff.StaffService.Select(e => e.ServiceId).Count() != staff.StaffService.Select(e => e.ServiceId).Distinct().Count())
                {
                    throw new BadRequestException("Không thể tạo nhân có hai sản dịch vụ nhau được babe");
                }
                staff.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));

                await _staff.EditMany2ManyAsync(staff);
                staff.StaffService = _staffService.FindBy(e => e.StaffId == staff.Id).Include(e => e.Service).ToList();
                return CreatedAtAction("GetStaff", new { id = staff.Id }, staff);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
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

                throw;
            }
        }

        // POST: api/Staffs
        [HttpPost]
        public async Task<IActionResult> PostStaff([FromBody] Staff staff)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (staff.StaffService.Select(e => e.ServiceId).Count() != staff.StaffService.Select(e => e.ServiceId).Distinct().Count())
                {
                    throw new BadRequestException("Không thể tạo nhân có hai sản dịch vụ nhau được babe");
                }
                staff.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                await _staff.AddMany2ManyAsync(staff);
                var title = await _staffTitle.FindAsync( staff.StaffTitleId.Value);
                staff.StaffTitle = title;
                var groupCommision = await  _commissionGroup.FindAsync(staff.StaffCommisionGroupId);
                staff.StaffCommisionGroup = groupCommision;
                staff.StaffService = _staffService.FindBy(e => e.StaffId == staff.Id).Include(e => e.Service).ToList();
                if(staff.PhotoId!= default)
                {
                    staff.Photo =await _photo.FindAsync(staff.PhotoId);
                }
            
                //_staff.Entry()
                return CreatedAtAction("GetStaff", new { id = staff.Id }, staff);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        // DELETE: api/Staffs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var staff = await _staff.FindAsync(id);
                if (staff == null)
                {
                    return NotFound();
                }

                await _staff.DeleteAsync(staff);

                return Ok(staff);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        private bool StaffExists(long id)
        {
            return _staff.Any<Staff>(e => e.Id == id);
        }
        [HttpPut("change-status/{id}")]
        public async Task<IActionResult> ChangeStatusAsync([FromBody] Staff staff, [FromRoute] long id)
        {         
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var oldStaff = await _staff.FindAsync(id);

                if (staff == null)
                {
                    return NotFound();
                }
                oldStaff.Status = staff.Status.ToUpper();
                _staff.LoadAllReference(oldStaff);
                return Ok(oldStaff);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGencodeBackend
{
    public static class ControllerUpdatedTemplate
    {
        public static string tempalte = @"
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
    [Route(""[controller]"")]
    [ApiController]
    [Authorize]
    public class {ClassName}sController : CustomControllerBase
    {
        private readonly I{ClassName} _{InstanceName};
        private readonly IUser _user;

        public {ClassName}sController(I{ClassName} {InstanceName}, IUser user)
        {
            _{InstanceName} = {InstanceName};
            _user = user;
        }

        // GET: api/{ClassName}s
        [HttpGet]
        public IActionResult Get{ClassName}(int page = 1, int rowPerPage = 50, string keyword = """", string orderBy = """", string orderType = """")
        {
            return OkList(_{InstanceName}.Paging( _{InstanceName}.SearchAllFileds(keyword),page,rowPerPage).Include(e=>e.Status));
        }
        // GET: api/{ClassName}s/5
        [HttpGet(""{id}"")]
        public async Task<IActionResult> Get{ClassName}([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var {InstanceName} = await _{InstanceName}.FindAsync(id);

                if ({InstanceName} == null)
                {
                    return NotFound();
                }
                return Ok({InstanceName});
            }
            catch (Exception e)
            {

                throw;
            }
        }

        // PUT: api/{ClassName}s/5
        [HttpPut(""{id}"")]
        public async Task<IActionResult> Put{ClassName}([FromRoute] long id, [FromBody] {ClassName} {InstanceName})
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != {InstanceName}.Id)
            {
                return BadRequest();
            }
            try
            {
                {InstanceName}.UpdatedBy = _user.FindBy(e => e.Id == JwtHelper.GetIdFromToken(User.Claims)).FirstOrDefault().Email;
                await _{InstanceName}.EditAsync({InstanceName});
                return CreatedAtAction(""Get{ClassName}"", new { id = {InstanceName}.Id }, {InstanceName});
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!{ClassName}Exists(id))
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

        // POST: api/{ClassName}s
        [HttpPost]
        public async Task<IActionResult> Post{ClassName}([FromBody] {ClassName} {InstanceName})
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                {InstanceName}.CreatedBy = _user.FindBy(e => e.Id == JwtHelper.GetIdFromToken(User.Claims)).FirstOrDefault().Email;
                await _{InstanceName}.AddAsync({InstanceName});
                return CreatedAtAction(""Get{ClassName}"", new { id = {InstanceName}.Id }, {InstanceName});
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        // DELETE: api/{ClassName}s/5
        [HttpDelete(""{id}"")]
        public async Task<IActionResult> Delete{ClassName}([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var {InstanceName} = await _{InstanceName}.FindAsync(id);
                if ({InstanceName} == null)
                {
                    return NotFound();
                }

                await _{InstanceName}.DeleteAsync({InstanceName});

                return Ok({InstanceName});
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        private bool {ClassName}Exists(long id)
        {
            return _{InstanceName}.Any<{ClassName}>(e => e.Id == id);
        }
    }
}

"
            ;
    }
}

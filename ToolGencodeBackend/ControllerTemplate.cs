using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGencodeBackend
{
    public static class ControllerTemplate
    {
        public static string tempalte = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ADMIN_EASYSAP_ENTITY.Entities;
using ADMIN_EASYSPA_CORE.Interface;
using Microsoft.AspNetCore.Authorization;
namespace ADMIN_EASYSPA.Controllers
{
    [Route(""[controller]"")]
    [ApiController]
    [Authorize]
    public class {ClassName}sController : CustomControllerBase
    {
        private readonly I{ClassName} _{InstanceName};
        public {ClassName}sController(I{ClassName} {InstanceName})
        {
            _{InstanceName} = {InstanceName};
        }

        // GET: api/{ClassName}s
        [HttpGet]
        public IActionResult Get{ClassName}(int page = 1, int rowPerPage = 50, string keyword = """")
        {
            return OkList(_{InstanceName}.Paging( _{InstanceName}.SearchAllFileds(keyword),page,rowPerPage));
        }
        // GET: api/{ClassName}s/5
        [HttpGet(""{id}"")]
        public async Task<IActionResult> Get{ClassName}([FromRoute] long id)
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
        }

        // POST: api/{ClassName}s
        [HttpPost]
        public async Task<IActionResult> Post{ClassName}([FromBody] {ClassName} {InstanceName})
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _{InstanceName}.AddAsync({InstanceName});
            return CreatedAtAction(""Get{ClassName}"", new { id = {InstanceName}.Id }, {InstanceName});
        }

        // DELETE: api/{ClassName}s/5
        [HttpDelete(""{id}"")]
        public async Task<IActionResult> Delete{ClassName}([FromRoute] long id)
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

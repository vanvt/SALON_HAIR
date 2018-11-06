using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGencodeBackend
{
    public static class ServiceTemplate
    {
        public static string tempalteService = @"
using ADMIN_EASYSAP_ENTITY.Entities;
using ADMIN_EASYSPA_CORE.Interface;
using ADMIN_EASYSPA_CORE.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADMIN_EASYSPA_CORE.Service
{
    public class {ClassName}Service: GenericRepository<{ClassName}> ,I{ClassName}
    {
        private easyspaContext _easyspaContext;
        public {ClassName}Service(easyspaContext easyspaContext) : base(easyspaContext)
        {
            _easyspaContext = easyspaContext;
        }        
    }
}
";
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGencodeBackend
{
    public class ServiceTemplateUpdate
    {
        public static string tmp = @"

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class {ClassName}Service: GenericRepository<{ClassName}> ,I{ClassName}
    {
        private salon_hairContext _salon_hairContext;
        public {ClassName}Service(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit({ClassName} {InstanceName})
        {
            {InstanceName}.Updated = DateTime.Now;

            base.Edit({InstanceName});
        }
        public async new Task<int> EditAsync({ClassName} {InstanceName})
        {            
            {InstanceName}.Updated = DateTime.Now;         
            return await base.EditAsync({InstanceName});
        }
        public new async Task<int> AddAsync({ClassName} {InstanceName})
        {
            {InstanceName}.Created = DateTime.Now;
            return await base.AddAsync({InstanceName});
        }
        public new void Add({ClassName} {InstanceName})
        {
            {InstanceName}.Created = DateTime.Now;
            base.Add({InstanceName});
        }
        public new void Delete({ClassName} {InstanceName})
        {
            {InstanceName}.Status = ""DELETED"";
            base.Edit({InstanceName});
        }
        public new async Task<int> DeleteAsync({ClassName} {InstanceName})
        {
            {InstanceName}.Status = ""DELETED"";
            return await base.EditAsync({InstanceName});
        }
    }
}
    ";
    }
}

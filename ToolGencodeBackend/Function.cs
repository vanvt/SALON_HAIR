using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGencodeBackend
{
    public class Function
    {
        static void CreateForm(IEntityType entityType)
        {
            string nameSpace = "ADMIN_EASYSAP_ENTITY.Entities.";
            string nameEnity = entityType.Name.Remove(0, nameSpace.Length);
            string intanceName = Char.ToLowerInvariant(nameEnity[0]) + nameEnity.Substring(1);

            string element = @"/>            
                   <input-group
                  :label=""'{Property} {InstanceName} (*)'""
                  v - model = ""{InstanceName}.{{Property}}""
                  :required = ""true""
                  /> ";
       
            foreach (var item in entityType.GetProperties())
            {
                
            }
        }
    }
}

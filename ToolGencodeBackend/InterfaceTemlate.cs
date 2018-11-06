using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGencodeBackend
{
    public static class InterfaceTemlate
    {
        public static string tempalteInterface = @"
using SALON_HAIR_CORE.Repository;
using SALON_HAIR_ENTITY.Entities;

namespace SALON_HAIR_CORE.Interface
{
    public interface I{ClassName}: IGenericRepository<{ClassName}>
    {

    }
}

";
    }
}

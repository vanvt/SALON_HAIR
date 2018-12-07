

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class SettingAdvanceService: GenericRepository<SettingAdvance> ,ISettingAdvance
    {
        private salon_hairContext _salon_hairContext;
        public SettingAdvanceService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
       
    }
}
    
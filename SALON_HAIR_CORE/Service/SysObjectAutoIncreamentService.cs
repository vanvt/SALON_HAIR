

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace SALON_HAIR_CORE.Service
{
    public class SysObjectAutoIncreamentService: GenericRepository<SysObjectAutoIncreament> ,ISysObjectAutoIncreament
    {
        private salon_hairContext _salon_hairContext;
        public SysObjectAutoIncreamentService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }
        public async Task<SysObjectAutoIncreament> GetCodeByObjectAsync(string objectName, long salonId)
        {
            var indexObject = _salon_hairContext.SysObjectAutoIncreament.Where(e => e.SpaId == salonId && e.ObjectName.Equals(objectName)).FirstOrDefault();

            if (indexObject == null)
            {
                indexObject = new SysObjectAutoIncreament
                {
                    SpaId = salonId,
                    ObjectIndex = 1,
                    ObjectName = objectName
                };
                await _salon_hairContext.SysObjectAutoIncreament.AddAsync(
                 indexObject
                    );
            }
            else
            {
                indexObject.ObjectIndex++;
                _salon_hairContext.SysObjectAutoIncreament.Update(indexObject);
            }
            await _salon_hairContext.SaveChangesAsync();
            return indexObject;
        }
        public  SysObjectAutoIncreament GetCodeByObjectAsyncWithoutSave(salon_hairContext salon_hairContext, string objectName, long salonId)
        {
            var indexObject = salon_hairContext.SysObjectAutoIncreament.Where(e => e.SpaId == salonId && e.ObjectName.Equals(objectName)).FirstOrDefault();

            if (indexObject == null)
            {
                indexObject = new SysObjectAutoIncreament
                {
                    SpaId = salonId,
                    ObjectIndex = 1,
                    ObjectName = objectName
                };               
            }
            else
            {
                indexObject.ObjectIndex++;
               
            }           
            return indexObject;
        }
        public async Task<SysObjectAutoIncreament> GetCodeByObjectAsyncWithoutSave(salon_hairContext salon_hairContext, string objectName, long salonId,long jumdIndex)
        {
            var indexObject = salon_hairContext.SysObjectAutoIncreament.Where(e => e.SpaId == salonId && e.ObjectName.Equals(objectName)).FirstOrDefault();

            if (indexObject == null)
            {
                indexObject = new SysObjectAutoIncreament
                {
                    SpaId = salonId,
                    ObjectIndex = jumdIndex,
                    ObjectName = objectName
                };
                await salon_hairContext.SysObjectAutoIncreament.AddAsync(
                 indexObject
                    );
            }
            else
            {
                indexObject.ObjectIndex = indexObject.ObjectIndex + jumdIndex;
                salon_hairContext.SysObjectAutoIncreament.Update(indexObject);
            }
            return indexObject;
        }
        public async Task<SysObjectAutoIncreament> CreateOrUpdateAsync(salon_hairContext salon_hairContext, SysObjectAutoIncreament indexObject)
        {
          
            if (salon_hairContext.SysObjectAutoIncreament.Local.Any(e=>e == indexObject))
            {
                salon_hairContext.SysObjectAutoIncreament.Update(indexObject);
            }
            else
            {
                await salon_hairContext.SysObjectAutoIncreament.AddAsync(indexObject);
            }
            return indexObject;
        }

    }
}
    
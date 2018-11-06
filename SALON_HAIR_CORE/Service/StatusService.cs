

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class StatusService: GenericRepository<Status> ,IStatus
    {
        private salon_hairContext _salon_hairContext;
        public StatusService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(Status status)
        {
            status.Updated = DateTime.Now;

            base.Edit(status);
        }
        public async new Task<int> EditAsync(Status status)
        {            
            status.Updated = DateTime.Now;         
            return await base.EditAsync(status);
        }
        public new async Task<int> AddAsync(Status status)
        {
            status.Created = DateTime.Now;
            return await base.AddAsync(status);
        }
        public new void Add(Status status)
        {
            status.Created = DateTime.Now;
            base.Add(status);
        }
       
    }
}
    
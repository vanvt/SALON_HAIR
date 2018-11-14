
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_ENTITY.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALON_HAIR_CORE.Service
{
    public class GenericService : IGeneric
    {
        private salon_hairContext _salon_hairContext;
        public GenericService(salon_hairContext salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }
        public object LoadAll(object data)
        {

            var refs = _salon_hairContext.Entry(data).References.Select(e => e.Metadata.Name).Where(e => !GlobalReferenceCustom.ListReference.Contains(e));
            refs.ToList().ForEach(e => {
                _salon_hairContext.Entry(data).Reference(e).Load();
            });
            return data;
        }
    }
}

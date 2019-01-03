

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ULTIL_HELPER;

namespace SALON_HAIR_CORE.Service
{
    public class SalonService: GenericRepository<Salon> ,ISalon
    {
        private salon_hairContext _salon_hairContext;
        private readonly ISecurityHelper _securityHelper;
        public SalonService(salon_hairContext salon_hairContext, ISecurityHelper SecurityHelper) : base(salon_hairContext)
        {
            _securityHelper = SecurityHelper;
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(Salon salon)
        {
            salon.Updated = DateTime.Now;

            base.Edit(salon);
        }
        public async new Task<int> EditAsync(Salon salon)
        {            
            salon.Updated = DateTime.Now;         
            return await base.EditAsync(salon);
        }
        public new async Task<int> AddAsync(Salon salon)
        {
            salon.Created = DateTime.Now;
            salon = InitSetting(salon);
            return await base.AddAsync(salon);
        }
        public new void Add(Salon salon)
        {
            salon.Created = DateTime.Now;
            salon = InitSetting(salon);
            base.Add(salon);
        }
        public new void Delete(Salon salon)
        {
            salon.Status = "DELETED";
            base.Edit(salon);
        }
        public new async Task<int> DeleteAsync(Salon salon)
        {
            salon.Status = "DELETED";
            return await base.EditAsync(salon);
        }

        private Salon InitSetting(Salon salon)
        {
           var listSettingAdvance =  _salon_hairContext.SettingAdvance
                .Where(e => e.SalonId == 1)
                .Select(e=>new SettingAdvance {Enum = e.Enum,Group =e.Group,Key =e.Key,Value = e.Value});
            salon.SettingAdvance = listSettingAdvance.ToList();
            return salon;
        }
        private Salon InitStaffGroup(Salon salon)
        {
            var staffGroups = _salon_hairContext.StaffGroup
                 .Where(e => e.SalonId == 1)
                 .Select(e => new StaffGroup {
                     Name = e.Name,
                     Code = e.Code,
                     Created = DateTime.Now,                     
                     CreatedBy = salon.Email
                 });
            salon.StaffGroup = staffGroups.ToList();
            return salon;
        }
        private Salon InitTransactionCategory(Salon salon)
        {
            var staffGroups = _salon_hairContext.CashBookTransactionCategory
                 .Where(e => e.SalonId == 1)
                 .Select(e => new CashBookTransactionCategory
                 {
                     Type = e.Type,
                     Name = e.Name,
                     Code = e.Code,
                     Created = DateTime.Now,
                     CreatedBy = salon.Email
                 });
            salon.CashBookTransactionCategory = staffGroups.ToList();
            return salon;
        }
        private Salon InitPaymentMethod(Salon salon)
        {
            var staffGroups = _salon_hairContext.PaymentMethod
                 .Where(e => e.SalonId == 1)
                 .Select(e => new PaymentMethod
                 {
                     Created = DateTime.Now,
                     CreatedBy = salon.Email,
                     Code = e.Code
                 });
            salon.PaymentMethod = staffGroups.ToList();
            return salon;
        }
        private List<Authority> InitListAuthority(Salon salon)
        {
          var list =    new List<Authority> {
                new Authority {Name = "Thu Ngân" ,Salon =salon},
                new Authority {Name = "Quản Lý" ,Salon =salon},
                new Authority {Name = "Admin" ,Salon =salon},
            };        
            return list;
        }      
        private Salon InitBranch(Salon salon)
        {
           
            var salonBranch = 
                new SalonBranch{
                Email = salon.Email,
                Address = salon.Address,
                Name = salon.Name,
                Created = DateTime.Now,
                CreatedBy = salon.Email
                }
            ;
            salon.SalonBranch = new List<SalonBranch> { salonBranch };
            return salon;
        }
        private User InitUser(Salon salon)
        {
             salon = InitBranch(salon);
            var user =
                new User
                {
                    Email = salon.Email,
                    Name = salon.Name,
                    CreatedBy = salon.Email,
                    CreatedDate = DateTime.Now,
                    UserAuthority = InitListAuthority(salon)
                    .Select(e => new UserAuthority
                    {
                        Created = DateTime.Now,
                        CreatedBy = salon.Email,
                        Authority = e,

                    }).ToList(),
                    SalonBranchCurrent = salon.SalonBranch.FirstOrDefault(),
                    PasswordHash = _securityHelper.BCryptPasswordEncoder(SYSTEMDEFAULT.PASSWORD),
                    UserSalonBranch = new List<UserSalonBranch>
                    {
                        new UserSalonBranch
                        {
                            SpaBranch = salon.SalonBranch.FirstOrDefault(),
                            Created = DateTime.Now,
                            CreatedBy = salon.Email,                            
                        }
                    },                       
                }
            ;           
            return user;
        }
        public async Task AddAsRegisterAsync(Salon salon)
        {            
            salon = InitSetting(salon);
            salon = InitStaffGroup(salon);
            salon = InitTransactionCategory(salon);
            salon = InitPaymentMethod(salon);
            salon.User = new List<User> { InitUser(salon) };
            salon.Created = DateTime.Now;
            salon.CreatedBy = salon.Email;
             _salon_hairContext.Salon.Add(salon);
            await _salon_hairContext.SaveChangesAsync();
        }
    }
}
    
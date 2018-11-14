using Microsoft.EntityFrameworkCore;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using SALON_HAIR_ENTITY.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace XUnitTestProject1.Service
{
    public class StaffRepository
    {
        [Fact]
        public void GetAll()
        {
            var _staff = GetInMemoryPersonRepository();
            Staff staff = new Staff()
            {
                CreatedBy = "vanvt",
                Name = "vanvt",
                Photo = new Photo
                {
                    Name = "vanvt"
                }
            };
            _staff.Add(staff);

            var data = _staff.SearchAllFileds("vanvt");
            var dataReturn = _staff.LoadAllInclude(data);
            Assert.Equal(1, _staff.GetAll().Count());
            Assert.Equal("vanvt", data.Include(e=>e.Photo.Name).FirstOrDefault().Photo.Name);
        }
        [Fact]
        public void Add()
        {
            var _staff = GetInMemoryPersonRepository();
            Staff staff = new Staff() {
                CreatedBy = "vanvt",
                Name = "vanvt"
            };
            _staff.Add(staff);
            Assert.Equal(1, _staff.GetAll().Count());

        }
        private GenericRepository<Staff> GetInMemoryPersonRepository()
        {
            DbContextOptions<salon_hairContext> options;
            var builder = new DbContextOptionsBuilder<salon_hairContext>();
            builder.UseInMemoryDatabase();
            options = builder.Options;
            salon_hairContext salon_hairContext = new salon_hairContext(options);
            salon_hairContext.Database.EnsureDeleted();
            salon_hairContext.Database.EnsureCreated();
            return new GenericRepository<Staff>(salon_hairContext);
        }
    }
   
}

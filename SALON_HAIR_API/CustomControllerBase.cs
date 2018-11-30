using Microsoft.AspNetCore.Mvc;
using SALON_HAIR_API.ViewModels;
using SALON_HAIR_CORE.Interface;
using System;
using System.Linq;
using ULTIL_HELPER;

namespace SALON_HAIR_API
{
    public class CustomControllerBase : ControllerBase
    {   
        public override OkObjectResult Ok(object value)
        {
            int.TryParse(HttpContext.Request.Query["page"].ToString(), out int page);
            int.TryParse(HttpContext.Request.Query["rowPerPage"].ToString(), out int rowPerPage);
            string keyword = HttpContext.Request.Query["keyword"].ToString();
            if (rowPerPage == 0)
            {
                rowPerPage = 50;
            }
            var response = new BaseViewModel
            {
                ErrorCode = 200,
                ErrorDesc = "Thành công",
                Data = value,
                Meta = new MetaViewModel
                {
                    CurrentPage = page,
                    RowPerPage = rowPerPage,

                    Query = new
                    {
                        keyword
                    },
                }
            };
            if (value is BaseViewModel)
            {
                var rs = value as BaseViewModel;
                response.Data = rs.Data;
                response.Meta.TotalItem = rs.Meta.TotalItem;
                response.Meta.TotalPage = StringHelper.CountTotalPage(rs.Meta.TotalItem, rowPerPage);
            }
           

            return new OkObjectResult(response);
        }
        public OkObjectResult OkList<T>(IQueryable<T> rs)
        {
            int.TryParse(HttpContext.Request.Query["page"].ToString(), out int page);
            int.TryParse(HttpContext.Request.Query["rowPerPage"].ToString(), out int rowPerPage);
            page = page == 0 ? 1 : page;
            rowPerPage = rowPerPage == 0 ? 50 : rowPerPage;
            var data = rs.Skip((page - 1) * rowPerPage).Take(rowPerPage);
            Console.WriteLine("Count list object");
            Console.WriteLine("-----------------------------------------------------------------------------");
            var response = new BaseViewModel
            {
                Data = data,
                Meta = new MetaViewModel
                {                 
                    TotalItem = rs.Count()
                }
            };       
            Console.WriteLine("-----------------------------------------------------------------------------");
            return Ok(response);
        }

        public override CreatedAtActionResult CreatedAtAction(string actionName, object routeValues, object value)
        {
            var response = new BaseViewModel
            {
                ErrorCode = 1,
                ErrorDesc = "Thành công",
                Data = value
            };
            return this.CreatedAtAction(actionName, (string)null, routeValues, response);
        }


    }
}

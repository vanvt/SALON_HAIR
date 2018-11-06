using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SALON_HAIR_API.Extension;
using SALON_HAIR_ENTITY.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Extension ex = new Extension();
            ex.uploadToS3Async()
            Console.WriteLine("Hello World!");
           
        }
        //public static IQueryable<ProductCategory> SearchAllFileds(string keyword, string field, string type)
        //{
        //    //DbContextOptions<salon_hairContext> dbContextOptions = new DbContextOptions<salon_hairContext>();
           
        //    salon_hairContext _easyspaContext = new salon_hairContext();
        //    field = field.ToLower().Trim();
        //    type = type.Trim().ToLower();
        //    var entityType = _easyspaContext.Model.FindEntityType(typeof(ProductCategory));
          
        //    var fieldOrder = entityType.GetProperties().Where(e => e.Name.ToLower().Equals("updated")).FirstOrDefault();
        //    IQueryable<ProductCategory> rs;
        //    string tableName = entityType.Relational().TableName;
        //    string query = "";
        //    if (string.IsNullOrEmpty(keyword))
        //    {
        //        query = $" SELECT * FROM  {tableName}   ";
        //    }
        //    else
        //    {
        //        query = $" SELECT * FROM  {tableName} where  ";

        //        List<string> filter = new List<string>();

        //        var parameters = new List<MySqlParameter>();

        //        var querySearch =
        //            from a in entityType.GetProperties()
        //            where a.ClrType.FullName != typeof(System.Nullable<DateTime>).FullName
        //            && a.ClrType.FullName != typeof(DateTime).FullName
        //        //join b in _easyspaContext.SupperSetting on a.Relational().ColumnName equals b.FieldDb
        //        //        into c  from b in c.DefaultIfEmpty()
        //        //select new { filler  = $" {a.Relational().ColumnName} like '%{keyword}%' " ,searchable = b==null? true:b.Searchalbe }.
        //        //select new { filler = $" {a.Relational().ColumnName} like @keyword ", searchable = true };
        //        //(LOCATE(@__keyword_0, CONVERT(`e`.`created_date`, CHAR(127))) > 0) OR (@__keyword_0 = '')
        //        select new { filler = $"(LOCATE(@keyword, CONVERT({a.Relational().ColumnName}, CHAR(450))) > 0) ", searchable = true };
        //        querySearch = querySearch.Where(a => a.searchable == true);
        //        filter = querySearch.Select(e => e.filler).ToList();

        //        query += String.Join("or", filter);

        //        //rs = _easyspaContext.Set<ProductCategory>().FromSql(query, new MySqlParameter("@keyword", keyword));
        //    }
        //    if (fieldOrder != null)
        //    {
        //        query += $" order by {fieldOrder.Relational().ColumnName} {type} ";

        //    }
        //    if (string.IsNullOrEmpty(keyword))
        //    {
        //        rs = _easyspaContext.Set<ProductCategory>().FromSql(query);
        //    }
        //    else
        //    {
        //        rs = _easyspaContext.Set<ProductCategory>().FromSql(query, new MySqlParameter("@keyword", keyword));
        //    }
          


        //    return rs;
        //}
       
    }
}

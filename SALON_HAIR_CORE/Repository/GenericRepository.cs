using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MySql.Data.MySqlClient;
using SALON_HAIR_CORE.Utilities;
using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_ENTITY.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private salon_hairContext _easyspaContext;

        public GenericRepository(salon_hairContext easyspaContext)
        {
            _easyspaContext = easyspaContext;
        }

        public void Add(T entity)
        {
            try
            {

                _easyspaContext.Set<T>().Add(entity);

                _easyspaContext.SaveChanges();
                entity =  LoadAllReference(entity);
   
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<int> AddAsync(T entity)
        {
            try
            {

                _easyspaContext.Set<T>().Add(entity);
                entity = LoadAllReference(entity);
                return await _easyspaContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;
            }

        }

        public void Delete(T entity)
        {
            try
            {
                _easyspaContext.Set<T>().Remove(entity);
                _easyspaContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<int> DeleteAsync(T entity)
        {
            try
            {

                _easyspaContext.Set<T>().Remove(entity);
                return await _easyspaContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void Edit(T entity)
        {

            try
            {
                _easyspaContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _easyspaContext.SaveChanges();
            }
            catch (Exception ex)
            {


                throw;
            }
            //_easyspaContext.Update(entity);
            //_easyspaContext.Attach(entity);

        }

        public async Task<int> EditAsync(T entity)
        {
            try
            {
                _easyspaContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                entity = LoadAllReference(entity);
                return await _easyspaContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            //_easyspaContext.Attach(entity);

        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _easyspaContext.Set<T>().Where(predicate);
            return query;
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = _easyspaContext.Set<T>();
            return query;
        }
        public EntityEntry<T> Entry(T entity )
        {            
            return _easyspaContext.Entry(entity);
        }
        public void Save()
        {
            _easyspaContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _easyspaContext.SaveChangesAsync();
        }

        static bool CheckAllFields<TInput, TValue>(TInput input, TValue value, bool alsoCheckProperties)
        {
            Type t = typeof(TInput);
            /*
            foreach (FieldInfo info in t.GetFields())
            {
                string rs1 = info.GetValue(input).ToString();
                string rs2 = value.ToString();

                if (info.GetValue(input).Equals(value))
                {                  
                    return true;
                }
            }
            */
            if (alsoCheckProperties)
            {
                foreach (PropertyInfo info in t.GetProperties())
                {
                    if (info.GetValue(input) != null)
                    {
                        string rs1 = info.GetValue(input).ToString();
                        string rs2 = value.ToString();
                        if (rs1.Contains(rs2))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        /*
        public IQueryable<T> SearchAllFileds(string keyword)
        {
            var kq = from a in _easyspaContext.Set<T>() where CheckAllFields(a, keyword, true) select a;
            return kq;
        }
        */
        public IQueryable<T> Paging(IQueryable<T> query, int currentPage, int rowPerPage)
        {
            //query = query.Include("Status");
            return query.Skip((currentPage - 1) * rowPerPage).Take(rowPerPage);
        }
        public async Task<T> FindAsync(params object[] keyValues)
        {
            var  entity = await _easyspaContext.Set<T>().FindAsync(keyValues);
            //entity = LoadAllReference(entity);
            return entity;
        }
        public bool Any<Tsoure>(Expression<Func<T, bool>> predicate)
        {
            return _easyspaContext.Set<T>().Any(predicate);
        }
        /*
        public IQueryable<T> SearchAllFileds(string keyword)
        {

            if (string.IsNullOrEmpty(keyword))
            {
                return _easyspaContext.Set<T>();
            }

            Type t = typeof(T);
            var entityType = _easyspaContext.Model.FindEntityType(t);
            string tableName = entityType.Relational().TableName;

            string query = $" SELECT * FROM  {tableName} where  ";
            List<string> filter = new List<string>();
            int index = 0;
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            var parameters = new List<SqlParameter>();

            foreach (var props in entityType.GetProperties())
            {
                var paramField = $"@p{ ++index}";
                string field = props.Relational().ColumnName;
                //keyValuePairs.Add(paramField, field);
                parameters.Add(new SqlParameter(paramField, field));
                filter.Add($" {paramField} like '%@keyword%' ");
            }
            //var parameters = new List<SqlParameter>();
            
            foreach (var item in keyValuePairs)
            {
                parameters.Add(new SqlParameter(item.Key, item.Value));
            }
            
           
            query += String.Join("or",filter);
            var test = query;
            RawSqlString rawSqlString = new RawSqlString(query);
            IQueryable<T> rs = _easyspaContext.Set<T>().FromSql(rawSqlString, parameters.ToArray());
            
            //var sults = _easyspaContext.Set<T>().FromSql(query);
            //var kq = CustomSearchAllField(source, keyword);
            //var kq = from a in _easyspaContext.Set<T>() where CheckAllFields(a, keyword, true) select a;
            return rs;
        }
    */
        public IQueryable<T> SearchAllFileds(string keyword)
        {
            return SearchAllFileds(keyword, string.Empty, string.Empty);
            //if (string.IsNullOrEmpty(keyword))
            //{
            //    return _easyspaContext.Set<T>();
            //}
            //var entityType = _easyspaContext.Model.FindEntityType(typeof(T));
            //string tableName = entityType.Relational().TableName;
            //string query = $" SELECT * FROM  {tableName} where  ";
            //List<string> filter = new List<string>();

            //var parameters = new List<MySqlParameter>();

            //var querySearch =
            //    from a in entityType.GetProperties()
            //    where a.ClrType.FullName != typeof(System.Nullable<DateTime>).FullName
            //    && a.ClrType.FullName != typeof(DateTime).FullName
            //    select new { filler = $"(LOCATE(@keyword, CONVERT({a.Relational().ColumnName}, CHAR(450))) > 0) ", searchable = true };
            //querySearch = querySearch.Where(a => a.searchable == true);
            //filter = querySearch.Select(e => e.filler).ToList();

            //query += String.Join("or", filter);

            //IQueryable<T> rs = _easyspaContext.Set<T>().FromSql(query, new MySqlParameter("@keyword", keyword));

            //return rs;
        }
        public IQueryable<T> SearchAllFileds(string keyword, string field, string type)
        {
            IQueryable<T> rs;
            if(string.IsNullOrEmpty(keyword) && string.IsNullOrEmpty(field) && string.IsNullOrEmpty(type))
            {
                return _easyspaContext.Set<T>();
            }
            string query = GennerateSQL(typeof(T), keyword, field, type);

            if (string.IsNullOrEmpty(keyword))
            {
                rs = _easyspaContext.Set<T>().FromSql(query);
            }
            else
            {
                rs = _easyspaContext.Set<T>().FromSql(query, new MySqlParameter("@keyword", keyword));
            }
            return rs;
        }
        public string GennerateSQL(Type type, string keyword, string field, string typeOrder)
        {
            string sqlRaw = "";
            field = field.ToLower().Trim();
            typeOrder = typeOrder.Trim().ToLower();
            var entityType = _easyspaContext.Model.FindEntityType(type);
       
            var fieldOrder = entityType.GetProperties().Where(e => e.Name.ToLower().Equals(field)).FirstOrDefault();
            string tableName = entityType.Relational().TableName;
            if (string.IsNullOrEmpty(keyword))
            {
                sqlRaw += $" SELECT * FROM  {tableName}        ";
            }
            else
            {
                sqlRaw += $" SELECT * FROM  {tableName} where  ";
                List<string> filter = new List<string>();
                var parameters = new List<MySqlParameter>();
                var querySearch =
                    from a in entityType.GetProperties()
                    where a.ClrType.FullName != typeof(System.Nullable<DateTime>).FullName
                    && a.ClrType.FullName != typeof(DateTime).FullName
                    select new { filler = $"(LOCATE(@keyword, CONVERT({a.Relational().ColumnName}, CHAR(450))) > 0) ", searchable = true };
                ;
                querySearch = querySearch.Where(a => a.searchable == true);
                filter = querySearch.Select(e => e.filler).ToList();
                sqlRaw += String.Join("or", filter);
                sqlRaw += "  ";               
            }
            sqlRaw += $@" {(fieldOrder == null ? "" : $" order by {fieldOrder.Relational().ColumnName}" +
                $" {(string.IsNullOrEmpty(typeOrder)? "desc":$"{typeOrder}")} ")} ";

            return sqlRaw;

        }
        public void AddRange(IEnumerable<T> entities)
        {
            try
            {
                _easyspaContext.Set<T>().AddRange(entities);
                _easyspaContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<int> AddRangeAsync(IEnumerable<T> entities)
        {
            await _easyspaContext.Set<T>().AddRangeAsync(entities);
            return _easyspaContext.SaveChanges();
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            try
            {
                _easyspaContext.Set<T>().RemoveRange(entities);
                _easyspaContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<int> DeleteRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                _easyspaContext.Set<T>().RemoveRange(entities);
                return await _easyspaContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }


        }
        public T LoadAllReference(T entity)
        {

            var refs = _easyspaContext.Entry(entity).References.Select(e=>e.Metadata.Name).Where(e=> !GlobalReferenceCustom.ListReference.Contains(e));
            refs.ToList().ForEach(e => {
                _easyspaContext.Entry(entity).Reference(e).Load();
            });
            return entity;
          
        }
        public T LoadAllReference(T entity, params string[] excludes)
        {

            var refs = _easyspaContext.Entry(entity).References.Select(e => e.Metadata.Name)
                .Where(e => !GlobalReferenceCustom.ListReference.Contains(e))
                .Where(e => !excludes.Contains(e)); 
            refs.ToList().ForEach(e => {
                _easyspaContext.Entry(entity).Reference(e).Load();
            });
            return entity;

        }
        public IQueryable<T> LoadAllInclude(IQueryable<T> rs)
        {
            var refs =  _easyspaContext.Entry(Activator.CreateInstance(typeof(T))).References.Select(e => e.Metadata.Name).Where(e => !GlobalReferenceCustom.ListReference.Contains(e)); ;        
            refs.ToList().ForEach(e =>
            {
                rs = rs.Include(e);
            });
            
            return rs;
        }
        public IQueryable<T> LoadAllInclude(IQueryable<T> rs, params string[] excludes)
        {
            var refs = _easyspaContext.Entry(Activator.CreateInstance(typeof(T))).References.Select(e => e.Metadata.Name)
                .Where(e => !GlobalReferenceCustom.ListReference.Contains(e))
                .Where(e => !excludes.Contains(e)); ;
            refs.ToList().ForEach(e =>
            {
                rs = rs.Include(e);
            });

            return rs;
        }        
        public IQueryable<T> LoadAllCollecttion(IQueryable<T> rs)
        {
            var refs = _easyspaContext.Entry(Activator.CreateInstance(typeof(T))).Collections.Select(e => e.Metadata.Name).Where(e => !GlobalReferenceCustom.ListReference.Contains(e)); ;
            refs.ToList().ForEach(e =>
            {
                rs = rs.Include(e);
            });

            return rs;
        }
        public IQueryable<T> LoadAllCollecttion(IQueryable<T> rs, params string[] excludes)
        {
            var refs = _easyspaContext.Entry(Activator.CreateInstance(typeof(T))).Collections.Select(e => e.Metadata.Name)
                .Where(e => !GlobalReferenceCustom.ListReference.Contains(e))
                .Where(e=> !excludes.Contains(e)); 
            //refs.ToList().ForEach(e =>
            //{
            //    rs = rs.Include(e);
            //});

            foreach (var e in refs)
            {
                rs = rs.Include(e);

            }
            return rs;
        }
        public IQueryable<T> LoadMany2Many(IQueryable<T> rs, params string[] excludes)
        {
            var refs = _easyspaContext.Entry(Activator.CreateInstance(typeof(T))).Collections.Select(e => e.Metadata.Name)
                 .Where(e => !GlobalReferenceCustom.ListReference.Contains(e))
                 .Where(e => !excludes.Contains(e));


            refs.ToList().ForEach(e =>
            {
                rs = rs.Include(e);
            });

            foreach (var e in refs)
            {
                Type subObject = typeof( T);
              

            }
            return rs;
        }
        public async Task<IEnumerable<T>> LoadAllIncludeEnumAsync(IQueryable<T> rs)
        {
            var refs = _easyspaContext.Entry(Activator.CreateInstance(typeof(T))).References.Select(e => e.Metadata.Name).Where(e => !GlobalReferenceCustom.ListReference.Contains(e)); ;
            refs.ToList().ForEach(e =>
            {
                rs = rs.Include(e);
            });
           await rs.ToListAsync();
            return rs;
        }
        public T LoadAllCollecttion(T entity)
        {

            var refs = _easyspaContext.Entry(entity).Collections.Select(e => e.Metadata.Name).Where(e => !GlobalReferenceCustom.ListReference.Contains(e));
            refs.ToList().ForEach(e => {
                _easyspaContext.Entry(entity).Collection(e).Load();
            });
            return entity;

        }
        public T LoadAllCollecttion(T entity, params string[] excludes)
        {

            var refs = _easyspaContext.Entry(entity).Collections.Select(e => e.Metadata.Name)
                .Where(e => !GlobalReferenceCustom.ListReference.Contains(e))
                .Where(e => !excludes.Contains(e)); 
            refs.ToList().ForEach(e => {
                _easyspaContext.Entry(entity).Collection(e).Load();
            });
            return entity;

        }
        public async Task<int> EditRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                _easyspaContext.Set<T>().UpdateRange(entities);
                return await _easyspaContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public string BuildPrepareSQLStament(string query, string keyword)
        {
            return query;
            //string rs = "";
            //rs += " PREPARE stmt FROM '" + query + ";';  ";
            //rs += " SET @keyword  = '" + keyword + "'; ";
            //rs += " EXECUTE stmt; ";
            //rs += " DEALLOCATE PREPARE stmt;   ";
            //return rs;
        }
        public T Find(params object[] keyValues)
        {
            var entity = _easyspaContext.Set<T>().Find(keyValues);
            return entity;
        }

    }
}

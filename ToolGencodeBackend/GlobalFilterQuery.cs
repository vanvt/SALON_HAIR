using Microsoft.EntityFrameworkCore.Metadata;
using SALON_HAIR_ENTITY.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGencodeBackend
{
    public static class GlobalFilterQuery
    {
       public static  string tempalteInterface = @"
                    using Microsoft.EntityFrameworkCore;
                    using SALON_HAIR_ENTITY.Entities;
                    namespace SALON_HAIR_ENTITY.Extensions
                    {
                        public static class GlobalQueryFillter
                        {
                            public static ModelBuilder BuilCustomFillter( ModelBuilder  builder)
                            {

                                {xxx}

                                return builder;
                            }
                    }
                    ";



        public static string GenGlobalFilterQuery(salon_hairContext dbContext, string nameSpaceEntity = "SALON_HAIR_ENTITY.Entities.")
        {

            var rs = tempalteInterface;
            string builer = "";
            foreach (var item in dbContext.Model.GetEntityTypes())
            {
                string nameEnity = item.Name.Remove(0, nameSpaceEntity.Length);
                builer += $@" builder.Entity<{nameEnity}>().HasQueryFilter(e => !e.Status.Equals(""DELETED""));" + Environment.NewLine;
            }
            rs.Replace("{xxx}", "33333333");
            return rs;

        }

    }
}

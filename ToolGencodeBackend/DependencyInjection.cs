using SALON_HAIR_ENTITY.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGencodeBackend
{
    class DependencyInjection
    {
        public static string tempalteInterface = @"
                 
                                {buidlerString}

                    ";



        public static string GenDependencyInjection(salon_hairContext dbContext, string nameSpaceEntity = "SALON_HAIR_ENTITY.Entities.")
        {

            var rs = tempalteInterface;
            string builer = "";
            foreach (var item in dbContext.Model.GetEntityTypes())
            {
                string nameEnity = item.Name.Remove(0, nameSpaceEntity.Length);
                //string nameEnity = item.Name;
                builer += $@"    services.AddScoped<I{nameEnity}, {nameEnity}Service>();" + Environment.NewLine;
            }
            rs.Replace("{buidlerString}", builer);
            return rs;

        }
    }


}

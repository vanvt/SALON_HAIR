
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SALON_HAIR_ENTITY.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ToolGencodeBackend
{
    class Program
    {
        //static public void UpdateAlertLevelAsync()
        //{
        //    var _easyspaContext = new VAS_DEALERContext();
        //    try
        //    {
        //        var queryOrverDue = _easyspaContext.SpaUserInformation.Where(e => e.WorkTo < DateTime.Now && e.AlertLevel != 2);
        //        queryOrverDue.ToList().ForEach(e => {
        //            e.AlertLevel = 2;
        //            if (e.Workfollow.Order == 5)
        //            {
        //                var nextStep = _easyspaContext.SpaUserInformationWorkfollow.Where(x => x.Order == 6).Select(x => x.Id).FirstOrDefault();
        //                e.WorkfollowId = nextStep;
        //            }
        //            if (e.Workfollow.Order == 3)
        //            {
        //                var nextStep = _easyspaContext.SpaUserInformationWorkfollow.Where(x => x.Order == 4).Select(x => x.Id).FirstOrDefault();
        //                e.WorkfollowId = nextStep;
        //            }
        //        });
        //        _easyspaContext.SpaUserInformation.UpdateRange(queryOrverDue);

        //        var queryAlert = _easyspaContext.SpaUserInformation.Where(e => e.WorkTo.Value.AddDays(e.Workfollow.AlertBeforeDay.Value) < DateTime.Now);
        //        queryAlert.ToList().ForEach(e => e.AlertLevel = 1);
        //        _easyspaContext.SpaUserInformation.UpdateRange(queryOrverDue);
        //        _easyspaContext.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }


        //}
        static void Main(string[] args)
        {

            var db = new salon_hairContext();
            //UpdateAlertLevelAsync();
            var nameContext = "salon_hairContext";
            string nameSpaceEntity = "SALON_HAIR_ENTITY.Entities.";
            string nameSpaceCore = "SALON_HAIR_CORE";

            foreach (var item in db.Model.GetEntityTypes())
            {
               
                string nameEnity = item.Name.Remove(0, nameSpaceEntity.Length);
                string intanceName = Char.ToLowerInvariant(nameEnity[0]) + nameEnity.Substring(1);
                //LoopProperties(item);
                //Console.WriteLine($"services.AddScoped<I{nameEnity}, {nameEnity}Service>();");

                //WriteFile(ControllerTemplate.tempalte, nameEnity, $"{nameEnity}sController.cs", @"C:\Users\votha\OneDrive\Desktop\test\update");


                LoopProperties(item);

                WriteFile(InterfaceTemlate.tempalteInterface.Replace("{nameSpaceEntity}", nameSpaceEntity), nameEnity, $"I{nameEnity}.cs", @"C:\Users\votha\OneDrive\Desktop\test\Interface");
                WriteFile(ServiceTemplateUpdate.tmp.Replace("{nameSpaceEntity}", nameSpaceEntity), nameEnity, $"{nameEnity}Service.cs", @"C:\Users\votha\OneDrive\Desktop\test\Service");
                WriteFile(ControllerUpdatedTemplate.tempalte, nameEnity, $"{nameEnity}sController.cs", @"C:\Users\votha\OneDrive\Desktop\test\Controller");
                //WriteFile(InterfaceTemlate.tempalteInterface, nameEnity, $"I{nameEnity}.cs", @"C:\Users\votha\source\repos\ADMIN_EASYSPA\ADMIN_EASYSPA_CORE\Interface");

                //depen += $"services.AddScoped<I{nameEnity}, {nameEnity}Service>();\n";
            }
            //System.IO.File.WriteAllText(@"C:\Users\votha\OneDrive\Desktop\test\sss\cccccc.cs", depen);

            Console.Read();

        }

        static public void WriteFile(string tempalte, string nameEntity, string nameFile, string folder)
        {
            string intanceName = Char.ToLowerInvariant(nameEntity[0]) + nameEntity.Substring(1);
            System.IO.File.WriteAllText($@"{folder}\{nameFile}", tempalte.Replace("{ClassName}", nameEntity).Replace("{InstanceName}", intanceName));
        }
        static public bool CheckHasUpdatedField(IEnumerable<IProperty> properties)
        {

            foreach (var x in properties)
            {
                if (x.Name.Equals("CreatedBy"))
                {
                    return true;
                }
            }

            return false;
        }
        static public bool CheckHasIdField(IEnumerable<IProperty> properties)
        {

            foreach (var x in properties)
            {
                if (x.Name.Equals("Id"))
                {
                    return true;
                }
            }

            return false;
        }
        static public void LoopProperties(IEntityType entityType)
        {
            foreach (var props in entityType.GetProperties())
            {
                PropertyInfo x = props.PropertyInfo;
                //var paramField = $"@p{ ++index}";
                var s = x.PropertyType;
               var xx =  typeof(string);
                if(x.PropertyType == typeof(string))
                {

                }
                string type = props.Name ;
                string field = props.Relational().ColumnName;
                //keyValuePairs.Add(paramField, field);
                //parameters.Add(new SqlParameter(paramField, field));
             
            }
        }

    }
}

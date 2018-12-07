using System;
using System.Collections.Generic;
using System.Text;

namespace SALON_HAIR_ENTITY.StoredProcedure
{
   public static class Commision
    {
        public static string CommisionService =  $@"
 
        INSERT IGNORE INTO  `salon_hair`.`commission_service`
        (`staff_id`,
        `service_id`,
        `salon_branch_id`
        )
        SELECT a.id 'staff_id' ,b.id 'service_id', c.id 'salon_branch_id' FROM 
        salon_hair.staff as a  
        cross join salon_hair.service as b
        cross join salon_hair.salon_branch as c 
        where a.salon_id = @salon_id and b.salon_id = @salon_id and c.salon_id = @salon_id
            ";


        public static string CommisionProduct = $@"
        
        INSERT IGNORE INTO  `salon_hair`.`commission_product`
        (`staff_id`,
        `product_id`,
        `salon_branch_id`
        )
        SELECT a.id 'staff_id' ,b.id 'product_id', c.id 'salon_branch_id' FROM 
        salon_hair.staff as a 
        cross join salon_hair.product as b
        cross join salon_hair.salon_branch as c     
        where a.salon_id =  @salon_id and b.salon_id =  @salon_id and c.salon_id =  @salon_id
";

        public static string CommisionPackage = $@"
        
         INSERT IGNORE INTO  `salon_hair`.`commission_package`
        (`staff_id`,
        `package_id`,
        `salon_branch_id`
        )
        SELECT a.id 'staff_id' ,b.id 'package_id', c.id 'salon_branch_id' FROM 
        salon_hair.staff as a 
        cross join salon_hair.package as b
        cross join salon_hair.salon_branch as c             
        where a.salon_id =  @salon_id and b.salon_id =  @salon_id and c.salon_id =  @salon_id
";
    }
}



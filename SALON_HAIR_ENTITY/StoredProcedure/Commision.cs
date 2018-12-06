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
        SELECT a.id 'staff_id' ,b.id 'service_id', c.id 'salon_branch_id' FROM salon_hair.staff as a
        cross join salon_hair.service as b
        cross join salon_hair.salon_branch as c 
            ";


        public static string CommisionProduct = $@"
        
        INSERT IGNORE INTO  `salon_hair`.`commission_product`
        (`staff_id`,
        `product_id`,
        `salon_branch_id`
        )
        SELECT a.id 'staff_id' ,b.id 'product_id', c.id 'salon_branch_id' FROM salon_hair.staff as a
        cross join salon_hair.product as b
        cross join salon_hair.salon_branch as c     

";
    }
}

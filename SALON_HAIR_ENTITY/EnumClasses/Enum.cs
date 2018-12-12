using System;
using System.Collections.Generic;
using System.Text;

namespace SALON_HAIR_ENTITY.Entities
{
    public static class INVOICEOBJECTTYPE
    {
        public const   string PACKAGE = "PACKAGE";
        public const string SERVICE = "SERVICE";
        public const string PRODUCT = "PRODUCT";
    }
    public static class CUSTOMERPACKAGETRANSACTIONACTION
    {
        public readonly static string USE = "USE";
        public readonly static string PAY = "PAY";
    }
    public static class OBJECTSTATUS
    {
        public readonly static string ENABLE = "ENABLE";
        public readonly static string DISABLE = "DISABLE"; 
        public readonly static string DELETED = "DELETED";
    }
    public static class WAREHOUSETRANSATIONACTION
    {
        public readonly static string IMPORT = "IMPORT";
        public readonly static string EXPORT = "EXPORT";
        public readonly static string SALE = "SALE";
    }
}

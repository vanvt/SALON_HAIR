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
    public static class PAYSTATUS
    {
        public const string UNPAID = "UNPAID";
        public const string PAID = "PAID";
        public const string CANCEL = "CANCEL";
    }
    public static class BOOKINGSTATUS
    {
        public const  string CHECKIN = "CHECKIN";
        public const string CHECKOUT = "CHECKOUT";
        public const  string NEW = "NEW";
        public const  string CONFIRMED = "CONFIRMED";
    }
    public static class CLAIMUSER
    {
        public const string SUB = "sub";
        public const string ROLE = "role";
        public const string NAME = "name";
        public const string EMAILADDRESS = "emailAddress";
        public const string SALONID = "salonId";
        public const string EXP = "exp";
        public const string ISS = "iss";
        public const string AUD = "aud"; 
    }
    public static class CASHBOOKTRANSACTIONACTION
    {
        public const string INCOME = "INCOME";
        public const string OUTCOME = "OUTCOME";
    }
    public static class DISCOUNTUNIT
    {
        public const string PERCENT = "PERCENT";
        public const string MONEY = "MONEY";
    }
    public static class SYSTEMDEFAULT
    {
        public const string PASSWORD = "Abc123456";
    }
}

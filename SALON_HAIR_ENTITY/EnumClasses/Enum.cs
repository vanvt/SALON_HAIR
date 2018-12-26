using System;
using System.Collections.Generic;
using System.Text;

namespace SALON_HAIR_ENTITY.Entities
{
    public static class INVOICEOBJECTTYPE
    {
        public const string PACKAGE = "PACKAGE";
        public const string PACKAGE_ON_BILL = "PACKAGE_ON_BILL";
        public const string SERVICE = "SERVICE";
        public const string PRODUCT = "PRODUCT";
        public const string EXTRA = "EXTRA";

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
        public const string CHECKIN = "CHECKIN";
        public const string CHECKOUT = "CHECKOUT";
        public const string NEW = "NEW";
        public const string CONFIRMED = "CONFIRMED";
        public const string PREPAID = "PREPAID";
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
    public static class CASH_BOOK_TRANSACTION_CATEGORY
    {
        public const string SALE = "SALE";
        public const string PREPAY = "PREPAY";
        public const string DEBT_RECOVERY = "DEBT_RECOVERY";
    }
    public static class GENERATECODE {
        public const string BOOKING = "ES";
        public const string CASHBOOKTRANSACTION = "ES";
        public const string INVOICE = "ES";
        public const string FORMATSTRING = "000000";
    };
    public static class PAYMENT_METHOD {
        public const string CASH = "CASH";
        public const string BANK_TRANSFER = "BANK_TRANSFER";
        public const string SWIPE_CARD = "SWIPE_CARD";
        public const string OTHER = "OTHER";
        public const string DEBIT = "DEBIT";
    };
    public static class DEPT_BEHAVIOR {
        public const string PAY = "PAY";
        public const string DEBIT = "DEBIT";
    };
}

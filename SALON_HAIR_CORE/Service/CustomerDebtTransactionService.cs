

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace SALON_HAIR_CORE.Service
{
    public class CustomerDebtTransactionService: GenericRepository<CustomerDebtTransaction> ,ICustomerDebtTransaction
    {
        private salon_hairContext _salon_hairContext;
        private ISysObjectAutoIncreament _sysObjectAutoIncreament;
        public CustomerDebtTransactionService(ISysObjectAutoIncreament sysObjectAutoIncreament,salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
            _sysObjectAutoIncreament = sysObjectAutoIncreament;
        }        
        public new void Edit(CustomerDebtTransaction customerDebtTransaction)
        {
            customerDebtTransaction.Updated = DateTime.Now;

            base.Edit(customerDebtTransaction);
        }
        public async new Task<int> EditAsync(CustomerDebtTransaction customerDebtTransaction)
        {            
            customerDebtTransaction.Updated = DateTime.Now;         
            return await base.EditAsync(customerDebtTransaction);
        }
        public new async Task<int> AddAsync(CustomerDebtTransaction customerDebtTransaction)
        {
            customerDebtTransaction.Created = DateTime.Now;
            return await base.AddAsync(customerDebtTransaction);
        }
        public new void Add(CustomerDebtTransaction customerDebtTransaction)
        {
            customerDebtTransaction.Created = DateTime.Now;
            base.Add(customerDebtTransaction);
        }
        public new void Delete(CustomerDebtTransaction customerDebtTransaction)
        {
            customerDebtTransaction.Status = "DELETED";
            base.Edit(customerDebtTransaction);
        }
        public new async Task<int> DeleteAsync(CustomerDebtTransaction customerDebtTransaction)
        {
            customerDebtTransaction.Status = "DELETED";
            return await base.EditAsync(customerDebtTransaction);
        }

        public async Task AddAsyncAsGenCashBookAsync(CustomerDebtTransaction customerDebtTransaction)
        {
            var cashBookTransactions = new List<CashBookTransaction>();
            //Get payment Method booking
            // var paymentMethod = _salon_hairContext.CustomerDebtTransactionPayment.Where(e => e.CustomerDebtTransactionId == customerDebtTransaction.Id);
            var cashBookTransactionCategoryId = _salon_hairContext.CashBookTransactionCategory
           .Where(e => e.Code.Equals(CASH_BOOK_TRANSACTION_CATEGORY.DEBT_RECOVERY))
           .Where(e => e.SalonId == customerDebtTransaction.SalonId).Select(e => e.Id).FirstOrDefault();

            var sysObjectAutoIncreamentService = _sysObjectAutoIncreament.GetCodeByObjectAsyncWithoutSave(_salon_hairContext, nameof(CashBookTransaction), customerDebtTransaction.SalonId);

            customerDebtTransaction.CustomerDebtTransactionPayment.ToList().ForEach(e => {
                // Add CashBookTransaction for every Payment Method
                cashBookTransactions.Add(new CashBookTransaction
                {
                    Action = CASHBOOKTRANSACTIONACTION.INCOME,
                    Created = DateTime.Now,
                    CreatedBy = customerDebtTransaction.CreatedBy,
                    CustomerId = customerDebtTransaction.CustomerId,
                    PaymentMethodId = e.PaymentMethodId,
                    Money = e.Total,
                    SalonBranchId = customerDebtTransaction.SalonBranchId,
                    SalonId = customerDebtTransaction.SalonId,
                    CashBookTransactionCategoryId = cashBookTransactionCategoryId,
                    Code = GENERATECODE.BOOKING + sysObjectAutoIncreamentService.ObjectIndex.ToString(GENERATECODE.FORMATSTRING),
                    
                });
                sysObjectAutoIncreamentService.ObjectIndex++;
            });

            await _sysObjectAutoIncreament.CreateOrUpdateAsync(_salon_hairContext, sysObjectAutoIncreamentService);
            await _salon_hairContext.CashBookTransaction.AddRangeAsync(cashBookTransactions);
            customerDebtTransaction.Money = customerDebtTransaction.CustomerDebtTransactionPayment.Sum(e => e.Total);
            _salon_hairContext.CustomerDebtTransaction.Add(customerDebtTransaction);          

            await _salon_hairContext.SaveChangesAsync();
        }
    }
}
    
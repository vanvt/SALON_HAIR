
using System.Threading.Tasks;
using SALON_HAIR_CORE.Repository;
using SALON_HAIR_ENTITY.Entities;

namespace SALON_HAIR_CORE.Interface
{
    public interface IInvoiceDetail : IGenericRepository<InvoiceDetail>
    {
        Task AddAsServiceAsync(InvoiceDetail invoiceDetail);
        Task AddAsPackgeAsync(InvoiceDetail invoiceDetail);
        string GetObjectName(InvoiceDetail invoiceDetail);
        Task EditAsServiceAsync(InvoiceDetail invoiceDetail);
        Task EditAsPackgeAsync(InvoiceDetail invoiceDetail);
        InvoiceDetail GetObjectDetail(InvoiceDetail invoiceDetail);
        Task EditAsServiceAsync(InvoiceDetail invoiceDetail, int? oldQuantity);
    }
}


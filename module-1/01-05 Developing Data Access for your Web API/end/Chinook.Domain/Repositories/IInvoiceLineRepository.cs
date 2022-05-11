﻿using Chinook.Domain.Entities;

namespace Chinook.Domain.Repositories
{
    public interface IInvoiceLineRepository : IDisposable
    {
        Task<List<InvoiceLine>> GetAll();
        Task<InvoiceLine> GetById(int id);
        Task<List<InvoiceLine>> GetByInvoiceId(int id);
        Task<List<InvoiceLine>> GetByTrackId(int id);
        Task<InvoiceLine> Add(InvoiceLine newInvoiceLine);
        Task<bool> Update(InvoiceLine invoiceLine);
        Task<bool> Delete(int id);
    }
}
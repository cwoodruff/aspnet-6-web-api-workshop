using Chinook.Domain.ApiModels;
using Chinook.Domain.Entities;
using Chinook.Domain.Extensions;
using FluentValidation;

namespace Chinook.Domain.Supervisor;

public partial class ChinookSupervisor
{
    public async Task<IEnumerable<InvoiceLineApiModel>> GetAllInvoiceLine()
    {
        List<InvoiceLine> invoiceLines = await _invoiceLineRepository.GetAll();
        var invoiceLineApiModels = invoiceLines.ConvertAll();

        return invoiceLineApiModels;
    }

    public async Task<InvoiceLineApiModel> GetInvoiceLineById(int id)
    {
        var invoiceLine = await _invoiceLineRepository.GetById(id);
        if (invoiceLine == null) return null!;
        var invoiceLineApiModel = invoiceLine.Convert();
        invoiceLineApiModel.Track = await GetTrackById(invoiceLineApiModel.TrackId);
        invoiceLineApiModel.Invoice = await GetInvoiceById(invoiceLineApiModel.InvoiceId);
        if (invoiceLineApiModel.Track != null) invoiceLineApiModel.TrackName = invoiceLineApiModel.Track.Name;

        return invoiceLineApiModel;
    }

    public async Task<IEnumerable<InvoiceLineApiModel>> GetInvoiceLineByInvoiceId(int id)
    {
        var invoiceLines = await _invoiceLineRepository.GetByInvoiceId(id);
        return invoiceLines.ConvertAll();
    }

    public async Task<IEnumerable<InvoiceLineApiModel>> GetInvoiceLineByTrackId(int id)
    {
        var invoiceLines = await _invoiceLineRepository.GetByTrackId(id);
        return invoiceLines.ConvertAll();
    }

    public async Task<InvoiceLineApiModel> AddInvoiceLine(InvoiceLineApiModel newInvoiceLineApiModel)
    {
        await _invoiceLineValidator.ValidateAndThrowAsync(newInvoiceLineApiModel);

        var invoiceLine = newInvoiceLineApiModel.Convert();

        invoiceLine = await _invoiceLineRepository.Add(invoiceLine);
        newInvoiceLineApiModel.Id = invoiceLine.Id;
        return newInvoiceLineApiModel;
    }

    public async Task<bool> UpdateInvoiceLine(InvoiceLineApiModel invoiceLineApiModel)
    {
        await _invoiceLineValidator.ValidateAndThrowAsync(invoiceLineApiModel);

        var invoiceLine = await _invoiceLineRepository.GetById(invoiceLineApiModel.InvoiceId);

        if (invoiceLine == null) return false;
        invoiceLine.Id = invoiceLineApiModel.Id;
        invoiceLine.InvoiceId = invoiceLineApiModel.InvoiceId;
        invoiceLine.TrackId = invoiceLineApiModel.TrackId;
        invoiceLine.UnitPrice = invoiceLineApiModel.UnitPrice;
        invoiceLine.Quantity = invoiceLineApiModel.Quantity;

        return await _invoiceLineRepository.Update(invoiceLine);
    }

    public Task<bool> DeleteInvoiceLine(int id)
        => _invoiceLineRepository.Delete(id);
}
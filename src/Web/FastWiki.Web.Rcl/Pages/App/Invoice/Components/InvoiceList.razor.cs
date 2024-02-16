using Masa.Blazor.Pro.Pages.App.Invoice;

namespace FastWiki.Web.Rcl.Pages.App.Invoice.Components;

public partial class InvoiceList
{
    List<int> _pageSizes = new() { 10, 25, 50, 100 };

    private List<DataTableHeader<InvoiceRecordDto>> _headers = new List<DataTableHeader<InvoiceRecordDto>>
    {
        new (){ Text="#" , Value= nameof(InvoiceRecordDto.Id)},
        new (){ Text="Client" , Value= nameof(InvoiceRecordDto.Client)},
        new (){ Text="State" , Value= nameof(InvoiceRecordDto.State)},
        new (){ Text="Total" , Value= nameof(InvoiceRecordDto.Total)},
        new (){ Text="Issued Date" , Value= nameof(InvoiceRecordDto.Date)},
        new (){ Text="Balance" , Value= nameof(InvoiceRecordDto.Balance)},
        new (){ Text="Actions" , Value = "Action", Sortable = false},
    };

    private List<InvoiceStateDto> _stateItems = InvoiceService.GetStateList();

    InvoicePage _invoicePage = new(InvoiceService.GetInvoiceRecordList());

    private void NavigateToPreview(int id)
    {
        NavigationManager.NavigateTo($"apps/invoice/preview/{id}");
    }

    private void NavigateToEdit(int id)
    {
        NavigationManager.NavigateTo($"apps/invoice/edit/{id}");
    }

    private void NavigateToAdd()
    {
        NavigationManager.NavigateTo($"apps/invoice/add");
    }
}



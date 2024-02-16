namespace FastWiki.Web.Rcl.Pages.App.Invoice
{
    public partial class Preview
    {
        private bool? _showSendInvoice;
        private bool? _showAddPayment;
        private InvoiceRecordDto? _invoiceRecord;

        [Parameter]
        public int? Id { get; set; }

        public InvoiceRecordDto InvoiceRecord => _invoiceRecord ??= InvoiceService.GetInvoiceRecordList().FirstOrDefault(i => i.Id == Id) ?? InvoiceService.GetInvoiceRecordList().First();

        private void NavigateToEdit()
        {
            NavigationManager.NavigateTo($"apps/invoice/edit/{Id ?? 0}");
        }
    }
}

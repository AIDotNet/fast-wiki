namespace FastWiki.Web.Rcl.Pages.App.Invoice
{
    public partial class Add : ProComponentBase
    {
        private bool? _showAddPayment;
        private string _invoiceTo = "";
        private readonly List<string> _invoices = new()
        {
            "Hall-Robbins PLC",
            " Mccann LLC and Sons",
            "Leonard-Garcia and Sons",
            "Smith, Miller and Henry LLC",
            "Garcia-Cameron and Sons"
        };
        private string _payment = "";
        private List<string> _payments = new()
        {
            "Bank Account",
            "PayPal",
            "UPI Transfer"
        };
        private readonly List<int> _taxs = new()
        {
            0,
            1,
            10,
            14,
            18
        };
        private List<BillDto> _bills = new()
        {
            new BillDto()
        };
        private string _note = "It was a pleasure working with you and your team. We hope you will Favorite us in mind for future freelance projects. Thank You!";

        [Parameter]
        public int? Id { get; set; }

        public bool IsEdit => Id is not null;

        public InvoiceRecordDto InvoiceRecord = InvoiceService.GetInvoiceRecordList().First();

        public void OnBillChange(BillDto oldValue, BillDto newValue)
        {
            oldValue.Set(newValue);
        }

        private static string ConvertText(int value)
        {
            return $"{value}%";
        }
    }
}

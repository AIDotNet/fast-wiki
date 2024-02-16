using Masa.Blazor.Pro.Pages.App.ECommerce.Shop;

namespace FastWiki.Web.Rcl.Pages.App.ECommerce.Shop
{
    public partial class Shop : ProComponentBase
    {
        readonly List<MultiRangeDto> _multiRanges = ShopService.GetMultiRangeList();
        readonly List<string> _categories = ShopService.GetCategortyList();
        readonly List<string> _brands = ShopService.GetBrandList();
        readonly ShopPage _shopData = new(ShopService.GetGoodsList());
        string _multiRangeText = "All";

        [Inject]
        public NavigationManager Nav { get; set; } = default!;

        string MultiRangeText
        {
            get => _multiRangeText;
            set
            {
                _multiRangeText = value;
                _shopData.MultiRange = _multiRanges.First(item => item.Text == value);
            }
        }

        protected override void OnInitialized()
        {
            _shopData.MultiRange = _multiRanges[0];
        }

        private void NavigateToDetails(Guid id)
        {
            Nav.NavigateTo($"app/ecommerce/details/{id}");
        }

        private void NavigateToOrder()
        {
            Nav.NavigateTo($"app/ecommerce/order");
        }
    }
}

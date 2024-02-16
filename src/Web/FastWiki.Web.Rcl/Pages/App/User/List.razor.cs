using Masa.Blazor.Pro.Pages.App.User;

namespace FastWiki.Web.Rcl.Pages.App.User
{
    public partial class List
    {
        public bool? _visible;
        public UserPage _userPage = new(UserService.GetList());
        private List<int> _pageSizes = new() { 10, 25, 50, 100 };
        private readonly List<DataTableHeader<UserDto>> _headers = new()
        {
            new() { Text = "USER", Value = nameof(UserDto.UserName), CellClass = "" },
            new() { Text = "EMAIL", Value = nameof(UserDto.Email) },
            new() { Text = "ROLE", Value = nameof(UserDto.Role) },
            new() { Text = "PLAN", Value = nameof(UserDto.Plan) },
            new() { Text = "STATUS", Value = nameof(UserDto.Status) },
            new() { Text = "ACTIONS", Value = "Action", Sortable = false }
        };
        private readonly Dictionary<string, string> _roleIconMap = UserService.GetRoleIconMap();

        private void NavigateToDetails(string id)
        {
            Nav.NavigateTo($"/app/user/view/{id}");
        }

        private void NavigateToEdit(string id)
        {
            Nav.NavigateTo($"/app/user/edit/{id}");
        }

        private void AddUserData(UserDto userData)
        {
            _userPage.UserDatas.Insert(0, userData);
        }
    }
}

namespace FastWiki.Web.Rcl.Pages.App.Todo
{
    public partial class Todo
    {
        private readonly Dictionary<string, string> _tagColorMap = TodoService.GetTagColorMap();
        private readonly string[] _avas = TodoService.GetAvatars();

        private TodoDto _selectItem = new();
        private string? _filterText;
        private bool? _visible = false;
        private string? _inputText;
        private List<TodoDto> _thisList = new();
        private readonly List<TodoDto> _dataList = TodoService.GetList();

        [Parameter]
        public string? FilterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                _thisList = GetFilterList(_filterText);
            }
        }

        public int GenerateId() => _dataList.Count + 1;

        public List<TodoDto> GetFilterList(string? filter)
        {
            return filter switch
            {
                "important" => _dataList.Where(item => item.IsImportant && !item.IsDeleted).ToList(),
                "completed" => _dataList.Where(item => item.IsCompleted && !item.IsDeleted).ToList(),
                "deleted" => _dataList.Where(item => item.IsDeleted).ToList(),
                "team" => _dataList.Where(item => item.Tag.Contains("Team")).ToList(),
                "low" => _dataList.Where(item => item.Tag.Contains("Low")).ToList(),
                "medium" => _dataList.Where(item => item.Tag.Contains("Medium")).ToList(),
                "high" => _dataList.Where(item => item.Tag.Contains("High")).ToList(),
                "update" => _dataList.Where(item => item.Tag.Contains("Update")).ToList(),
                _ => _dataList.Where(item => !item.IsDeleted).ToList(),
            };
        }

        private void ShowDetail(TodoDto item)
        {
            _visible = true;
            _selectItem = item;
        }

        private void ResetSort()
        {
            _thisList = _thisList.OrderBy(d => d.Id).ToList();
        }

        private void SortbyAssignee()
        {
            _thisList = _thisList.OrderBy(d => d.Assignee).ToList();
        }

        private void SortbyDueDate()
        {
            _thisList = _thisList.OrderBy(d => d.DueDate).ToList();
        }

        private void InputTextChanged(string? text)
        {
            if (!string.IsNullOrWhiteSpace(text))
                _thisList = _dataList.Where(item => item.Title.Contains(text)).ToList();
            else
                _thisList = _dataList;
        }

        public string? InputText
        {
            get { return _inputText; }
            set
            {
                _inputText = value;
                InputTextChanged(_inputText);
            }
        }

        public void AddData(TodoDto data)
        {
            _dataList.Insert(0, data);

            RefreshData();
        }

        public void UpdateData(TodoDto data)
        {
            var index = _dataList.FindIndex(d => d.Id == data.Id);
            _dataList[index] = data;

            RefreshData();
        }

        public void RefreshData()
        {
            FilterText = _filterText;

            StateHasChanged();
        }
    }
}

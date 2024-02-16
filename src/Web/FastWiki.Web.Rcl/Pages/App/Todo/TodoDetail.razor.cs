using Masa.Blazor;
using Microsoft.AspNetCore.Components.Forms;

namespace FastWiki.Web.Rcl.Pages.App.Todo;

public partial class TodoDetail
{
    private readonly List<SelectData> _tagList = TodoService.GetTagList();
    private readonly List<SelectData> _assigneeList = TodoService.GetAssigneeList();
    private MForm? _mForm;
    private bool _isEdit;
    private TodoDto _selectData = new();

    private string CompletedColor { get { return _selectData.IsCompleted ? "text-capitalize neutral-lighten-5 neutral-lighten-2--text" : "theme--dark primary"; } }

    private string CompletedText { get { return _selectData.IsCompleted ? "Completed" : "Mark Complete"; } }

    [CascadingParameter]
    public FastWiki.Web.Rcl.Pages.App.Todo.Todo Todo { get; set; } = default!;

    [Parameter]
    public bool? Value { get; set; }

    [Parameter]
    public TodoDto? SelectItem { get; set; }

    [Parameter]
    public EventCallback<bool?> ValueChanged { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    private async Task HideNavigationDrawer()
    {
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(false);
        }
    }

    private void Complete()
    {
        _selectData.IsCompleted = !_selectData.IsCompleted;
        _selectData.IsChecked = _selectData.IsCompleted;
    }

    private void HandleCloseClick(string lable)
    {
        _selectData.Tag.Remove(lable);
    }

    protected override void OnParametersSet()
    {
        if (SelectItem == null)
        {
            SelectItem = new TodoDto();
            _isEdit = false;
        }
        else
        {
            _isEdit = true;
        }

        _selectData = new TodoDto
        {
            Id = SelectItem.Id,
            IsChecked = SelectItem.IsChecked,
            Assignee = SelectItem.Assignee,
            Avatar = SelectItem.Avatar,
            Description = SelectItem.Description,
            IsCompleted = SelectItem.IsCompleted,
            IsDeleted = SelectItem.IsDeleted,
            IsImportant = SelectItem.IsImportant,
            DueDate = SelectItem.DueDate,
            Title = SelectItem.Title
        };
        _selectData.Tag.AddRange(SelectItem.Tag);

        if (ValueChanged.HasDelegate && Value is not true && _mForm != null)
        {
            _mForm.ResetValidation();
        }
    }

    private async Task AddAsync(EditContext context)
    {
        var success = context.Validate();
        if (success)
        {
            _selectData.Id = Todo.GenerateId();
            Todo.AddData(_selectData);
            await HideNavigationDrawer();

            NavigationManager.NavigateTo("app/todo");
        }
    }

    private async Task UpdateAsync(EditContext context)
    {
        var success = context.Validate();
        if (success)
        {
            var data = (TodoDto)context.Model;
            Todo.UpdateData(data);
            await HideNavigationDrawer();
        }
    }

    private void Reset()
    {
        if (_mForm != null)
        {
            _mForm.ResetValidation();
        }

        if (SelectItem != null)
        {
            _selectData = new TodoDto
            {
                Id = SelectItem.Id,
                IsChecked = SelectItem.IsChecked,
                Assignee = SelectItem.Assignee,
                Avatar = SelectItem.Avatar,
                Description = SelectItem.Description,
                IsCompleted = SelectItem.IsCompleted,
                IsDeleted = SelectItem.IsDeleted,
                IsImportant = SelectItem.IsImportant,
                DueDate = SelectItem.DueDate,
                Tag = SelectItem.Tag,
                Title = SelectItem.Title
            };
        }
    }

    private async Task DeleteAsync()
    {
        _selectData.IsDeleted = true;
        Todo.UpdateData(_selectData);
        await HideNavigationDrawer();
    }
}
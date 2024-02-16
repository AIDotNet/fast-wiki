namespace Masa.Blazor.Pro;

public abstract class ProComponentBase : ComponentBase
{
    [Inject]
    protected I18n I18n { get; set; } = null!;
    
    [CascadingParameter(Name = "CultureName")]
    protected string? Culture { get; set; }

    protected string T(string? key, params object[] args)
    {
        return I18n.T(key, args: args);
    }
}

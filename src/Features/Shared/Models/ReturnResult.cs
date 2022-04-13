namespace BlazorApp.Features.Shared.Models;

public class ReturnResult<T>
{
    public bool IsOk { get; set; }
    public string ResultMessage { get; set; } = string.Empty;
    public T? Result { get; set; } = default;
}
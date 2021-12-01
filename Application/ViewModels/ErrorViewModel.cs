namespace Application.ViewModels;

/// <summary>
/// Модель для страницы ошибки
/// </summary>
public class ErrorViewModel
{
    public string RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
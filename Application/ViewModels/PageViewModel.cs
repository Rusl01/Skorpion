namespace Application.ViewModels;

/// <summary>
/// Модель для пагинации
/// </summary>
public class PageViewModel
{
    /// <summary>
    /// Номер текущей страницы
    /// </summary>
    public int PageNumber { get; private set; }
    /// <summary>
    /// Общее количество страниц
    /// </summary>
    public int TotalPages { get; private set; }
 
    public PageViewModel(int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }
 
    /// <summary>
    /// Функция проверяет есть ли предыдущая страница с данными
    /// </summary>
    public bool HasPreviousPage
    {
        get
        {
            return (PageNumber > 1);
        }
    }
 
    /// <summary>
    /// Функция проверяет есть ли следующая страница с данными
    /// </summary>
    public bool HasNextPage
    {
        get
        {
            return (PageNumber < TotalPages);
        }
    }
}
namespace SolarLab.Academy.Contracts.Universal;

/// <summary>
/// Результат в виде списка сущностей.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ResultWithPagination<T>
{
    /// <summary>
    /// Список сущностей.
    /// </summary>
    public IEnumerable<T> Result { get; set; }

    /// <summary>
    /// Доступное количество страниц.
    /// </summary>
    public int AvailablePages { get; set; }
}
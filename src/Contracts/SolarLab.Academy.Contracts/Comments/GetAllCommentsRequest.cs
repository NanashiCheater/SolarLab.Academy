namespace SolarLab.Academy.Contracts.Universal;

/// <summary>
/// Запрос на получения сущностей с пагинацией.
/// </summary>
public class GetAllCommentsRequest
{
    /// <summary>
    /// Идентификатор объявления.
    /// </summary>
    public Guid AnnouncementId { get; set; }
    /// <summary>
    /// Номер страницы.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Количечество сущностей на странице.
    /// </summary>
    public int Batchsize { get; set; } = 10;
}
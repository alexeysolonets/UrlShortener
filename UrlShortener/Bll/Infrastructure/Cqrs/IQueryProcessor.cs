namespace UrlShortener.Bll.Infrastructure.Cqrs
{
    /// <summary>
    /// Интерфейс обработчика запросов
    /// </summary>
    public interface IQueryProcessor
    {
        TResult Process<TResult>(IQuery<TResult> query);
    }
}

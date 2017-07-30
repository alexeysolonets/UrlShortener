namespace UrlShortener.Bll.Infrastructure.Cqrs
{
    /// <summary>
    /// Обработчик запроса
    /// </summary>
    /// <typeparam name="TQuery">Тип запроса</typeparam>
    /// <typeparam name="TResult">Тип результата</typeparam>
    public interface IQueryHandler<TQuery, TResult>
        where TQuery: IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}

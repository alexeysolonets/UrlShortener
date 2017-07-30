namespace UrlShortener.Bll.Infrastructure.Cqrs
{
    /// <summary>
    /// Обработчик запросов по умолчанию
    /// </summary>
    public class QueryProcessor : IQueryProcessor
    {
        private GetInstance GetInstance { get; set; }

        public QueryProcessor(GetInstance getInstance)
        {
            GetInstance = getInstance;
        }

        public TResult Process<TResult>(IQuery<TResult> query)
        {
            // TODO: сделать проверку, чтобы не могло произойти переполнения буфера от циклической зависимости Query
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = GetInstance(handlerType);
            return handler.Handle((dynamic)query);
        }
    }
}

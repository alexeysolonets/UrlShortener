namespace UrlShortener.Bll.Infrastructure.Cqrs
{
    /// <summary>
    /// Команда
    /// </summary>
    public interface ICommand
    {
    }

    /// <summary>
    /// Команда с результатом
    /// </summary>
    public interface ICommand<TResult>
    {
    }
}

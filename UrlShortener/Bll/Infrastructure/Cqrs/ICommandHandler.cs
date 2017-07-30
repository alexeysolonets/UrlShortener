namespace UrlShortener.Bll.Infrastructure.Cqrs
{
    /// <summary>
    /// Обработчик команды
    /// </summary>
    /// <typeparam name="TCommand">Тип команды</typeparam>
    public interface ICommandHandler<TCommand>
        where TCommand: ICommand
    {
        void Handle(TCommand command);
    }

    /// <summary>
    /// Обработчик команды с результатом
    /// </summary>
    /// <typeparam name="TCommand">Тип команды</typeparam>
    /// <typeparam name="TResult">Тип результата</typeparam>
    public interface ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        TResult Handle(TCommand command);
    }
}

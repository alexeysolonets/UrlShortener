using System.Collections.Generic;

namespace UrlShortener.Bll.Infrastructure.Cqrs
{
    /// <summary>
    /// Интерфейс обработчика команд
    /// </summary>
    public interface ICommandProcessor
    {
        /// <summary>
        /// Обработать команду
        /// </summary>
        /// <param name="command">Команда</param>
        void Process(ICommand command);

        /// <summary>
        /// Обработать несколько команд
        /// </summary>
        /// <param name="commands">Командаы</param>
        void ProcessAll<TCommand>(IEnumerable<TCommand> commands) where TCommand : ICommand;

        /// <summary>
        /// Обработать команду с результатом
        /// </summary>
        /// <typeparam name="TResult">Тип результата</typeparam>
        /// <param name="command">Команда</param>
        /// <returns>Результат выполнения команды</returns>
        TResult Process<TResult>(ICommand<TResult> command);
    }
}

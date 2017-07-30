using System.Collections.Generic;

namespace UrlShortener.Bll.Infrastructure.Cqrs
{
    /// <summary>
    /// Обработчик запросов по умолчанию
    /// </summary>
    public class CommandProcessor : ICommandProcessor
    {
        private GetInstance GetInstance { get; set; }

        public CommandProcessor(GetInstance getInstance)
        {
            GetInstance = getInstance;
        }

        /// <summary>
        /// Обработать команду
        /// </summary>
        /// <param name="command">Команда</param>
        public void Process(ICommand command)
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            dynamic handler = GetInstance(handlerType);
            handler.Handle((dynamic)command);
        }

        /// <summary>
        /// Обработать несколько команд
        /// </summary>
        /// <param name="commands">Командаы</param>
        public void ProcessAll<TCommand>(IEnumerable<TCommand> commands)
            where TCommand: ICommand
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(typeof(TCommand));
            dynamic handler = GetInstance(handlerType);

            foreach (var command in commands)
            {
                handler.Handle((dynamic)command);
            }
        }

        /// <summary>
        /// Обработаь команду с результатом
        /// </summary>
        /// <typeparam name="TResult">Тип команды</typeparam>
        /// <param name="command">Команда</param>
        /// <returns>Результат команды</returns>
        public TResult Process<TResult>(ICommand<TResult> command)
        {
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            dynamic handler = GetInstance(handlerType);
            var result = handler.Handle((dynamic)command);
            return result;
        }
    }
}

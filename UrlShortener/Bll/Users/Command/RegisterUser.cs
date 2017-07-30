using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Bll.Infrastructure.Cqrs;

namespace UrlShortener.Bll.Users.Command
{
    /// <summary>
    /// Зарегистрировать пользователя
    /// </summary>
    public class RegisterUser: ICommandHandler<RegisterUser.Command, long>
    {
        public class Command: ICommand<long>
        {
            // на будущее: данные пользователя
        }

        public long Handle(Command command)
        {
            throw new NotImplementedException();
        }
    }
}

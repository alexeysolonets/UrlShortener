using System;

namespace UrlShortener.Bll.Infrastructure.Cqrs
{
    /// <summary>
    /// Активатор экземпляров (для отвязки от реализации IoC-контейнера)
    /// </summary>
    public delegate dynamic GetInstance(Type type);
}

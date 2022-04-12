using System;

namespace Notes.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        // исключение получения сущности в БД
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) not found") { }
    }
}

using System;
using System.Collections.Generic;

namespace UGF.Elements.Runtime
{
    public interface IElementContext : IEnumerable<object>
    {
        T Get<T>();
        object Get(Type type);
        bool TryGet<T>(out T value);
        bool TryGet(Type type, out object value);
    }
}

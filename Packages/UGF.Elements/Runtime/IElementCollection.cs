using System;
using System.Collections.Generic;

namespace UGF.Elements.Runtime
{
    public interface IElementCollection : IReadOnlyList<IElement>
    {
        bool Contains(IElement element);
        void Add(IElement element);
        bool Remove(IElement element);
        void Clear();
        T Get<T>() where T : IElement;
        IElement Get(Type type);
        bool TryGet<T>(out T element) where T : IElement;
        bool TryGet(Type type, out IElement element);
    }
}

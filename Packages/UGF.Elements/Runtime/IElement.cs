using System;
using System.Collections.Generic;
using UGF.Initialize.Runtime;

namespace UGF.Elements.Runtime
{
    public interface IElement : IInitialize
    {
        IElement Parent { get; }
        bool HasParent { get; }
        IReadOnlyList<IElement> Children { get; }

        void Add(IElement element);
        void Remove(IElement element);
        T Get<T>() where T : IElement;
        IElement Get(Type type);
        bool TryGet<T>(out T element) where T : IElement;
        bool TryGet(Type type, out IElement element);
    }
}

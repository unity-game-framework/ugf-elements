using System;
using System.Collections;
using System.Collections.Generic;

namespace UGF.Elements.Runtime
{
    public class ElementCollection<TItem> : IElementCollection where TItem : class, IElement
    {
        public int Count { get { return m_elements.Count; } }
        public IElement this[int index] { get { return m_elements[index]; } }

        private readonly List<TItem> m_elements = new List<TItem>();

        public bool Contains(TItem element)
        {
            return m_elements.Contains(element);
        }

        public void Add(TItem element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));

            m_elements.Add(element);
        }

        public bool Remove(TItem element)
        {
            return m_elements.Remove(element);
        }

        public void Clear()
        {
            m_elements.Clear();
        }

        public T Get<T>() where T : IElement
        {
            return (T)Get(typeof(T));
        }

        public IElement Get(Type type)
        {
            if (!TryGet(type, out IElement element))
            {
                throw new ArgumentException($"Element by the specified type not found: '{type}'.");
            }

            return element;
        }

        public bool TryGet<T>(out T element) where T : IElement
        {
            if (TryGet(typeof(T), out IElement value))
            {
                element = (T)value;
                return true;
            }

            element = default;
            return false;
        }

        public bool TryGet(Type type, out IElement element)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            for (int i = 0; i < m_elements.Count; i++)
            {
                element = m_elements[i];

                if (type.IsInstanceOfType(element))
                {
                    return true;
                }
            }

            element = null;
            return false;
        }

        public List<TItem>.Enumerator GetEnumerator()
        {
            return m_elements.GetEnumerator();
        }

        bool IElementCollection.Contains(IElement element)
        {
            return Contains((TItem)element);
        }

        void IElementCollection.Add(IElement element)
        {
            Add((TItem)element);
        }

        bool IElementCollection.Remove(IElement element)
        {
            return Remove((TItem)element);
        }

        IEnumerator<IElement> IEnumerable<IElement>.GetEnumerator()
        {
            return m_elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_elements.GetEnumerator();
        }
    }
}

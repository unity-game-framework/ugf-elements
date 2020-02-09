using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UGF.Elements.Runtime
{
    public class ElementContext : IElementContext
    {
        public IReadOnlyList<object> Values { get; }

        private readonly List<object> m_values = new List<object>();

        public ElementContext()
        {
            Values = new ReadOnlyCollection<object>(m_values);
        }

        public void Add(object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            m_values.Add(value);
        }

        public void Remove(object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            m_values.Remove(value);
        }

        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        public object Get(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!TryGet(type, out object value))
            {
                throw new ArgumentException($"Value by the specified type not found: '{type}'.");
            }

            return value;
        }

        public bool TryGet<T>(out T value)
        {
            if (TryGet(typeof(T), out object result) && result is T cast)
            {
                value = cast;
                return true;
            }

            value = default;
            return false;
        }

        public bool TryGet(Type type, out object value)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            for (int i = 0; i < m_values.Count; i++)
            {
                value = m_values[i];

                if (type.IsInstanceOfType(value))
                {
                    return true;
                }
            }

            value = null;
            return false;
        }

        public List<object>.Enumerator GetEnumerator()
        {
            return m_values.GetEnumerator();
        }

        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            return m_values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_values.GetEnumerator();
        }
    }
}

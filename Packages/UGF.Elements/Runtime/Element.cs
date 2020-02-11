using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UGF.Initialize.Runtime;

namespace UGF.Elements.Runtime
{
    public class Element : InitializeBase, IElement, IEnumerable<IElement>
    {
        public IReadOnlyList<IElement> Children { get; }
        public IElement Parent { get { return m_parent ?? throw new ArgumentException("Parent not assigned."); } }
        public bool HasParent { get { return m_parent != null; } }

        private readonly List<IElement> m_children = new List<IElement>();
        private Element m_parent;

        public Element()
        {
            Children = new ReadOnlyCollection<IElement>(m_children);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            OnInitializeChildren();
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            OnUninitializeChildren();
        }

        protected virtual void OnInitializeChildren()
        {
            for (int i = 0; i < m_children.Count; i++)
            {
                m_children[i].Initialize();
            }
        }

        protected virtual void OnUninitializeChildren()
        {
            for (int i = m_children.Count - 1; i >= 0; i--)
            {
                m_children[i].Uninitialize();
            }
        }

        void IElement.Add(IElement element)
        {
            Add((Element)element);
        }

        void IElement.Remove(IElement element)
        {
            Remove((Element)element);
        }

        public void Add(Element element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            if (m_children.Contains(element)) throw new ArgumentException($"Element already child of this element: '{element}'.");
            if (IsInitialized && !element.IsInitialized) throw new ArgumentException($"Element not initialized: '{element}'.");

            if (element.m_parent != null)
            {
                element.OnParentClear();
                element.m_parent = null;
            }

            element.m_parent = this;
            element.OnParentChange();

            m_children.Add(element);

            OnChildAdd(element);
        }

        public void Remove(Element element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            if (!m_children.Contains(element)) throw new ArgumentException($"Element not the child of this element: '{element}'.");

            OnChildRemove(element);

            element.OnParentClear();
            element.m_parent = null;

            m_children.Remove(element);
        }

        public T Get<T>() where T : IElement
        {
            return (T)Get(typeof(T));
        }

        public IElement Get(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!TryGet(type, out IElement element))
            {
                throw new ArgumentException($"Element not found by the specified type: '{type}'.");
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

            for (int i = 0; i < m_children.Count; i++)
            {
                element = m_children[i];

                if (type.IsInstanceOfType(element))
                {
                    return true;
                }
            }

            element = null;
            return false;
        }

        protected virtual void OnChildAdd(Element child)
        {
        }

        protected virtual void OnChildRemove(Element child)
        {
        }

        protected virtual void OnParentChange()
        {
        }

        protected virtual void OnParentClear()
        {
        }

        public List<IElement>.Enumerator GetEnumerator()
        {
            return m_children.GetEnumerator();
        }

        IEnumerator<IElement> IEnumerable<IElement>.GetEnumerator()
        {
            return m_children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_children.GetEnumerator();
        }
    }
}

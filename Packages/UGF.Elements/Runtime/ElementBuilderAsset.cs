using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Elements.Runtime
{
    public abstract class ElementBuilderAsset : ScriptableObject, IElementBuilder
    {
        [SerializeField] private List<Object> m_children = new List<Object>();

        public List<Object> Children { get { return m_children; } }

        public IElement Build(IElementContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            IElement element = OnBuild(context);

            for (int i = 0; i < m_children.Count; i++)
            {
                if (m_children[i] is IElementBuilder builder)
                {
                    IElement child = builder.Build(context);

                    element.Add(child);
                }
            }

            return element;
        }

        protected abstract IElement OnBuild(IElementContext context);
    }
}

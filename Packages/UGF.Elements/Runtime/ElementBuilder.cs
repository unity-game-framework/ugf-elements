using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Elements.Runtime
{
    [Serializable]
    public class ElementBuilder : IElementBuilder
    {
        [SerializeReference] private List<IElementBuilder> m_children = new List<IElementBuilder>();

        public List<IElementBuilder> Children { get { return m_children; } }

        public IElement Build(IElementContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            IElement element = OnBuild(context);

            BuildChildren(element, context);

            return element;
        }

        protected virtual IElement OnBuild(IElementContext context)
        {
            return new Element();
        }

        protected void BuildChildren(IElement parent, IElementContext context)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (context == null) throw new ArgumentNullException(nameof(context));

            for (int i = 0; i < m_children.Count; i++)
            {
                IElementBuilder builder = m_children[i];
                IElement element = builder.Build(context);

                parent.Add(element);
            }
        }
    }
}

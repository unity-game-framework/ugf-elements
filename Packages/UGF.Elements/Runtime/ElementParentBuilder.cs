using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Elements.Runtime
{
    public class ElementParentBuilder : ElementBuilder
    {
        [SerializeField, AssetType(typeof(IElementBuilder))] private List<Object> m_children = new List<Object>();

        public List<Object> Children { get { return m_children; } }

        protected override IElement OnBuild(IElementContext context)
        {
            IElementParent parent = OnBuildParent(context);

            OnBuildChildren(parent, context);

            return parent;
        }

        protected virtual IElementParent OnBuildParent(IElementContext context)
        {
            return new ElementParent<IElement>();
        }

        protected virtual void OnBuildChildren(IElementParent parent, IElementContext context)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            for (int i = 0; i < m_children.Count; i++)
            {
                if (m_children[i] is IElementBuilder builder)
                {
                    IElement child = builder.Build(context);

                    parent.Children.Add(child);
                }
            }
        }
    }
}

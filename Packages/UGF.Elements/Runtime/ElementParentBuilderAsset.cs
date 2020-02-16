using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Elements.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Elements/ElementParentBuilder", order = 2000)]
    public class ElementParentBuilderAsset : ElementBuilderAsset
    {
        [SerializeField] private List<ElementBuilderAsset> m_children = new List<ElementBuilderAsset>();

        public List<ElementBuilderAsset> Children { get { return m_children; } }

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
                ElementBuilderAsset builder = m_children[i];
                IElement child = builder.Build(context);

                parent.Children.Add(child);
            }
        }
    }
}

using UnityEngine;

namespace UGF.Elements.Runtime
{
    public abstract class ElementBuilderAsset : ScriptableObject, IElementBuilder
    {
        public IElement Build(IElementContext context)
        {
            return OnBuild(context);
        }

        protected abstract IElement OnBuild(IElementContext context);
    }
}

using UnityEngine;

namespace UGF.Elements.Runtime
{
    public abstract class ElementBuilderAsset : ScriptableObject, IElementBuilder
    {
        public IElement Build()
        {
            return OnBuild();
        }

        protected abstract IElement OnBuild();
    }
}

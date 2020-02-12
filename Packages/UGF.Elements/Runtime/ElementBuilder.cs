using UnityEngine;

namespace UGF.Elements.Runtime
{
    public abstract class ElementBuilder : MonoBehaviour, IElementBuilder
    {
        public IElement Build()
        {
            return OnBuild();
        }

        protected abstract IElement OnBuild();
    }
}

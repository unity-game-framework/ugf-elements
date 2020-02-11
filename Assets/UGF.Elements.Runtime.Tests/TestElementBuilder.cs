using UnityEngine;

namespace UGF.Elements.Runtime.Tests
{
    public class TestElementBuilder : ElementBuilder
    {
        private void Start()
        {
            IElement element = Build(new ElementContext());

            element.Initialize();

            Debug.Log(element.Children.Count);
        }

        protected override IElement OnBuild(IElementContext context)
        {
            return new Element();
        }
    }
}

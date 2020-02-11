using UnityEngine;

namespace UGF.Elements.Runtime.Tests
{
    [CreateAssetMenu(menuName = "Tests/TestElementBuilderAsset")]
    public class TestElementBuilderAsset : ElementBuilderAsset
    {
        protected override IElement OnBuild(IElementContext context)
        {
            return new Element();
        }
    }
}

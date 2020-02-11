namespace UGF.Elements.Runtime.Tests
{
    public class TestElementBuilder : ElementBuilder
    {
        protected override IElement OnBuild(IElementContext context)
        {
            return new Element();
        }
    }
}

namespace UGF.Elements.Runtime
{
    public interface IElementBuilder
    {
        IElement Build(IElementContext context);
    }
}

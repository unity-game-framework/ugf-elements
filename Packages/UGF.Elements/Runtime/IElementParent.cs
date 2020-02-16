namespace UGF.Elements.Runtime
{
    public interface IElementParent : IElement
    {
        IElementCollection Children { get; }
    }
}

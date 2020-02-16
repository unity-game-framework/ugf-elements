namespace UGF.Elements.Runtime
{
    public class ElementParent<TChild> : Element, IElementParent where TChild : class, IElement
    {
        public ElementCollection<TChild> Children { get; } = new ElementCollection<TChild>();

        IElementCollection IElementParent.Children { get { return Children; } }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Initialize();
            }
        }

        protected override void OnPreUninitialize()
        {
            base.OnPreUninitialize();

            for (int i = Children.Count - 1; i >= 0; i--)
            {
                Children[i].Uninitialize();
            }
        }
    }
}

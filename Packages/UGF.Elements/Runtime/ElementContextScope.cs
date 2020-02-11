using System;

namespace UGF.Elements.Runtime
{
    public struct ElementContextScope : IDisposable
    {
        private readonly IElementContext m_context;
        private readonly object m_value;

        public ElementContextScope(IElementContext context, object value)
        {
            m_context = context ?? throw new ArgumentNullException(nameof(context));
            m_value = value ?? throw new ArgumentNullException(nameof(value));

            m_context.Add(m_value);
        }

        public void Dispose()
        {
            m_context.Remove(m_value);
        }
    }
}

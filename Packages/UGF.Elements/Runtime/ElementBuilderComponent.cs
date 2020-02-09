using UnityEngine;

namespace UGF.Elements.Runtime
{
    public class ElementBuilderComponent : MonoBehaviour
    {
        [SerializeReference] private IElementBuilder m_builder;

        public IElementBuilder Builder { get { return m_builder; } set { m_builder = value; } }
    }
}

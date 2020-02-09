using UnityEngine;

namespace UGF.Elements.Runtime.Tests
{
    public class TestElementBuilderFields : MonoBehaviour
    {
        [SerializeReference] private IElementBuilder m_builder1;
        [SerializeField] private IElementBuilder m_builder2;
    }
}

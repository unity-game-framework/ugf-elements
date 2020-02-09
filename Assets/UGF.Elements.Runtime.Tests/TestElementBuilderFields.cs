using System;
using UnityEngine;

namespace UGF.Elements.Runtime.Tests
{
    public class TestElementBuilderFields : MonoBehaviour
    {
        [SerializeReference] private IElementBuilder m_builder1;
        [SerializeReference] private IElementBuilder m_builder2;
        [SerializeReference] private IElementBuilder m_builder3;
    }

    [Serializable]
    public class TestBuilder : ElementBuilder
    {
        [SerializeField] private string m_stringValue;
        [SerializeField] private bool m_boolValue;
        [SerializeField] private int m_intValue;

        public string StringValue { get { return m_stringValue; } set { m_stringValue = value; } }
        public bool BoolValue { get { return m_boolValue; } set { m_boolValue = value; } }
        public int IntValue { get { return m_intValue; } set { m_intValue = value; } }
    }
}

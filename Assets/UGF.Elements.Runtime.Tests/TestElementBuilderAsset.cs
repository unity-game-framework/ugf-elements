using UnityEngine;

namespace UGF.Elements.Runtime.Tests
{
    [CreateAssetMenu(menuName = "Tests/TestElementBuilderAsset")]
    public class TestElementBuilderAsset : ElementParentBuilderAsset
    {
        [SerializeField] private bool m_boolValue;
        [SerializeField] private float m_floatValue;

        public bool BoolValue { get { return m_boolValue; } set { m_boolValue = value; } }
        public float FloatValue { get { return m_floatValue; } set { m_floatValue = value; } }
    }
}

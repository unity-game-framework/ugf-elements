using System;
using UGF.EditorTools.Editor.IMGUI.Types;
using UGF.Elements.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGF.Elements.Editor
{
    [CustomPropertyDrawer(typeof(IElementBuilder), true)]
    internal class ElementBuilderInterfaceDrawer : PropertyDrawer
    {
        private readonly GUIContent m_contentEmpty = new GUIContent(" ");
        private readonly GUIContent m_contentNone = new GUIContent("None");
        private readonly GUIContent m_contentMissing = new GUIContent("Missing");
        private TypesDropdown m_dropdown;
        private Type m_type;
        private bool m_assign;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (m_dropdown == null)
            {
                Initialize();
            }

            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType == SerializedPropertyType.ManagedReference)
            {
                GUIContent contentLabel = label;

                if (!string.IsNullOrEmpty(property.managedReferenceFullTypename))
                {
                    contentLabel = m_contentEmpty;
                }

                Rect rect = EditorGUI.PrefixLabel(position, contentLabel);
                GUIContent content = m_contentNone;

                rect.height = EditorGUIUtility.singleLineHeight;

                if (!string.IsNullOrEmpty(property.managedReferenceFullTypename))
                {
                    content = TryGetManagedReferenceType(property, out Type type) ? new GUIContent(type.Name) : m_contentMissing;
                }

                if (EditorGUI.DropdownButton(rect, content, FocusType.Keyboard) && !m_assign)
                {
                    m_dropdown.Show(rect);
                }

                if (m_assign)
                {
                    property.managedReferenceValue = m_type != null ? Activator.CreateInstance(m_type) : null;
                    property.serializedObject.ApplyModifiedProperties();

                    m_assign = false;
                }

                if (!string.IsNullOrEmpty(property.managedReferenceFullTypename))
                {
                    EditorGUI.PropertyField(position, property, true);
                }
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);

                Debug.LogWarning($"Serialized property type must be 'SerializedPropertyType.ManagedReference' in order to draw 'IElementBuilder' selector. Property path: '{property.propertyPath}'.");
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        private void Initialize()
        {
            m_dropdown = new TypesDropdown(() => TypeCache.GetTypesDerivedFrom(typeof(IElementBuilder)));
            m_dropdown.Selected += OnDropdownSelected;
        }

        private void OnDropdownSelected(Type type)
        {
            m_type = type;
            m_assign = true;
        }

        private static bool TryGetManagedReferenceType(SerializedProperty serializedProperty, out Type type)
        {
            string typeName = serializedProperty.managedReferenceFullTypename;

            if (!string.IsNullOrEmpty(typeName))
            {
                string[] names = typeName.Split(' ');

                type = Type.GetType(names.Length > 1 ? $"{names[1]}, {names[0]}" : names[0]);

                return type != null;
            }

            type = null;
            return false;
        }
    }
}

using UGF.Elements.Runtime;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace UGF.Elements.Editor
{
    [CustomEditor(typeof(ElementParentBuilder), true)]
    internal class ElementParentBuilderEditor : UnityEditor.Editor
    {
        private readonly string[] m_excluding = { "m_children" };
        private ReorderableList m_list;

        private void OnEnable()
        {
            SerializedProperty propertyChildren = serializedObject.FindProperty("m_children");

            m_list = new ReorderableList(serializedObject, propertyChildren);
            m_list.drawHeaderCallback = OnDrawHeader;
            m_list.elementHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2F;
            m_list.drawElementCallback = OnDrawElement;
            m_list.onAddCallback = OnAdd;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            DrawPropertiesExcluding(serializedObject, m_excluding);

            m_list.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }

        private void OnDrawHeader(Rect rect)
        {
            GUI.Label(rect, $"{m_list.serializedProperty.displayName} (Size: {m_list.serializedProperty.arraySize})", EditorStyles.boldLabel);
        }

        private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.height = EditorGUIUtility.singleLineHeight;
            rect.y += EditorGUIUtility.standardVerticalSpacing;

            SerializedProperty propertyElement = m_list.serializedProperty.GetArrayElementAtIndex(index);

            EditorGUI.PropertyField(rect, propertyElement, GUIContent.none);
        }

        private void OnAdd(ReorderableList list)
        {
            list.serializedProperty.InsertArrayElementAtIndex(list.serializedProperty.arraySize);

            SerializedProperty propertyElement = list.serializedProperty.GetArrayElementAtIndex(list.serializedProperty.arraySize - 1);

            propertyElement.objectReferenceValue = null;
            propertyElement.serializedObject.ApplyModifiedProperties();
        }
    }
}

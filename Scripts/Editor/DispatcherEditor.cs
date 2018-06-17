using UnityEngine;
using UnityEditor;

namespace Plugboard.Editor
{
    [CustomEditor(typeof(Dispatcher)), CanEditMultipleObjects()]
    internal sealed class DispatcherEditor : UnityEditor.Editor
    {
        private const string PROPERTY_EVENTS = "Events";
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(
                serializedObject.FindProperty(PROPERTY_EVENTS),
                new GUIContent());

            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }
    }
}
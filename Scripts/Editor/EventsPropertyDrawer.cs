using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Plugboard.Editor
{
    [CustomPropertyDrawer(typeof(Events))]
    internal sealed class EventsPropertyDrawer : PropertyDrawer
    {
        private const float FIELD_PADDING = 2f;
        private const string EVENTS_PROPERTY_LIST = "eventTypes";
        private const string EVENT_PROPERTY_NAME = "name";
        private const string EVENT_PROPERTY_ID = "id";
        private const string DEFAULT_EVENT_NAME = "<Unnamed Event>";

        private ReorderableList list;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, GUIContent.none, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't indent children
            int indentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel -= 1;

            EditorGUI.BeginChangeCheck();

            // get serialized object

            // Draw reorderable list 
            GetList(property).DoList(position);


            EditorGUI.indentLevel += 1;
            EditorGUI.EndProperty();
        }

        private SerializedProperty GetEventsList(SerializedProperty property)
        {
            SerializedObject obj = new SerializedObject(property.objectReferenceValue);
            return obj.FindProperty(EVENTS_PROPERTY_LIST);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return GetList(property).GetHeight();
        }

        private ReorderableList GetList(SerializedProperty property)
        {
            if (list == null)
                list = CreateReorderableListFor(GetEventsList(property));

            return list;
        }

        private ReorderableList CreateReorderableListFor(SerializedProperty property)
        {
            Debug.Log(property.name);
            ReorderableList list = new ReorderableList(property.serializedObject, property);

            list.drawHeaderCallback = (rect) =>
            {
                EditorGUI.LabelField(rect, "Events");
            };

            list.drawElementCallback = DrawListElement;

            list.onAddCallback = OnAddListElement;

            list.onReorderCallback = OnReorderList;

            list.onChangedCallback = (l) =>
            {
                l.serializedProperty.serializedObject.ApplyModifiedProperties();
                Debug.Log("List changed!");
            };

            return list;
        }

        private void DrawListElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.y += FIELD_PADDING;

            SerializedProperty element = list.serializedProperty.serializedObject
                .FindProperty(EVENTS_PROPERTY_LIST).GetArrayElementAtIndex(index);

            // draw id label
            int id = element.FindPropertyRelative(EVENT_PROPERTY_ID).intValue;
            //EditorGUILayout.PrefixLabel("ID: " + id);

            // draw name field
            string name = element.FindPropertyRelative(EVENT_PROPERTY_NAME).stringValue;
            name = EditorGUI.TextField(
                new Rect(
                    rect.x,
                    rect.y,
                    rect.width,
                    EditorGUIUtility.singleLineHeight),
                name);

            element.FindPropertyRelative(EVENT_PROPERTY_NAME).stringValue = name;
        }

        private void OnAddListElement(ReorderableList list)
        {
            int id = list.count;

            // add element
            list.serializedProperty.InsertArrayElementAtIndex(id);

            // set id
            list.serializedProperty
                .GetArrayElementAtIndex(id)
                .FindPropertyRelative(EVENT_PROPERTY_ID)
                .intValue = id;

            // set name
            list.serializedProperty
                .GetArrayElementAtIndex(id)
                .FindPropertyRelative(EVENT_PROPERTY_NAME)
                .stringValue = DEFAULT_EVENT_NAME;
        }

        private void OnReorderList(ReorderableList list)
        {
            // update ids
            for (int i = 0; i < list.count; i++)
            {
                list.serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative(EVENT_PROPERTY_ID).intValue = i;
            }
        }
    }
}
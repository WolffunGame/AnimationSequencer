using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BrunoMikoski.AnimationSequencer
{
    [CustomPropertyDrawer(typeof(DOTweenActionBase), true)]
    public class DOTweenActionBasePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float originY = position.y;

            Type type = property.GetTypeFromManagedFullTypeName();

            GUIContent displayName = AnimationSequenceEditorGUIUtility.GetTypeDisplayName(type);

            position.x += 10;
            position.width -= 20;

            EditorGUI.BeginProperty(position, GUIContent.none, property);

            float startingYPosition = position.y;

            EditorGUI.LabelField(position, displayName, EditorStyles.boldLabel);

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.BeginChangeCheck();

            List<string> excludeProperties = new List<string>()
            { DOTweenActionBase.EASE, DOTweenActionBase.IS_SCRIPTABLE_EASE, DOTweenActionBase.SCRIPTABLE_EASE };

            DrawPropertiesInChildren(ref position, property, excludeProperties);

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            SerializedProperty isUseScriptableEaseProperety =
                property.FindPropertyRelative(DOTweenActionBase.IS_SCRIPTABLE_EASE);
            EditorGUI.PropertyField(position, isUseScriptableEaseProperety);

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            if (isUseScriptableEaseProperety.boolValue)
            {
                SerializedProperty tmpProperty =
                    property.FindPropertyRelative(DOTweenActionBase.SCRIPTABLE_EASE);
                EditorGUI.PropertyField(position, tmpProperty);

                position.y += EditorGUI.GetPropertyHeight(tmpProperty) + EditorGUIUtility.standardVerticalSpacing;

            }
            else
            {
                SerializedProperty tmpProperty = property.FindPropertyRelative(DOTweenActionBase.EASE);
                EditorGUI.PropertyField(position, tmpProperty);

                position.y += EditorGUI.GetPropertyHeight(tmpProperty) + EditorGUIUtility.standardVerticalSpacing;

            }

            position.x -= 10;
            position.width += 10;

            Rect boxPosition = position;
            boxPosition.y = startingYPosition - 10;
            boxPosition.height = (position.y - startingYPosition) + 20;
            boxPosition.width += 20;
            GUI.Box(boxPosition, GUIContent.none, EditorStyles.helpBox);

            EditorGUI.EndProperty();
            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

            property.SetPropertyDrawerHeight(position.y - originY);
        }

        protected virtual void DrawPropertiesInChildren(ref Rect position, SerializedProperty property, List<string> excludeProperties)
        {
            foreach (SerializedProperty serializedProperty in property.GetChildren())
            {
                if (excludeProperties != null && excludeProperties.Contains(serializedProperty.name))
                {
                    continue;
                }

                Rect propertyRect = position;
                EditorGUI.PropertyField(propertyRect, serializedProperty, true);

                position.y += EditorGUI.GetPropertyHeight(serializedProperty, true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return property.GetPropertyDrawerHeight();
        }
    }
}

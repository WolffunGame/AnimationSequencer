using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BrunoMikoski.AnimationSequencer
{
    [CustomPropertyDrawer(typeof(AnimationStepBase), true)]
    public class AnimationStepBasePropertyDrawer : PropertyDrawer
    {
        protected static List<string> listExcludedItems = new List<string>();
        protected void DrawBaseGUI(Rect position, SerializedProperty property, GUIContent label, params string[] excludedPropertiesNames)
        {
            float originY = position.y;

            position.height = EditorGUIUtility.singleLineHeight;

            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, EditorStyles.foldout);

            if (property.isExpanded)
            {
                listExcludedItems.Clear();
                listExcludedItems.AddRange(excludedPropertiesNames);
                listExcludedItems.Add(AnimationStepBase.IS_USE_SCRIPTABLE_DELAY_NAME);
                listExcludedItems.Add(AnimationStepBase.SCRIPTABLE_DELAY_UNIT_NAME);
                listExcludedItems.Add(AnimationStepBase.SCRIPTABLE_DELAY_MULTIPLIER_NAME);
                listExcludedItems.Add(AnimationStepBase.DELAY_NAME);


                

                EditorGUI.BeginChangeCheck();

                EditorGUI.indentLevel++;
                position = EditorGUI.IndentedRect(position);
                EditorGUI.indentLevel--;

                position.height = EditorGUIUtility.singleLineHeight;
                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                foreach (SerializedProperty serializedProperty in property.GetChildren())
                {
                    bool shouldDraw = true;

                    if (listExcludedItems.Contains(serializedProperty.name)) 
                    {
                        shouldDraw = false;
                    }

                    if (!shouldDraw)
                        continue;

                    EditorGUI.PropertyField(position, serializedProperty);
                    position.y += EditorGUI.GetPropertyHeight(serializedProperty) + EditorGUIUtility.standardVerticalSpacing;

                }

                position.y += EditorGUIUtility.standardVerticalSpacing * 12;
                EditorGUI.LabelField(position, AnimationStepBase.DELAY_NAME.ToUpper(), EditorStyles.boldLabel);
                
                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                SerializedProperty isUseScriptableDelayProperety = property.FindPropertyRelative(AnimationStepBase.IS_USE_SCRIPTABLE_DELAY_NAME);
                EditorGUI.PropertyField(position, isUseScriptableDelayProperety);

                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                if (isUseScriptableDelayProperety.boolValue)
                {
                    SerializedProperty tmpProperty = property.FindPropertyRelative(AnimationStepBase.SCRIPTABLE_DELAY_UNIT_NAME);
                    EditorGUI.PropertyField(position, tmpProperty);

                    position.y += EditorGUI.GetPropertyHeight(tmpProperty) + EditorGUIUtility.standardVerticalSpacing;

                    
                    tmpProperty = property.FindPropertyRelative(AnimationStepBase.SCRIPTABLE_DELAY_MULTIPLIER_NAME);
                    EditorGUI.PropertyField(position, tmpProperty);

                    position.y += EditorGUI.GetPropertyHeight(tmpProperty) + EditorGUIUtility.standardVerticalSpacing;

                }
                else
                {
                    SerializedProperty tmpProperty = property.FindPropertyRelative(AnimationStepBase.DELAY_NAME);
                    EditorGUI.PropertyField(position, tmpProperty);

                    position.y += EditorGUI.GetPropertyHeight(tmpProperty) + EditorGUIUtility.standardVerticalSpacing;

                }

                if (EditorGUI.EndChangeCheck())
                    property.serializedObject.ApplyModifiedProperties();
            }

            property.SetPropertyDrawerHeight(position.y - originY + EditorGUIUtility.singleLineHeight);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawBaseGUI(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return property.GetPropertyDrawerHeight();
        }
    }
}

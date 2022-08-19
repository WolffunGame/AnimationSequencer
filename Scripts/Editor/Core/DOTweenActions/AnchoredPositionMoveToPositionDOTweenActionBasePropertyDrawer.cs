using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BrunoMikoski.AnimationSequencer
{
    [CustomPropertyDrawer(typeof(AnchoredPositionMoveToPositionDOTweenActionBase), true)]
    public class AnchoredPositionMoveToPositionDOTweenActionBasePropertyDrawer : DOTweenActionBasePropertyDrawer
    {
        protected override void DrawPropertiesInChildren(ref Rect position, SerializedProperty property, List<string> excludeProperties)
        {
            if (excludeProperties == null)
            {
                excludeProperties = new List<string>();
            }

            excludeProperties.Add(AnchoredPositionMoveToPositionDOTweenActionBase.IS_USE_SCRIPTABLE_POSITION_NAME);
            excludeProperties.Add(AnchoredPositionMoveToPositionDOTweenActionBase.SCRIPTABLE_POSITION_NAME);
            excludeProperties.Add(AnchoredPositionMoveToPositionDOTweenActionBase.POSITION_NAME);
            excludeProperties.Add(AnchoredPositionMoveToPositionDOTweenActionBase.IS_MOVE_OPPOSITE_NAME);

            base.DrawPropertiesInChildren(ref position, property, excludeProperties);

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            SerializedProperty isUseScriptablePositionProperety = 
                property.FindPropertyRelative(AnchoredPositionMoveToPositionDOTweenActionBase.IS_USE_SCRIPTABLE_POSITION_NAME);
            EditorGUI.PropertyField(position, isUseScriptablePositionProperety);

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            if (isUseScriptablePositionProperety.boolValue)
            {
                SerializedProperty tmpProperty = 
                    property.FindPropertyRelative(AnchoredPositionMoveToPositionDOTweenActionBase.SCRIPTABLE_POSITION_NAME);
                EditorGUI.PropertyField(position, tmpProperty);

                position.y += EditorGUI.GetPropertyHeight(tmpProperty) + EditorGUIUtility.standardVerticalSpacing;
                
                
                tmpProperty = 
                    property.FindPropertyRelative(AnchoredPositionMoveToPositionDOTweenActionBase.IS_MOVE_OPPOSITE_NAME);
                EditorGUI.PropertyField(position, tmpProperty);

                position.y += EditorGUI.GetPropertyHeight(tmpProperty) + EditorGUIUtility.standardVerticalSpacing;

            }
            else
            {
                SerializedProperty tmpProperty = property.FindPropertyRelative(AnchoredPositionMoveToPositionDOTweenActionBase.POSITION_NAME);
                EditorGUI.PropertyField(position, tmpProperty);

                position.y += EditorGUI.GetPropertyHeight(tmpProperty) + EditorGUIUtility.standardVerticalSpacing;

            }
        }
    }
}

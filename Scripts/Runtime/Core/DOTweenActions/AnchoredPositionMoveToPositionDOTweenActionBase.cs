using System;
using UnityEngine;

namespace BrunoMikoski.AnimationSequencer
{
    [Serializable]
    public sealed class AnchoredPositionMoveToPositionDOTweenActionBase : AnchoredPositionMoveDOTweenActionBase
    {
        public static string IS_USE_SCRIPTABLE_POSITION_NAME = nameof(isUseScriptablePosition);
        public static string SCRIPTABLE_POSITION_NAME = nameof(scriptablePosition);
        public static string IS_MOVE_OPPOSITE_NAME = nameof(isMoveOpposite);
        public static string POSITION_NAME = nameof(position);

        [SerializeField] private bool isUseScriptablePosition;
        
        [SerializeField] private Vector2Comp scriptablePosition;
        [SerializeField] private bool isMoveOpposite;


        [SerializeField] private Vector2 position;

        public Vector2 Position
        {
            get
            {
                if (isUseScriptablePosition && scriptablePosition)
                {
                    int multiplier = isMoveOpposite ? -1 : 1;
                    return scriptablePosition.Value * multiplier;
                }
                else
                {
                    return position;
                }
            }

            set
            {
                if (isUseScriptablePosition && scriptablePosition)
                {
                    scriptablePosition.SetValue(value);
                }
                else
                {
                    position = value;
                }
            }
        }

        public override string DisplayName => "Move To Anchored Position";

        protected override Vector2 GetPosition()
        {
            if (isUseScriptablePosition && scriptablePosition)
            {
                int multiplier = isMoveOpposite ? -1 : 1;
                return scriptablePosition.Value * multiplier;
            }
            else
            {
                return position;
            }
        }
    }
}

using System;
using DG.Tweening;
using UnityEngine;

namespace BrunoMikoski.AnimationSequencer
{
    [Serializable]
    public abstract class AnimationStepBase
    {

        public static string IS_USE_SCRIPTABLE_DELAY_NAME => nameof(isUseScriptableDelay);
        
        public static string SCRIPTABLE_DELAY_UNIT_NAME => nameof(scriptableDelayUnit);
        public static string SCRIPTABLE_DELAY_MULTIPLIER_NAME => nameof(scriptableDelayUnitMultiplier);

        public static string DELAY_NAME => nameof(delay);

        [SerializeField] private bool isUseScriptableDelay;

        [SerializeField] private float delay;

        [SerializeField] private FloatComp scriptableDelayUnit;
        [SerializeField] private float scriptableDelayUnitMultiplier;

        public float Delay
        {
            get
            {
                return (isUseScriptableDelay && scriptableDelayUnit) ? scriptableDelayUnit.Value * scriptableDelayUnitMultiplier : delay;
            }
        }

        [SerializeField]
        private FlowType flowType;
        public FlowType FlowType => flowType;

        public abstract string DisplayName { get; }
        
        public abstract void AddTweenToSequence(Sequence animationSequence);

        public abstract void ResetToInitialState();

        public virtual string GetDisplayNameForEditor(int index)
        {
            return $"{index}. {this}";
        }
    }
}

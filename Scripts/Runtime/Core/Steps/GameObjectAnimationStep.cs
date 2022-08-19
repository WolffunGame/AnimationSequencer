using System;
using UnityEngine;

namespace BrunoMikoski.AnimationSequencer
{
    [Serializable]
    public abstract class GameObjectAnimationStep : AnimationStepBase
    {
        public static string TARGET_VARIABLE_NAME => nameof(target);
        public static string IS_USE_SCRIPTABLE_DURATION_NAME => nameof(isUseScriptableDuration);

        public static string SCRIPTABLE_DURATION_NAME => nameof(scriptableDuration);

        public static string DURATION_NAME => nameof(duration);


        [SerializeField]
        protected GameObject target;
        public GameObject Target
        {
            get => target;
            set => target = value;
        }

        

        [SerializeField] private bool isUseScriptableDuration;

        [SerializeField] private FloatComp scriptableDuration;

        [SerializeField]
        private float duration = 1;
        public float Duration
        {
            get => (isUseScriptableDuration && scriptableDuration)? scriptableDuration.Value : duration;
            set
            {
                if (isUseScriptableDuration && scriptableDuration)
                {
                    scriptableDuration.SetValue(value);
                }
                else
                {
                    duration = value;
                }
            }
        }

        public void SetTarget(GameObject newTarget)
        {
            target = newTarget;
        }
    }
}

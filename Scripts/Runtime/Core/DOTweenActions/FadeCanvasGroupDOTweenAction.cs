using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace BrunoMikoski.AnimationSequencer
{
    [Serializable]
    public sealed class FadeCanvasGroupDOTweenAction : DOTweenActionBase
    {
        public override Type TargetComponentType => typeof(CanvasGroup);

        public override string DisplayName => "Fade Canvas Group";

        [SerializeField]
        private float alpha;
        public float Alpha
        {
            get => alpha;
            set => alpha = value;
        }

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private bool isAutoTurnOnOrOff = true;
        private float previousFade;

        protected override Tweener GenerateTween_Internal(GameObject target, float duration)
        {
            if (canvasGroup == null)
            {
                canvasGroup = target.GetComponent<CanvasGroup>();

                if (canvasGroup == null)
                {
                    Debug.LogError($"{target} does not have {TargetComponentType} component");
                    return null;
                }
            }

            previousFade = canvasGroup.alpha;
            TweenerCore<float, float, FloatOptions> canvasTween = canvasGroup.DOFade(alpha, duration);

            if (isAutoTurnOnOrOff)
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = true;
                canvasTween.onComplete += AutoTurnOnOrOff;
            }
            return canvasTween;
        }

        private void AutoTurnOnOrOff()
        {
            if (canvasGroup.alpha == 0)
            {
                canvasGroup.blocksRaycasts = canvasGroup.interactable = false;
            }
            else if (canvasGroup.alpha == 1)
            {
                canvasGroup.blocksRaycasts = canvasGroup.interactable = true;
            }
        }

        public override void ResetToInitialState()
        {
            if (canvasGroup == null)
                return;

            canvasGroup.alpha = previousFade;
        }

        public override void ResetCache(GameObject target)
        {
            canvasGroup = null;
            canvasGroup = target.GetComponent<CanvasGroup>();

        }
    }
}

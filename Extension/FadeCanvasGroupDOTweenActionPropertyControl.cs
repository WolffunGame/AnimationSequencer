using BrunoMikoski.AnimationSequencer;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCanvasGroupDOTweenActionPropertyControl : DOTweenActionBase
{
    [Serializable]
    struct CanvasGroupPropertiesChange
    {
        public bool interactable;
        public bool blockRaycast;
    }


    public override Type TargetComponentType => typeof(CanvasGroup);

    public override string DisplayName => "Fade Canvas Group With Properties Control";

    [SerializeField]
    private float alpha;
    public float Alpha
    {
        get => alpha;
        set => alpha = value;
    }

    [SerializeField] CanvasGroupPropertiesChange startTweenProperties;
    [SerializeField] CanvasGroupPropertiesChange endTweenProperties;

    [SerializeField] private CanvasGroup canvasGroup;
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
        TweenerCore<float, float, FloatOptions> canvasTween = 
            canvasGroup.DOFade(alpha, duration);

        canvasTween.OnStart(ChangePropertiesOnStart);
        canvasTween.OnComplete(ChangePropertiesOnComplete);


        return canvasTween;
    }

    void ChangePropertiesOnStart()
    {
        if (canvasGroup)
        {
            canvasGroup.interactable = startTweenProperties.interactable;
            canvasGroup.blocksRaycasts = startTweenProperties.blockRaycast;
        }
    }   
    
    void ChangePropertiesOnComplete()
    {
        if (canvasGroup)
        {
            canvasGroup.interactable = endTweenProperties.interactable;
            canvasGroup.blocksRaycasts = endTweenProperties.blockRaycast;
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

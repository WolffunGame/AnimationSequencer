using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BrunoMikoski.AnimationSequencer;
using UnityEngine.UI;
using System;

public class ScaleToAnotherObjectSize : DOTweenActionBase
{
    public override string DisplayName => "Scale To Another Object Rect Size";

    public override Type TargetComponentType => typeof(RectTransform);

    [SerializeField]
    private RectTransform.Axis axis;

    [SerializeField]
    private RectTransform anotherObjTransform;

    private RectTransform _cacheTarget;
    private RectTransform.Axis _cacheScaleType;
    private Vector2 _cacheSize;

    public override void ResetCache(GameObject target)
    {
    }

    public override void ResetToInitialState()
    {
        if (!_cacheTarget)
            return;

        switch (axis)
        {
            case RectTransform.Axis.Horizontal:
                _cacheTarget.SetSizeWithCurrentAnchors(_cacheScaleType, _cacheSize.x);
                break;
            case RectTransform.Axis.Vertical:
                _cacheTarget.SetSizeWithCurrentAnchors(_cacheScaleType, _cacheSize.y);
                break;
        }
    }

    protected override Tweener GenerateTween_Internal(GameObject target, float duration)
    {
        RectTransform rectTarget = target.transform as RectTransform;

        if (!rectTarget)
            return null;

        _cacheTarget = rectTarget;
        _cacheSize = rectTarget.rect.size;
        _cacheScaleType = axis;

        float initSize = 0;
        float desiredSize = 0;
        switch (axis)
        {
            case RectTransform.Axis.Horizontal:
                initSize = rectTarget.rect.width;
                desiredSize = anotherObjTransform.rect.width;
                break;
            case RectTransform.Axis.Vertical:
                initSize = rectTarget.rect.height;
                desiredSize = anotherObjTransform.rect.height;
                break;
        }

        Tweener tween = DOVirtual.Float(initSize, desiredSize, duration, (updateValue) =>
        {
            rectTarget.SetSizeWithCurrentAnchors(axis, updateValue);
        });

        return tween;
    }
}

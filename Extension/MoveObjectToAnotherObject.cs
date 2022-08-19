using BrunoMikoski.AnimationSequencer;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveObjectToAnotherObject : DOTweenActionBase
{
    public override string DisplayName => "MoveObjectToAnotherObject";

    public override Type TargetComponentType => typeof(RectTransform);

    [SerializeField]
    private RectTransform destinationTransform;

    private Vector3 initialPosition;

    private RectTransform cachedRectTransform;

    protected override Tweener GenerateTween_Internal(GameObject target, float duration)
    {
        RectTransform targetTransform = target.transform as RectTransform;

        if (targetTransform == null)
            return null;

        cachedRectTransform = targetTransform;
        initialPosition = targetTransform.position;

        var startPosition = Vector3.zero;

        var tween = DOVirtual.Float(0, 1, duration, (value) =>
        {
            var a = startPosition;
            var b = destinationTransform.position;

            targetTransform.position = a + (b - a) * value;
        }).SetEase(ease)
        .OnStart(() => 
        {
            startPosition = targetTransform.position;
        });

        //var tween = targetTransform.DOMove(destinationTransform.position, duration, true).SetEase(ease);
        return tween;
    }

    public override void ResetToInitialState()
    {
        if (cachedRectTransform != null)
            cachedRectTransform.position = initialPosition;
    }

    public override void ResetCache(GameObject target)
    {
    }
}

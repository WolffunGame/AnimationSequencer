using BrunoMikoski.AnimationSequencer;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
class BoolWrapper
{
    public bool isPassCondition;
}

public class ConditionalWrapperAnimationSequence : AnimationStepBase
{
    [SerializeField]
    private UnityEvent<BoolWrapper> conditionChecker;

    [SerializeField]
    private AnimationSequencerController sequencer;

    BoolWrapper conditionResult;

    public override string DisplayName => "Conditional Animation Sequence Switch Wrapper ";

    public override string GetDisplayNameForEditor(int index)
    {
        return index + DisplayName;
    }

    public override void AddTweenToSequence(Sequence animationSequence)
    {
        if (conditionResult == null)
        {
            conditionResult = new BoolWrapper();
        }

        conditionChecker?.Invoke(conditionResult);

        if (!conditionResult.isPassCondition)
            return;

        Sequence sequence = sequencer.GenerateSequence();
        sequence.SetDelay(Delay);
        if (FlowType == FlowType.Join)
            animationSequence.Join(sequence);
        else
            animationSequence.Append(sequence);
    }

    public override void ResetToInitialState()
    {
        sequencer.ResetToInitialState();
    }
}

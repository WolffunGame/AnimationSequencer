using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrunoMikoski.AnimationSequencer;
using DG.Tweening;
using System;

public class SwitchWrapperAnimationSequence : AnimationStepBase
{
    [SerializeField]
    private GameObject gOSwitch;

    [SerializeField]
    private AnimationSequencerController sequencer;

    public override string DisplayName => "Animation Sequence Switch Wrapper";

    public override string GetDisplayNameForEditor(int index)
    {
        return index + ". Animation Sequence Switch Wrapper";
    }

    public override void AddTweenToSequence(Sequence animationSequence)
    {
        if (!gOSwitch.activeSelf)
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

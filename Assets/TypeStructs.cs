using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    SwiftAttack,
    HeavyAttack,
    Block,
    Dodge,
    NotTaken
}
public class ActionTakenEventArgs : EventArgs
{
    public ActionType ActionT { get; set; }
    public object Sender { get; set; }
}
public enum ActionCompareResult
{
    BlockHeavyAttack,
    BlockSwiftAttack,
    BlockBlock,
    BlockDodge,
    DodgeHeavyAttack,
    DodgeSwiftAttack,
    DodgeBlock,
    DodgeDodge,
    HeavyAttackHeavyAttack,
    HeavyAttackSwiftAttack,
    HeavyAttackBlock,
    HeavyAttackDodge,
    SwiftAttackHeavyAttack,
    SwiftAttackSwiftAttack,
    SwiftAttackBlock,
    SwiftAttackDodge,
    NotTakenDodge,
    NotTakenHeavyAttack,
    NotTakenSwiftAttack,
    NotTakenBlock
}


[System.Serializable]
public class ActionReaction
{
    public ActionCompareResult res;
    public AnimationClip playerAnimation;
    public AnimationClip bossAnimation;
    public AudioClip sound;
}

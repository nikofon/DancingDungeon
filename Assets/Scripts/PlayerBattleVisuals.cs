using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleVisuals : MonoBehaviour
{
    private Animator am;
    void Start()
    {
        am = GetComponent<Animator>();
        BattleController.instance.OnActionTaken += PlayAnimation;
    }

    private void PlayAnimation(ActionTakenEventArgs e)
    {
        if (e.Sender != (object) PlayerStateManager.instance) return;
        if (!RtythManager.instance.Synced) return;
        switch (e.ActionT)
        {
            case ActionType.SwiftAttack:
                am.Play("swiftAttack");
                break;
        }
    }
}

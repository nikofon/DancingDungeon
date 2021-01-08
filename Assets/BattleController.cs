using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController instance;
    public event ActionTakenDelegate OnActionTaken;
    public delegate void ActionTakenDelegate(ActionTakenEventArgs e);
    private void Awake()
    {
        instance = this;
    }
    public void OnActionTakenInvoke(ActionType t, object sender)
    {
        OnActionTaken?.Invoke(new ActionTakenEventArgs
        {
            ActionT = ActionType.HeavyAttack,
            Sender = sender
        });
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("z");
            OnActionTaken?.Invoke(new ActionTakenEventArgs
            {
                ActionT = ActionType.HeavyAttack,
                Sender = PlayerStateManager.instance
            });
            return;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            OnActionTaken?.Invoke(new ActionTakenEventArgs
            {
                ActionT = ActionType.SwiftAttack,
                Sender = PlayerStateManager.instance
            });
            return;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnActionTaken?.Invoke(new ActionTakenEventArgs
            {
                ActionT = ActionType.Dodge,
                Sender = PlayerStateManager.instance
            });
            return;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            OnActionTaken?.Invoke(new ActionTakenEventArgs
            {
                ActionT = ActionType.Block,
                Sender = PlayerStateManager.instance
            });
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RtythManager : MonoBehaviour
{
    #region TEST
    public AudioSource metronome;
    private void Start()
    {
        StartCoroutine(Metronome(1f));
        SetEnemy(FindObjectOfType<Enemy>());
        BattleController.instance.OnActionTaken += RecieveAction;
    }
    #endregion
    private ActionType currentPlayersAction;
    private ActionType currentEnemyAction;
    public Enemy CurrentEnemy { get; private set; }
    private bool synced = false;
    public float errorMargin;
    public void SetEnemy(Enemy enemy)
    {
        CurrentEnemy = enemy;
    }
    public void RecieveAction(ActionTakenEventArgs e)
    {
        if (e.Sender == (object) PlayerStateManager.instance)
        {
            if (!synced)
            {
                PlayerStateManager.AsyncPenalty();
            }
            else { currentPlayersAction = e.ActionT; }
        }
    }
    private ActionCompareResult CompareActions(ActionType playerAct, ActionType enemyAct, Enemy sender)
    {
        if(playerAct == ActionType.NotTaken)
        {
            return ActionCompareResult.NotTakenBlock;
        }
        if (playerAct == ActionType.Block)
        {
            if (enemyAct == ActionType.HeavyAttack)
            {
                sender.Attack(ActionType.HeavyAttack);
                return ActionCompareResult.BlockHeavyAttack;
            }
            if (enemyAct == ActionType.Dodge)
            {
                return ActionCompareResult.BlockDodge;
            }
            if (enemyAct == ActionType.SwiftAttack)
            {
                return ActionCompareResult.BlockSwiftAttack;
            }
            if (enemyAct == ActionType.Block)
            {
                return ActionCompareResult.BlockBlock;
            }
        }
        if (playerAct == ActionType.Dodge)
        {
            if (enemyAct == ActionType.SwiftAttack)
            {
                sender.Attack(ActionType.SwiftAttack);
                return ActionCompareResult.DodgeSwiftAttack;
            }
            if (enemyAct == ActionType.Block)
            {
                return ActionCompareResult.DodgeBlock;
            }
            if (enemyAct == ActionType.Dodge)
            {
                return ActionCompareResult.DodgeDodge;
            }
            if (enemyAct == ActionType.HeavyAttack)
            {
                return ActionCompareResult.DodgeHeavyAttack;
            }
        }
        if (playerAct == ActionType.SwiftAttack)
        {
            if (enemyAct == ActionType.HeavyAttack)
            {
                sender.Attack(ActionType.HeavyAttack);
                PlayerStateManager.Attack(ActionType.SwiftAttack, sender);
                return ActionCompareResult.SwiftAttackHeavyAttack;
            }
            if (enemyAct == ActionType.SwiftAttack)
            {
                sender.Attack(ActionType.SwiftAttack);
                PlayerStateManager.Attack(ActionType.SwiftAttack, sender);
                return ActionCompareResult.SwiftAttackSwiftAttack;
            }
            if (enemyAct == ActionType.Dodge)
            {
                PlayerStateManager.Attack(ActionType.SwiftAttack, sender);
                return ActionCompareResult.SwiftAttackDodge;
            }
            if (enemyAct == ActionType.Block)
            {
                return ActionCompareResult.SwiftAttackBlock;
            }
        }
        if(playerAct == ActionType.HeavyAttack)
        {
            if (enemyAct == ActionType.Dodge)
            {
                return ActionCompareResult.HeavyAttackDodge;
            }
            if(enemyAct == ActionType.HeavyAttack)
            {
                PlayerStateManager.Attack(ActionType.HeavyAttack, sender);
                sender.Attack(ActionType.HeavyAttack);
                return ActionCompareResult.HeavyAttackHeavyAttack;
            }
            if (enemyAct == ActionType.SwiftAttack)
            {
                PlayerStateManager.Attack(ActionType.HeavyAttack, sender);
                sender.Attack(ActionType.SwiftAttack);
                return ActionCompareResult.HeavyAttackSwiftAttack;
            }
        }
        PlayerStateManager.Attack(ActionType.HeavyAttack, sender);
        return ActionCompareResult.HeavyAttackBlock;
    }
    public IEnumerator Metronome(float beat)
    {
        synced = true;
        currentPlayersAction = ActionType.NotTaken;
        while (true)
        {
            metronome.Play();
            yield return new WaitForSeconds(errorMargin);
            Debug.Log(CompareActions(currentPlayersAction, currentEnemyAction, CurrentEnemy).ToString());
            BattleController.instance.OnActionTakenInvoke(CurrentEnemy.Act(), CurrentEnemy);
            currentPlayersAction = ActionType.NotTaken;
            synced = false;
            yield return new WaitForSeconds(beat - 2*errorMargin);
            synced = true;
            yield return new WaitForSeconds(errorMargin);
        }
    }
}

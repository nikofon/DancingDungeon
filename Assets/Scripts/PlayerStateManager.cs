using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager instance;
    public int Health { get; private set; }
    public int penalty;
    public int SwiftAttackDmg { get; private set; }
    public int HeavyAttackDmg { get; private set; }
    private void Awake()
    {
        instance = this;
    }
    public static void Die()
    {
        Debug.Log("you are dead");
    }
    public static void TakeDamage(int damage)
    {

    }
    public static void Attack(ActionType attack, Enemy reciever)
    {
        switch (attack)
        {
            case ActionType.HeavyAttack:
                reciever.TakeDamage(instance.HeavyAttackDmg);
                break;
            case ActionType.SwiftAttack:
                reciever.TakeDamage(instance.SwiftAttackDmg);
                break;
        }
    }
    public static void AsyncPenalty()
    {
        instance.Health -= instance.penalty;
        Debug.Log("missed!");
    }

}

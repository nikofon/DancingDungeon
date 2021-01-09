using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    public int Health { get { return health; } protected set { health = value; } }
    [SerializeField]
    private int health;
    public int SwiftAttackDamage { get { return swiftAttackDamage; } protected set { swiftAttackDamage = value; } }
    [SerializeField]
    private int swiftAttackDamage;
    public int HeavyAttackDamage { get { return heavyAttackDamage; } protected set { heavyAttackDamage = value; } }
    [SerializeField]
    private int heavyAttackDamage;
    System.Random random = new System.Random();

    public ActionType Act()
    {
        List<ActionType> values = Enum.GetValues(typeof(ActionType)).ArrayToList<ActionType>();
        values.Remove(ActionType.NotTaken);

        ActionType randomAction = values[random.Next(values.Count)];
        return randomAction;
    } 

    public void Attack(ActionType attack)
    {
        switch(attack)
        {
            case ActionType.HeavyAttack:
                PlayerStateManager.TakeDamage(HeavyAttackDamage);
                break;
            case ActionType.SwiftAttack:
                PlayerStateManager.TakeDamage(SwiftAttackDamage);
                break;
        }

    }
    public void TakeDamage(int dmg)
    {
        Health -= dmg;
    }
}

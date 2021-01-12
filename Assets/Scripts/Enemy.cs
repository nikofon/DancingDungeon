using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public TextMeshProUGUI endText;
    public TextMeshProUGUI textMesh;
    private Animator am;
    public int Health { get { return health; } protected set { if(value == 0)  endText.gameObject.SetActive(true); health = value; textMesh.text = "Health " + value; } }
    [SerializeField]
    private int health;
    public int SwiftAttackDamage { get { return swiftAttackDamage; } protected set { swiftAttackDamage = value; } }
    [SerializeField]
    private int swiftAttackDamage;
    public int HeavyAttackDamage { get { return heavyAttackDamage; } protected set { heavyAttackDamage = value; } }
    [SerializeField]
    private int heavyAttackDamage;
    System.Random random = new System.Random();

    private void Start()
    {
        endText.gameObject.SetActive(false);
        am = GetComponent<Animator>();
        BattleController.instance.OnActionTaken += PlayAnimation;
    }
    public ActionType Act()
    {
        List<ActionType> values = Enum.GetValues(typeof(ActionType)).ArrayToList<ActionType>();
        values.Remove(ActionType.NotTaken);

        ActionType randomAction = values[random.Next(values.Count)];
        return randomAction;
    } 

    public void PlayAnimation(ActionTakenEventArgs e)
    {
        if(e.Sender != (object) PlayerStateManager.instance)
        {
            switch (e.ActionT)
            {
                case ActionType.Block: am.Play("Block");
                    break;
                case ActionType.HeavyAttack: am.Play("Attack");
                    break;

            }
        }
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

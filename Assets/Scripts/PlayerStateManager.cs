using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public TextMeshProUGUI healthBar;
    public static PlayerStateManager instance;
    public static event Action OnDeath;
    public int Health { get { return health; } private set { if (value == 0) Die(); health = value; if (healthBar != null) healthBar.text = "Health: " + value; } }
    [SerializeField]
    private int health;
    public int penalty;
    public int SwiftAttackDmg { get { return swiftAttackDamage; } }
    [SerializeField]
    private int swiftAttackDamage;
    public int HeavyAttackDmg { get { return heavyAttackDmg; } }
    [SerializeField]
    private int heavyAttackDmg;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
    }
    public static void Restart()
    {
        AudioManager.instance.SaveSceneData();
        AudioManager.instance.UnubscriveToEvents();
        LevelLoader.ReloadLevel();
    }
    public static void Die()
    {
        OnDeath?.Invoke();
        AudioManager.instance.UnubscriveToEvents();
        LevelLoader.ReloadLevel();
        
    }
    public static void TakeDamage(int damage)
    {
        instance.Health -= damage;
    }
    public static void Attack(ActionType attack, Enemy reciever)
    {

        switch (attack)
        {
            case ActionType.HeavyAttack:
                reciever.TakeDamage(instance.HeavyAttackDmg);
                break;
            case ActionType.SwiftAttack:
                Debug.Log("Attacking!");
                reciever.TakeDamage(instance.SwiftAttackDmg);
                break;
        }
    }
    public static void AsyncPenalty()
    {
        instance.Health -= instance.penalty;
    }

}

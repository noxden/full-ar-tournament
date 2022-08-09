//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 09-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    //# Public Variables 
    public MonsterData monsterData;
    public new string name { get { return GetName(); } }
    public string species { get; private set; }
    public string customName { get; private set; }
    public Gender gender { get; private set; }
    public List<ElementalType> Types { get; private set; }

    public int level { get; private set; }
    public int hpMax { get; private set; }
    public int hpCurrent { get; private set; }
    public int attack { get; private set; }
    public int defense { get; private set; }
    public int specialAttack { get; private set; }
    public int specialDefense { get; private set; }
    public int speed { get; private set; }
    public int evasion { get; private set; }
    public int accuracy { get; private set; }
    public List<Action> AvailableActions { get; private set; }

    //# Monobehaviour Events 
    private void Start()
    {
        if (monsterData == null)
            Debug.LogWarning($"Monster.Start: Monster {this.name} does not hold any monsterData to load.");     //< Should probably be loaded from outside after Instantiating anyways
        else
            ReloadMonsterData();
    }

    //# Public Methods 
    public void UseAction(Action action, Monster user, Monster opponent)
    {
        action.Use(user, opponent);
    }

    public void ApplyStatModification(StatModification modification)
    {
        int value = modification.value;
        switch (modification.stat)
        {
            case Stat.HP:
                hpCurrent += value;
                hpCurrent = Mathf.Clamp(hpCurrent, 0, hpMax);
                break;
            case Stat.Attack:
                attack += value;
                attack = Mathf.Clamp(attack, 0, int.MaxValue);
                break;
            case Stat.Defense:
                defense += value;
                defense = Mathf.Clamp(defense, 0, int.MaxValue);
                break;
            case Stat.SpecialAttack:
                specialAttack += value;
                specialAttack = Mathf.Clamp(specialAttack, 0, int.MaxValue);
                break;
            case Stat.SpecialDefense:
                specialDefense += value;
                specialDefense = Mathf.Clamp(specialDefense, 0, int.MaxValue);
                break;
            case Stat.Speed:
                speed += value;
                speed = Mathf.Clamp(speed, 0, int.MaxValue);
                break;
            case Stat.Evasion:
                evasion += value;
                evasion = Mathf.Clamp(evasion, 0, int.MaxValue);
                break;
            case Stat.Accuracy:
                accuracy += value;  //< Does not have to be clamped to 0, as accuracy is a modifier applied to any action accuracy.
                break;
        }
        GameManager.QueueFlavourText($"Monster.ApplyStatModification: {GetName()}'s {modification.stat} has been {(modification.value >= 0 ? "increased" : "decreased")} by {modification.value * (modification.value >= 0 ? 1 : -1)}.", this);
    }

    private string GetName()
    {
        string displayName;
        if (string.IsNullOrEmpty(customName) || string.IsNullOrWhiteSpace(customName))
        {
            displayName = species;
        }
        else
        {
            displayName = $"{customName} ({species})";
        }
        return displayName;
    }

    public bool isValid()
    {
        return (hpCurrent > 0);
    }

    public void ReloadMonsterData()
    {
        LoadMonsterData(monsterData);
    }

    public void LoadMonsterData(MonsterData _monsterData)
    {
        monsterData = _monsterData;
        species = _monsterData.species;
        customName = _monsterData.customName;
        Gender gender = _monsterData.gender;
        List<ElementalType> Types = _monsterData.Types;

        level = _monsterData.level;
        hpMax = _monsterData.hpMax;
        hpCurrent = _monsterData.hpMax;
        attack = _monsterData.attack;
        defense = _monsterData.defense;
        specialAttack = _monsterData.specialAttack;
        specialDefense = _monsterData.specialDefense;
        speed = _monsterData.speed;

        AvailableActions = new List<Action>();
        foreach (var entry in _monsterData.AvailableActionsByLibraryIndexes)
        {
            AvailableActions.Add(GameManager.Instance.GetActionByLibraryIndex(entry));
        }
    }
    // public void Animation_Spawn()
    // {

    // }

    // public void Animation_Despawn()
    // {

    // }
    // public void Destroy()
    // {
    //     Destroy(this.gameObject);
    // }
}

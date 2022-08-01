//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 20-07-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementalType { Normal, Fire, Water, Grass }
public enum Gender { Male, Female, Unknown }

// Todo: Could this also be converted into a scriptable object?

public class Monster : MonoBehaviour
{
    //# Public Variables 
    //public GameObject modelPrefab;
    public string species;
    public string customName;
    public Gender gender;
    public List<ElementalType> Types;

    public int level;
    public int hpMax;
    public int hpCurrent;
    public int attack;
    public int defense;
    public int specialAttack;
    public int specialDefense;
    public int speed;
    public int evasion;
    public int accuracy;
    public List<Action> AvailableActions;                 //< Maybe an array is better for this case?
                                                          //public Action[] AvailableActions = new Action[4];   //< Arrays are a pain to deal with

    //public Player owner;  //< Probably not needed ; THIS IS DANGEROUS RECURSION -> Monster has owner and Owner has Monster

    //# Monobehaviour Events 
    private void Start()
    {
        hpCurrent = hpMax;    //< Maybe remove this as well
    }

    //# Public Methods 
    public void UseAction(Action action)
    {
        Monster enemy = CombatHandler.Instance.GetEnemy().monsterOnField;
        action.Use(this, enemy);
    }

    public void ApplyStatModification(StatModification modification)
    {
        int value = modification.value;
        switch (modification.stat)
        {
            case Stat.HP:
                hpCurrent += value;
                Mathf.Clamp(hpCurrent, 0, hpMax);
                break;
            case Stat.Attack:
                attack += value;
                break;
            case Stat.Defense:
                defense += value;
                break;
            case Stat.SpecialAttack:
                specialAttack += value;
                break;
            case Stat.SpecialDefense:
                specialDefense += value;
                break;
            case Stat.Speed:
                speed += value;
                break;
            case Stat.Evasion:
                evasion += value;
                break;
            case Stat.Accuracy:
                accuracy += value;
                break;
        }
    }

    public string GetName()
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

    // public bool GetIsInBag()
    // {
    //     List<Monster> monsterBag = owner.Monsters;
    //     return monsterBag.Contains(this);
    // }

    // public bool GetIsInBox()
    // {
    //     List<Monster> monsterBox = owner.Monsters;
    //     return monsterBox.Contains(this);
    // }

    public void Spawn()
    {

    }

    public void Despawn()
    {

    }
}

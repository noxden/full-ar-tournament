//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 03-08-22
// Todo: Could this also be converted into a scriptable object?
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementalType { Normal, Fire, Water, Grass, Electic }
public enum Gender { Male, Female, Unknown }

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
        Debug.Log($"Monster.ApplyStatModification: {GetName()}'s {modification.stat} has been {(modification.value >= 0 ? "increased" : "decreased")} by {modification.value * (modification.value >= 0 ? 1 : -1)}.");
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

    public bool isValid()
    {
        return (hpCurrent > 0);
    }

    public float RelaySpeedValue(float newSpeedValue)
    {
        return newSpeedValue;
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

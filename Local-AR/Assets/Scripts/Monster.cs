//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 24-06-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementalType { Normal, Fire, Water, Grass }

// Todo: Could this also be converted into a scriptable object?

public class Monster : MonoBehaviour
{
    //# Public Variables 
    public string species;
    public string customName;
    public List<ElementalType> Types;

    public int level;
    public int hp_max;
    public int hp_current;
    public int attack;
    public int defense;
    public int specialAttack;
    public int specialDefense;
    public int speed;
    public int evasion;
    public int accuracy;
    public List<Action> AvailableActions;
    //public List<StatModification> ActiveEffects;  //< Maybe for later implementation of temporary effects.

    public Player owner;  //< Probably not needed
    public bool isInBag;
    public bool isOnField;

    //# Monobehaviour Events 
    private void Start()
    {
        if (customName == null)
        {
            customName = species;
        }
    }

    //# Public Methods 
    public void useAction(Action action)
    {
        Monster enemy = CombatHandler.Instance.GetEnemyData(owner).GetMonsterIsOnField();
        action.use(this, enemy);
    }



    public void ApplyStatModification(StatModification modification)
    {
        int value = modification.value;
        switch (modification.stat)
        {
            case Stat.HP:
                hp_current += value;
                Mathf.Clamp(hp_current, 0, hp_max);
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
}

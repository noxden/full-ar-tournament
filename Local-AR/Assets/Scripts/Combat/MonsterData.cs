//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 04-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementalType { Normal, Fire, Water, Grass, Electic , Ice, Fighting, Poison, Ground, Flying, Psychic, Bug, Rock, Ghost, Dark, Dragon, Steel, Fairy}
public enum Gender { Male, Female, Unknown }

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable/MonsterData")]
public class MonsterData : ScriptableObject
{
    //# Public Variables 
    public GameObject modelPrefab;
    public Sprite icon;
    public string species;
    public string customName;
    public Gender gender;
    public List<ElementalType> Types;

    public int level;
    public int hpMax;
    public int attack;
    public int defense;
    public int specialAttack;
    public int specialDefense;
    public int speed;
    public List<int> AvailableActionsByLibraryIndexes;

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
}


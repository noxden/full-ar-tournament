//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 14-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementalType { Normal, Fire, Water, Grass, Electric , Ice, Fighting, Poison, Ground, Flying, Psychic, Bug, Rock, Ghost, Dark, Dragon, Steel, Fairy}
public enum Gender { Male, Female, Unknown }

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable/MonsterData")]
public class MonsterData : ScriptableObject
{
    //# Public Variables 
    public GameObject modelPrefab;
    public Sprite icon;
    public new string name { get { return GetName(); } }
    public string species;
    public string nickname;
    public Gender gender;
    public List<ElementalType> Types;

    public int level;
    public int hpMax;
    public int attack;
    public int defense;
    public int specialAttack;
    public int specialDefense;
    public int speed;
    public List<Action> AvailableActions;

    //# Private Methods 
    private string GetName()
    {
        string displayName;
        if (string.IsNullOrWhiteSpace(nickname))    //< IsNullOrWhiteSpace returns true if the value parameter is null or Empty, or if value consists exclusively of white-space characters.
        {
            displayName = species;
        }
        else
        {
            displayName = $"{nickname} ({species})";
        }
        return displayName;
    }
}


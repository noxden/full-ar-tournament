//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 24-06-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stat { HP, Attack, Defense, SpecialAttack, SpecialDefense, Speed, Evasion, Accuracy }

[CreateAssetMenu(fileName = "New StatModification", menuName = "Scriptable/StatModification")]
public class StatModification : ScriptableObject
{
    public Stat stat;
    public int value;
}

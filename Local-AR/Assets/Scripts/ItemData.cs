//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 24-06-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//> Can be used on all monsters of a battle -> Active Ally, Active Enemy, Bagged Allies.
[CreateAssetMenu(fileName = "New ItemData", menuName = "Scriptable/ItemData")]
public class ItemData : ScriptableObject
{
    public string description;
    public List<StatModification> Effect;
}
